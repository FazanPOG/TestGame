using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounterText;

    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        player.OnTakeCoin += Player_OnTakeCoin;
    }

    private void Player_OnTakeCoin()
    {
        UpdateVisual();
    }

    private void UpdateVisual() 
    {
        int coinCount = player.coinCount;
        coinCounterText.text = coinCount.ToString();
    }
}
