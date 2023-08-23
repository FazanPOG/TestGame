using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    private void Start()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount > 1) 
        {
            StartCoroutine(SpawnCoin());
        }
    }

    IEnumerator SpawnCoin() 
    {
        while (true)
        {
            CreateCoinClone();
            yield return new WaitForSecondsRealtime(4f);
        }
    }

    private void CreateCoinClone() 
    {
        float leftBorder = -7.5f;
        float rightBorder = 7.5f;
        float downBorder = -3.5f;
        float upBorder = 3.5f;

        float randomXPosition = Random.Range(leftBorder, rightBorder);
        float randomYPosition = Random.Range(downBorder, upBorder);
        Vector3 randomPosition = new Vector3(randomXPosition, randomYPosition);
        PhotonNetwork.Instantiate(coinPrefab.name, randomPosition, Quaternion.identity);
    }

}
