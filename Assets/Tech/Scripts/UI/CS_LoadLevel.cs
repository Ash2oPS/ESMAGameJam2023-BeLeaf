using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_LoadLevel : MonoBehaviour
{

    public void LoadLevel(int _sceneID)
    {
        SceneManager.LoadScene(_sceneID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
