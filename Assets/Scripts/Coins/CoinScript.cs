using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private Text coinText;
    private int currentCoins;

    private void Awake() {
        currentCoins = 0;
    }

    public void AddCoin() {
        currentCoins += 1;
        coinText.text = currentCoins.ToString() + "/3";
    }
}
