using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int m_currentHealth;

    public Slider healthSlider;

    public Text gameOverText;

    private void Start()
    {
        gameOverText.enabled = false;
        m_currentHealth = maxHealth;
    }

    /// <summary>
    /// Update health bar to show current health
    /// </summary>
    public void SetHealthbar()
    {
        healthSlider.value = m_currentHealth;
    }

    /// <summary>
    /// Update players health
    /// </summary>
    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        SetHealthbar();
        
        if (m_currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.enabled = true;
    }
}
