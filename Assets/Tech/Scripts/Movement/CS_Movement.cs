using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Movement : MonoBehaviour
{
    [Header("---Parameters---")]
    [SerializeField] private float _movementSpeed;

    //private

    private Vector2 _currentDirection;
    private bool _canMove;

    //public

    public float MovementSpeed => _movementSpeed;

    public void RegisterMove(Vector2 direction, float factor = 1f)
    {
        if (direction.magnitude > 1f) direction = direction.normalized;

        _currentDirection = direction * factor;
    }

    private void FixedUpdate()
    {
        if (_currentDirection == Vector2.zero) return;

        transform.position += new Vector3(_currentDirection.x, _currentDirection.y, 0f) * _movementSpeed * Time.deltaTime;
    }
}