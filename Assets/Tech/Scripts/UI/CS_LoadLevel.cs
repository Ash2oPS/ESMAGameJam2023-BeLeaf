using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_LoadLevel : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _transition;
    private void Start()
    {
    }

    public void LoadLevel(int _sceneID)
    {
        _audioSource.Play();
       StartCoroutine(LoadLevelTrigger(_sceneID));
    }

    IEnumerator LoadLevelTrigger(int levelIndex)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
