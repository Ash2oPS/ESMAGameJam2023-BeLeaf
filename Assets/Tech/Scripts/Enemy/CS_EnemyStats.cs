using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyStats : MonoBehaviour
{
    [SerializeField] private float _enemyHP;
    [SerializeField] private float _enemyDamage;

    public float EnemyHP => _enemyHP;
    public float EnemyDamage => _enemyDamage;
}
