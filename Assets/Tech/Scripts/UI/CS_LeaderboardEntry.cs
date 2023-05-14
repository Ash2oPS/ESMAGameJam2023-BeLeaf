using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CS_LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankTMP;
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _numberTMP;

    public void OnCreated(int rank, string name, int number)
    {
        _rankTMP.text = "# " + rank.ToString("000");
        _nameTMP.text = name;
        _numberTMP.text = number.ToString("000") + " Victims";
    }
}