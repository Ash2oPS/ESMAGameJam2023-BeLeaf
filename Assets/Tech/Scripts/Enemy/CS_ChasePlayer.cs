using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ChasePlayer : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private CS_Movement _movement;
    [SerializeField] private SpriteRenderer _sprite;
    
    //privates
    private CS_GameManager _gameManager;
    private Transform _target;
    private Vector2 _direction;
    private float _angle;

    private void Awake()
    {
        _target = FindObjectOfType<CS_PlayerController>().transform;
        _gameManager = FindObjectOfType<CS_GameManager>();
    }

    public void MoveToPlayer()
    {
        if (_target && !_gameManager.PartTwoBegin)
        {
            GoToPlayer();
        }

        else if (_target && _gameManager.PartTwoBegin)
        {
            AvoidPlayer();
        }
    }

    private void GoToPlayer()
    {
        _direction = (_target.position - transform.position).normalized;

        if (_direction.x < 0)
        {
            _sprite.flipX = true;
            //Debug.Log("JE VAIS A GAUCHE");
        }
        else
        {
            _sprite.flipX = false;
            //Debug.Log("JE VAIS A DROITE");
        }

        _movement.RegisterMove(_direction);
    }

    private void AvoidPlayer()
    {
        Debug.Log("l'IA Fuit sa race");
        _direction = (Vector2)transform.position - (Vector2)_target.position;
        _direction.Normalize();

        float distance = _movement.MovementSpeed * Time.deltaTime;

        transform.position = (Vector2)transform.position + _direction * distance;
    }
}