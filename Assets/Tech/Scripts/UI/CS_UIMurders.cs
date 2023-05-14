using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CS_UIMurders : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfMurdersTMP;

    public void UpdateUI(int numberOfMurders)
    {
        _numberOfMurdersTMP.text = numberOfMurders.ToString();
    }
}