using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class CS_GameManager : MonoBehaviour
{
    public bool PartTwoBegin = false;
    [SerializeField] private CS_CamManager _camManager;
    [SerializeField] private CS_PostProcessChanges _postProcessChanges;

    public void TriggerEndGame()
    {
        Debug.Log("Fin du jeu lancée");
        Time.timeScale = 0;
    }

    private void Start()
    {
        StartCoroutine(oui());
    }

    private IEnumerator oui()
    {
        yield return new WaitForSeconds(3f);
        TriggerPartTwo();
    }

    public void TriggerPartTwo()
    {
        _camManager.StartGlobalShake();
        _postProcessChanges.StartChanges();
    }
}