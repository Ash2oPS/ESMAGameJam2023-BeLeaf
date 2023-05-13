using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerStats : MonoBehaviour
{
    [SerializeField] public float _playerHP;

    public void SetHP(float _newHP)
    {
        _playerHP = _newHP;

        if (_playerHP == 0)
        {
            Debug.Log("Joueur Dead");
        }
    }
}
