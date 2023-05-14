using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_LoadLevel : MonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;
    
    public void LoadLevel(int _sceneID)
    {
        _audioSource.Play();
        SceneManager.LoadScene(_sceneID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
