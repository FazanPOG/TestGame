using System;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;

    public float health {  get; private set; }
    public int coinCount {  get; private set; }

    public string playerName { get; private set; }

    private FixedJoystick joystick;
    private PlayerVisual playerVisual;
    private Rigidbody2D _rigidbody2D;
    private float moveSpeed;

    private PhotonView photonView;

    public event Action OnHit;
    public event Action OnTakeCoin;
    public event Action<Player> OnPlayerDied;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerVisual = GetComponentInChildren<PlayerVisual>();
        photonView = GetComponent<PhotonView>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    private void Start()
    {
        health = 100f;
        coinCount = 0;
        playerName = "Player " + GetPlayerActorNumber().ToString();

        playerVisual.OnDeathAnimationEnd += PlayerVisual_OnDeathAnimationEnd;
    }

    private void PlayerVisual_OnDeathAnimationEnd()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCount > 1) 
            {
                Movement();
            }
            
        }
    }

    private void Movement()
    {
        _rigidbody2D.velocity = new Vector2(joystick.Horizontal * speed, joystick.Vertical * speed);
    }

    public float GetMoveSpeed() 
    {
        if (Mathf.Abs(joystick.Horizontal * speed) > 1.5f || Mathf.Abs(joystick.Vertical * speed) > 1.5f)
        {
            moveSpeed = 1f;
        }
        else 
        {
            moveSpeed = 0f;
        }

        return moveSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamage>(out IDamage iDamage))
        {
            TakeDamage(iDamage.damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            coinCount++;

            OnTakeCoin?.Invoke();
        }
    }

    private void TakeDamage(float damage) 
    {
        health -= damage;

        if (health < 0)
        {
            OnPlayerDied?.Invoke(this);
            return;
        }

        OnHit?.Invoke();

    }

    public int GetPlayerActorNumber() 
    {
        return photonView.Owner.ActorNumber;
    }
}
