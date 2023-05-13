using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour
{
    private bool _partTwoBegin = false;
    
    public void TriggerEndGame()
    {
        Debug.Log("Fin du jeu lancée");
        Time.timeScale = 0;
    }

    public void TriggerPartTwo()
    {
        _partTwoBegin = true;
    }
}
