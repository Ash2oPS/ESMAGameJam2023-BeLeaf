using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyController : MonoBehaviour
{
    [SerializeField] private CS_ChasePlayer _chasePlayer;
    [SerializeField] private CS_AttackPlayer _attackPlayer;
    [SerializeField] private CS_EnemyMovement _enemyMovement;

    private void FixedUpdate()
    {
        _chasePlayer.MoveToPlayer();
    }

    public void Explode()
    {
        _enemyMovement.StopBouncing();
        Destroy(gameObject);
    }
}