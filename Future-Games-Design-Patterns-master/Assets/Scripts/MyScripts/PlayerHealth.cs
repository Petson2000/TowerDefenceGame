using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider;

    public Text gameOverText;

    private void Start()
    {
        gameOverText.enabled = false;
        currentHealth = maxHealth;
    }

    public void SetHealthbar()
    {
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealthbar();
        
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.enabled = true;
    }
}
