using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AttackPlayer : MonoBehaviour
{
    [SerializeField] private CS_EnemyStats _enemyStats;
    private CS_PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = FindObjectOfType<CS_PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent<CS_PlayerController>(out CS_PlayerController oui)) return;

        //Play anim câlin
        DamagePlayer();
    }

    public void DamagePlayer()
    {
        _playerStats.SetHP(Mathf.Clamp(_playerStats.PlayerHP - _enemyStats.EnemyDamage, 0f, 1000f), true);
    }
}