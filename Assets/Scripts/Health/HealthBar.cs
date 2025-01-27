using Microsoft.Unity.VisualStudio.Editor;
using Image = UnityEngine.UI.Image;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image Heart1fill;
    [SerializeField] private Image Heart2fill;
    [SerializeField] private Image Heart3fill;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Heart1fill.fillAmount = 1;
        Heart2fill.fillAmount = 1;
        Heart3fill.fillAmount = 1;
    }

    // Update is called once per frame
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
