using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private float minX = -7.5f;
    private float maxX = 7.5f;
    private float minY = -3.5f;
    private float maxY = 3.5f;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

}
