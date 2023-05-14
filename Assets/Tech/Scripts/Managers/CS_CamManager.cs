using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CamManager : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _shotgunShakeDuration, _shotgunShakeFactor;
    [SerializeField] private AnimationCurve _shotgunShakeCurve;
    [SerializeField] private AnimationCurve _shakeCurve;
    [SerializeField] private float _shakeDuration, _shakeMagnitude;

    private float _timer;
    private Coroutine _currentShakeCoroutine;
    private Vector3 _basePos;

    public void ShotgunShake()
    {
        if (_currentShakeCoroutine != null) StopCoroutine(_currentShakeCoroutine);

        _currentShakeCoroutine = StartCoroutine(ShtogunShaking());
    }

    private IEnumerator ShtogunShaking()
    {
        _timer = 0;
        float factor;
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        while (_timer < _shotgunShakeDuration)
        {
            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, _shotgunShakeDuration);
            factor = _timer / _shotgunShakeDuration;
            float curved = 1 - _shotgunShakeCurve.Evaluate(factor);
            _cam.transform.localPosition = new Vector3(dir.x * curved, dir.y * curved, -10f) * _shotgunShakeFactor;

            yield return new WaitForEndOfFrame();
        }

        Debug.Log("oui");
        _cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        _currentShakeCoroutine = null;
    }

    public void StartGlobalShake()
    {
        StartCoroutine(Shake(_shakeDuration, _shakeMagnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float timer = 0.0f;

        float currentMagnitude;

        while (timer < duration)
        {
            currentMagnitude = magnitude * _shakeCurve.Evaluate(timer / duration);

            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            _cam.transform.localPosition = new Vector3(x, y, -10f);

            timer = Mathf.Clamp(timer + Time.unscaledDeltaTime, 0f, duration);

            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _cam.transform.localPosition = new Vector3(x, y, -10f);

            yield return new WaitForEndOfFrame();
        }
    }
}