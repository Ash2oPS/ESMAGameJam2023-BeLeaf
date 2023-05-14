using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_SoundManager : MonoBehaviour
{
    private CS_GameManager _gameManager;

    [SerializeField] private AudioSource _piste1, _piste2;

    private void Awake()
    {
        _gameManager = FindObjectOfType<CS_GameManager>();
    }

    public void PlaySound(AudioSource _pisteAudio)
    {
        _pisteAudio.Play();
    }

    public void ChangeMusic()
    {
        _piste1.enabled = false;
        _piste2.enabled = true;
    }
}