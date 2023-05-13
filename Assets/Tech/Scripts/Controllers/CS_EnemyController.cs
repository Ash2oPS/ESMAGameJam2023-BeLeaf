using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyController : MonoBehaviour
{

    [SerializeField] private CS_Movement _movement;
    [SerializeField] private CS_ChasePlayer _chasePlayer;

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
