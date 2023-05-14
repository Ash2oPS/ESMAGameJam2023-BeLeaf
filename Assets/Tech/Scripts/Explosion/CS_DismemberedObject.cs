using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CS_DismemberedObject : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _projectionDuration;
    private float _lifeTime;
    private float _projectionHeight;
    [SerializeField] private AnimationCurve _velocityCurve;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _basePos;

    public void OnCreated(Sprite sprite, Vector3 basePose, Vector2 dir, float speed, float projectionDuration, float lifetime, float projectionHeight)
    {
        _basePos = basePose;

        _spriteRenderer.sprite = sprite;

        _direction = dir;
        _speed = speed;
        _projectionDuration = projectionDuration;
        _lifeTime = lifetime;
        _projectionHeight = projectionHeight;

        _spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0f, 180f));

        StartCoroutine(Projection());
    }

    private IEnumerator Projection()
    {
        float timer = 0f;
        float factor;
        float scaledFactor;

        while (timer < _projectionDuration)
        {
            factor = timer / _projectionDuration;
            scaledFactor = _velocityCurve.Evaluate(factor);

            _spriteRenderer.transform.localPosition = new Vector2(0f, scaledFactor * _projectionHeight);
            transform.position = _basePos + new Vector3(_direction.x, _direction.y, 0f) * scaledFactor * _speed;

            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, _lifeTime);
        }

        _spriteRenderer.transform.localPosition = new Vector2(0f, 1f * _projectionHeight);
        transform.position = _basePos + new Vector3(_direction.x, _direction.y, 0f) * 1f * _speed;

        while (timer < _lifeTime)
        {
            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, _lifeTime);
        }

        Destroy(gameObject);
    }
}