using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CS_EnemyController : MonoBehaviour
{
    [SerializeField] private CS_ChasePlayer _chasePlayer;
    [SerializeField] private CS_AttackPlayer _attackPlayer;
    [SerializeField] private CS_EnemyMovement _enemyMovement;
    private CS_GameManager _gameManager;

    private bool _isDead;

    public void SetGameManager(CS_GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void FixedUpdate()
    {
        _chasePlayer.MoveToPlayer();
    }

    public void Explode()
    {
        if (_isDead) return;

        _isDead = true;

        _gameManager.SetNumberOfMurders(_gameManager.NumberOfMureders + 1);

        _enemyMovement.StopBouncing();
        Destroy(gameObject);
    }
}