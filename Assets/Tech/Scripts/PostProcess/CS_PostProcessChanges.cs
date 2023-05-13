using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class CS_PostProcessChanges : MonoBehaviour
{
     [Header("---Data---")]
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private float _currentWeight;

    
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

    private void Update()
    {
        if (_gameManager.PartTwoBegin)
        {
            PostProcessChange();
        }
    }

    public void PostProcessChange()
    {
        _currentWeight = Mathf.Lerp(_currentWeight, 1.0f, _transitionSpeed * Time.deltaTime);
        _postProcessVolume.weight = _currentWeight;
    }
}
