using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ChasePlayer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private CS_Movement _movement;
    [SerializeField] private SpriteRenderer _sprite;
    private Vector2 _direction;
    private float _angle;
    public void MoveToPlayer()
    {
        if (_target)
        {
            _direction = (_target.position - transform.position).normalized;

            if (_direction.x < 0)
            {
                _sprite.flipX = true;
                Debug.Log("JE VAIS A GAUCHE");
            }

            else
            {
                _sprite.flipX = false;
                Debug.Log("JE VAIS A DROITE");

            }
            
            _movement.RegisterMove(_direction);
        }
    }
    
}
