using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AutoSpriteSorting : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private SpriteRenderer[] _spriteRenderers;

    private void Start()
    {
        StartCoroutine(AutoSorting());
    }

    private IEnumerator AutoSorting()
    {
        while (true)
        {
            Sorting();
            yield return new WaitForSeconds(0.05f);
        }
    }

    protected virtual void Sorting()
    {
        if (_spriteRenderers == null) return;

        foreach (SpriteRenderer sr in _spriteRenderers)
        {
            sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
        }
    }
}