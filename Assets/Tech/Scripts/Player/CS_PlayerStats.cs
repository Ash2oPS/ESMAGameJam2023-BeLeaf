using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerStats : MonoBehaviour
{
    [SerializeField] private float _playerHP;
    [SerializeField] private CS_HealthBar _healthBar;
    [SerializeField] private float _coefficient;
    public float PlayerHP => _playerHP;

    private void Update()
    {
        DecreaseHPOverTime();
    }

    public void SetHP(float _newHP)
    {
        _playerHP = _newHP;
        _healthBar.SetJauge(_newHP);
        if (_playerHP == 0)
        {
            Debug.Log("Joueur Dead");
        }
    }
    
    private void DecreaseHPOverTime()
    {
        SetHP(_playerHP - _coefficient * Time.deltaTime);
    }
}
