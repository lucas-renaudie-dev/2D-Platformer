using Microsoft.Unity.VisualStudio.Editor;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image Heart1fill;
    [SerializeField] private Image Heart2fill;
    [SerializeField] private Image Heart3fill;
    [SerializeField] private GameObject Heart2; //for impossible mode
    [SerializeField] private GameObject Heart3; //for impossible mode
    
    private void Awake() {
        string diff = DifficultyScript.Instance.currentDifficulty;
        
        if (diff == "IMPOSSIBLE") {
            Heart2.SetActive(false);
            Heart3.SetActive(false);

            //gameObject.SetActive(false); //comment out the 2 above if want no health bar
        }
        else {
            Heart2.SetActive(true);
            Heart3.SetActive(true);

            //gameObject.SetActive(true); //comment out the 2 above if want no health bar
        }
    }

    void Start()
    {
        Heart1fill.fillAmount = 1;
        Heart2fill.fillAmount = 1;
        Heart3fill.fillAmount = 1;
    }

    void Update()
    {
        if (playerHealth.currentHealth == 3) {
            Heart1fill.fillAmount = 1;
            Heart2fill.fillAmount = 1;
            Heart3fill.fillAmount = 1;
        }
        else if (playerHealth.currentHealth == 2) {
            Heart1fill.fillAmount = 1;
            Heart2fill.fillAmount = 1;
            Heart3fill.fillAmount = 0;
        }
        else if (playerHealth.currentHealth == 1) {
            Heart1fill.fillAmount = 1;
            Heart2fill.fillAmount = 0;
            Heart3fill.fillAmount = 0;
        }
        else {
            Heart1fill.fillAmount = 0;
            Heart2fill.fillAmount = 0;
            Heart3fill.fillAmount = 0;
        }
    }
}
