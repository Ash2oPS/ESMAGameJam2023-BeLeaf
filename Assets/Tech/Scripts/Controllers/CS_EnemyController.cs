using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyController : MonoBehaviour
{
    [SerializeField] private CS_ChasePlayer _chasePlayer;
    [SerializeField] private CS_AttackPlayer _attackPlayer;

    private void Start()
    {
        
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        _chasePlayer.MoveToPlayer();
    }
}
