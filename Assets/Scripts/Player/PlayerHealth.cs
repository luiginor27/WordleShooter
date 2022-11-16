using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    private float _currentHealth;

    private void Start()
    {
        RestoreHealth();
    }

    public void Hit(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            GameManager.Instance.ResetGame();
            RestoreHealth();
        }
    }

    private void RestoreHealth()
    {
        _currentHealth = maxHealth;
    }
}
