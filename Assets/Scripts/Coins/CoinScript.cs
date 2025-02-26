using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private Text coinText;
    public int currentCoins;

    private void Awake() {
        currentCoins = 0;
    }

    public void AddCoin(int number) {
        currentCoins += number;
        coinText.text = currentCoins.ToString() + "/3";
    }
}
