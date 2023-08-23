using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour, IDamage
{
    public float damage { get; private set; }

    public int targetPlayerID; // Идентификатор игрока, на которого пуля попала

    private PhotonView photonView;

    private void Awake()
    {
        damage = 10f;

        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    private void DestroyBulletRPC(int playerActorNumber)
    {
        if (playerActorNumber == photonView.Owner.ActorNumber) 
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            int playerActorNumber = player.GetPlayerActorNumber();

            if (!photonView.IsMine)
            {
                photonView.RPC("DestroyBulletRPC", RpcTarget.All, playerActorNumber);
            }
            Destroy(gameObject);
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
