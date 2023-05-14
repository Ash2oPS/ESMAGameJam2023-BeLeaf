using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class CS_EnemyController : MonoBehaviour
{
    [SerializeField] private CS_ChasePlayer _chasePlayer;
    [SerializeField] private CS_AttackPlayer _attackPlayer;
    [SerializeField] private CS_EnemyMovement _enemyMovement;
    [SerializeField] private CS_DismemberedObject _dismemberedObjectPrefab;

    [SerializeField] private VisualEffect _demembrementVFX;
    [SerializeField] private Transform _sangVFX;
    [SerializeField] private Sprite[] _dismemberedSprites;

    private CS_GameManager _gameManager;
    private CS_EnemyManager _enemyManager;

    private bool _isDead;

    public void SetGameManager(CS_GameManager gameManager, CS_EnemyManager enemyManager)
    {
        _gameManager = gameManager;
        _enemyManager = enemyManager;
    }

    private void FixedUpdate()
    {
        _chasePlayer.MoveToPlayer();
    }

    public void Explode(Vector2 direction)
    {
        if (_isDead) return;

        _isDead = true;

        _gameManager.SetNumberOfMurders(_gameManager.NumberOfMureders + 1);

        _enemyMovement.StopBouncing();

        VisualEffect vfx = Instantiate(_demembrementVFX, transform.position, Quaternion.identity);
        Transform sangvfx = Instantiate(_sangVFX, transform.position, Quaternion.identity);
        sangvfx.right = direction;

        _enemyManager.DecreaseNumberOfEnemies();

        //for (int i = 0; i < _dismemberedSprites.Length; i++)
        //{
        //    CS_DismemberedObject disO = Instantiate(_dismemberedObjectPrefab, transform.position, Quaternion.identity);

        //    Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        //    float speed = Random.Range(2f, 4f);
        //    float projDur = Random.Range(0.2f, 0.5f);
        //    float lifeTime = Random.Range(7f, 10f);
        //    float projHeight = Random.Range(0.2f, 0.5f);

        //    disO.OnCreated(_dismemberedSprites[i], transform.position, dir, speed, projDur, lifeTime, projHeight);
        //}

        Destroy(gameObject);
    }
}