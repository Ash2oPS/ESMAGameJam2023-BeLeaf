using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CS_TimeManager : MonoBehaviour
{
    [SerializeField] private float _transitionDuration;
    [SerializeField] private AnimationCurve _transitionCurve;
    private float _currentTimeFactor;

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        float timer = 0f;
        float factor;
        float value;

        while (timer < _transitionDuration)
        {
            factor = timer / _transitionDuration;
            value = _transitionCurve.Evaluate(factor);

            Time.timeScale = Mathf.Clamp(value, 0f, 1f);

            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.unscaledDeltaTime, 0f, _transitionDuration);
        }
    }
}