using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Bullet : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _lifeTime;
    private AnimationCurve _velocityCurve;

    private float _timer;

    public void OnCreated(Vector2 direction, float speed, float lifeTime, AnimationCurve velocityCurve)
    {
        _direction = direction;
        _speed = speed;
        _lifeTime = lifeTime;
        _velocityCurve = velocityCurve;
    }

    public void Update()
    {
        TimerTick();

        TryToDestroy();
    }

    public void FixedUpdate()
    {
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
        if (_timer == _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}