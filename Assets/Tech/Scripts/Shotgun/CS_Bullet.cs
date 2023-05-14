using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CS_Bullet : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _lifeTime;
    private AnimationCurve _velocityCurve;

    private float _timer;

    private bool _isActive = false;

    #region Movement

    public void OnCreated(Vector2 position, Vector2 direction, float speed, float lifeTime, AnimationCurve velocityCurve)
    {
        _isActive = true;

        transform.position = position;

        _direction = direction;
        _speed = speed;
        _lifeTime = lifeTime;
        _velocityCurve = velocityCurve;

        _timer = 0;
    }

    public void Update()
    {
        TimerTick();

        TryToDestroy();
    }

    public void FixedUpdate()
    {
        if (!_isActive) return;

        Vector3 pos = new Vector3(_direction.x, _direction.y, 0f);

        float factor = _velocityCurve.Evaluate(_timer / _lifeTime);

        transform.position += pos * _speed * Time.deltaTime * factor;
    }

    private void TimerTick()
    {
        _timer = Mathf.Clamp(_timer += Time.deltaTime, 0f, _lifeTime);
    }

    private void TryToDestroy()
    {
        if (!_isActive) return;

        if (_timer == _lifeTime)
        {
            Disable();
        }
    }

    private void Disable()
    {
        _isActive = false;
        transform.position = new Vector3(100f, 100f, 0f);
    }

    #endregion Movement

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<CS_EnemyController>(out CS_EnemyController enemy)) return;

        enemy.Explode();
        
        Disable();
    }
}