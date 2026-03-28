using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TMP_Text coinCountText;
    private int coinCount;

    public void UpdateCoinsText()
    {
        coinCountText.text = coinCount.ToString();
    }

    // Will Be Called Through Unity Event
    public void AddCoin(int coin)
    {
        if (coin > 0)
        {
            coinCount += coin;
            UpdateCoinsText();
        }
    }
}
