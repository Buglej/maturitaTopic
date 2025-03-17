using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private playerCombat health;
    [SerializeField] private Image healthBarTotal;
    [SerializeField] private Image healthBarCurrent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the health bar
        healthBarTotal.fillAmount = health.maxHealth / 10f;
        healthBarCurrent.fillAmount = health.currentHealth / 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the health bar based on the player's current health
        healthBarCurrent.fillAmount = health.currentHealth / 10f;
    }
}
