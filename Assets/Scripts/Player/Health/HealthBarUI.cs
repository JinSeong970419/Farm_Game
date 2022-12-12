using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    IHealth health;
    Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        if(health != null)
            health.onHealthChange += RefreshSlider;
    }
    private void OnDisable()
    {
        if (health != null)
            health.onHealthChange -= RefreshSlider;
    }

    private void Start()
    {
        health = GameManager.instance.Health as IHealth;
        health.onHealthChange += RefreshSlider;
        RefreshSlider();
    }

    void RefreshSlider()
    {
        healthSlider.value = health.HP;
    }
}
