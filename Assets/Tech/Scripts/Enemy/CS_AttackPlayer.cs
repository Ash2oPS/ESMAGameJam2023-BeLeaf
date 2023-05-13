using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AttackPlayer : MonoBehaviour
{
    [SerializeField] private CS_PlayerStats _playerStats;
    [SerializeField] private CS_EnemyStats _enemyStats;
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Play anim câlin
        DamagePlayer();
    }

    public void DamagePlayer()
    {
        Debug.Log("J'attaque le player");

        _playerStats.SetHP(_playerStats._playerHP -= _enemyStats._enemyDamage);

    }
}
