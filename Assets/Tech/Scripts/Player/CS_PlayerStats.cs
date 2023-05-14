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

    private CS_PlayerController _controller;

    //getters
    public float PlayerHP => _playerHP;

    //private
    private CS_GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<CS_GameManager>();
        _controller = FindObjectOfType<CS_PlayerController>();
    }

    private void Update()
    {
        DecreaseHPOverTime();
    }

    public void SetHP(float _newHP, bool isFromEnemy = false)
    {
        if (isFromEnemy)
        {
            if (_controller.IsInvincible) return;

            _controller.SetInvincible();
        }

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