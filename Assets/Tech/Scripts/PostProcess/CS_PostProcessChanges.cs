using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CS_PostProcessChanges : MonoBehaviour
{
    [Header("---Parameters---")]
    [SerializeField] private float _transitionDuration;

    [SerializeField] private float _currentWeight;
    [SerializeField] private AnimationCurve _curve;

    //privates
    private Volume _postProcessVolume;

    private CS_GameManager _gameManager;

    //getters
    public Volume PostProcessVolume => _postProcessVolume;

    private void Awake()
    {
        _postProcessVolume = GetComponent<Volume>();
        _gameManager = FindObjectOfType<CS_GameManager>();
    }

    public void StartChanges()
    {
        StartCoroutine(PostProcessChange());
    }

    public IEnumerator PostProcessChange()
    {
        float timer = 0f;
        float factor;

        while (timer < _transitionDuration)
        {
            factor = timer / _transitionDuration;
            _currentWeight = _curve.Evaluate(factor);
            _postProcessVolume.weight = _currentWeight;

            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, _transitionDuration);
        }

        _postProcessVolume.weight = 1f;
    }
}