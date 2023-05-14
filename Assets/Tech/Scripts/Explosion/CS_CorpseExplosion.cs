using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CorpseExplosion : MonoBehaviour
{
    [Header("---Data---")]
    [SerializeField] private float _fieldOfImpact;
    [SerializeField] private float _force;
    [SerializeField] private LayerMask LayerToHit;

    private void Update()
    {
        
    }

    private void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _fieldOfImpact, LayerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            
            obj.GetComponent<Rigidbody2D>().AddForce(direction * _force);
            Debug.Log("J'explose");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fieldOfImpact);
    }
}
