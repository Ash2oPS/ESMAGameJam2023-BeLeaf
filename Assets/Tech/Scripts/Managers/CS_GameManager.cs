using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CS_GameManager : MonoBehaviour
{
    public bool PartTwoBegin = false;
    
    public void TriggerEndGame()
    {
        Debug.Log("Fin du jeu lancée");
        Time.timeScale = 0;
    }

    public void TriggerPartTwo()
    {
        PartTwoBegin = true;
    }
}
