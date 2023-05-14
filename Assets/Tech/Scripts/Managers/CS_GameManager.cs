using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class CS_GameManager : MonoBehaviour
{
    [Header("---Parameters---")]
    [SerializeField] private int _numberOfMurdersToStartPhase2;

    [Header("---References---")]
    [SerializeField] private CS_CamManager _camManager;

    [SerializeField] private CS_PostProcessChanges _postProcessChanges;
    [SerializeField] private CS_PostProcessChanges _postBlackNWhiteProcessChanges;
    [SerializeField] private CS_SoundManager _soundManager;
    [SerializeField] private CS_TimeManager _timeManager;
    [SerializeField] private CS_UIMurders _uIMurders;
    private CS_PlayerController _playerController;

    private int _numberOfMurders;
    public bool PartTwoBegin = false;

    public int NumberOfMureders => _numberOfMurders;

    private void Awake()
    {
        _playerController = FindObjectOfType<CS_PlayerController>();
    }

    public void TriggerEndGame()
    {
        Debug.Log("Fin du jeu lancée");
        Time.timeScale = 0;
    }

    public void TriggerPartTwo()
    {
        _camManager.StartGlobalShake();
        _postProcessChanges.StartChanges();
        _postBlackNWhiteProcessChanges.StartChanges();
        _soundManager.ChangeMusic();
        _timeManager.StartTransition();
        _playerController.SetAngry();
    }

    public void SetNumberOfMurders(int value)
    {
        _numberOfMurders = value;
        _uIMurders.UpdateUI(_numberOfMurders);

        if (_numberOfMurders == _numberOfMurdersToStartPhase2) TriggerPartTwo();
    }
}