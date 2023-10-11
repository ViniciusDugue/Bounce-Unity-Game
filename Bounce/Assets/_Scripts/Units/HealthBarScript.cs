using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Slider _healthSlider;
    [SerializeField] private Basic_Enemy enemy;
    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        // enemy = transform.parent.GetComponent<Basic_Enemy>();
        SetMaxHealth(enemy.maxHealth);
    }
    void Update()
    {
        SetHealth(enemy.health);
    }
    public void SetMaxHealth(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        _healthSlider.value = health;
    }
}