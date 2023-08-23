using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private PhotonView photonView;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        player.OnHit += Player_OnHit;
    }

    private void Player_OnHit()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        float health = player.health;
        healthBar.fillAmount = health / 100f;
        //photonView.RPC("UpdateVisualSync", RpcTarget.All);
    }

    [PunRPC]
    private void UpdateVisualSync() 
    {
        if (photonView.IsMine) 
        {
            float health = player.health;
            healthBar.fillAmount = health / 100f;
        }
    }
}
