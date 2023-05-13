using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CamManager : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _shakeDuration, _shakeFactor;
    [SerializeField] private AnimationCurve _shakeCurve;

    private float _timer;
    private Coroutine _currentShakeCoroutine;
    private Vector3 _basePos;

    private void Start()
    {
        _basePos = _cam.transform.position;
    }

    public void Shake()
    {
        if (_currentShakeCoroutine != null) StopCoroutine(_currentShakeCoroutine);

        _currentShakeCoroutine = StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        _timer = 0;
        float factor;
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (_timer < _shakeDuration)
        {
            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, _shakeDuration);
            factor = _timer / _shakeDuration;
            float curved = 1 - _shakeCurve.Evaluate(factor);
            _cam.transform.position = _basePos + new Vector3(dir.x * curved, dir.y * curved, 0f) * _shakeFactor;

            yield return new WaitForEndOfFrame();
        }

        _cam.transform.position = _basePos;
        _currentShakeCoroutine = null;
    }
}