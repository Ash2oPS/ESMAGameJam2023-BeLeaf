using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_CorpseExplosion : MonoBehaviour
{
    [Header("---Data---")]
    [SerializeField] private float _fieldOfImpact;

    [SerializeField] private float _force;
    [SerializeField] private float _eplosionDuration;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private LayerMask LayerToHit;

    private void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _fieldOfImpact, LayerToHit);

        float timer = 0f;
        float factor;
        float currentForce;

        while (timer < _eplosionDuration)
        {
            factor = timer / _eplosionDuration;

            currentForce = _curve.Evaluate(factor) * _force;

            foreach (Collider2D obj in objects)
            {
                Vector2 direction = obj.transform.position - transform.position;

                obj.GetComponent<Rigidbody2D>().AddForce(direction * currentForce);
                Debug.Log("J'explose");
            }

            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, _eplosionDuration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fieldOfImpact);
    }
}