using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_HealthBar : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private Slider _slider;
    
    //private 
    
    //getters
    public Slider Slider => _slider;

    public void SetMaxHealth(float health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }
    
    public void SetJauge(float _health)
    {
        _slider.value = _health;
    }
    
}
