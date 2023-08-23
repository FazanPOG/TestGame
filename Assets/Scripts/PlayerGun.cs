using Photon.Pun;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField, Range(10, 40)] private float bulletSpeed = 12f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private FixedJoystick joystick;
    private PlayerGunVisual playerGunVisual;
    private Vector2 lastDirection;
    private float rotationAngle;

    private PhotonView photonView;

    private void Awake()
    {
        playerGunVisual = GetComponentInChildren<PlayerGunVisual>();
        joystick = FindObjectOfType<FixedJoystick>();
        photonView = GetComponentInParent<PhotonView>();
    }

    private void Start()
    {
        lastDirection = new Vector2(-1f, 0f);
        playerGunVisual.OnShoot += PlayerGunVisual_OnShoot;
    }

    private void PlayerGunVisual_OnShoot()
    {
        if (photonView.IsMine)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1) 
            {
                SpawnBullet();
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine) 
        {
            GunRotation();
        }
    }

    private void GunRotation() 
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            lastDirection = new Vector2(horizontalInput, verticalInput);

            rotationAngle = Mathf.Atan2(-verticalInput, -horizontalInput) * Mathf.Rad2Deg;

            if (rotationAngle < 0)
            {
                rotationAngle += 360;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        }
    }

    private void SpawnBullet() 
    {
        GameObject bulletClone = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawnPoint.position, Quaternion.Euler(0f, 0f, rotationAngle));
        AddBulletForce(bulletClone);
    }
    private void AddBulletForce(GameObject bullet) 
    {
        Rigidbody2D bulletRigidbody2d;
        bullet.TryGetComponent<Rigidbody2D>(out bulletRigidbody2d);

        
        lastDirection = lastDirection.normalized;
        bulletRigidbody2d.AddForce(lastDirection * bulletSpeed, ForceMode2D.Impulse);
    }
}
