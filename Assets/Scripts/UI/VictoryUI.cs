using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VictoryUI : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI coinCount;


    private void Start()
    {
        Hide();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == 1) 
        {
            Show();
            Player player = FindObjectOfType<Player>();
            playerName.text = player.name;
            coinCount.text = player.coinCount.ToString();
        }
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
