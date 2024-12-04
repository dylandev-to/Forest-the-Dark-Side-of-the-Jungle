using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private GameObject player;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UIManager.OnShowDeadScreen?.Invoke(true);
        }

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        UIManager.OnHealthSliderUpdate?.Invoke(currentHealth);
    }

    public void HideSkill()
    {
        player.SetActive(false);
    }

    public int Heal()
    {
        int healAmount = 5;
        return healAmount;
    }
}
