using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerStats : MonoBehaviour
{
    [Header("---Références---")]
    [SerializeField] private CS_HealthBar _healthBar;

    [Header("---Data---")]
    [SerializeField] private float _playerHP;
    [SerializeField] private float _coefficient;
   
    //getters
    public float PlayerHP => _playerHP;
    
    //private
    private CS_GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<CS_GameManager>();
    }

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
            _gameManager.TriggerEndGame();
        }
    }
    
    private void DecreaseHPOverTime()
    {
        SetHP(Mathf.Clamp(_playerHP - _coefficient * Time.deltaTime, 0f, 1000f));
    }
}
