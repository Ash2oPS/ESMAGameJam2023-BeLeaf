using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AutoSpriteSorting : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        StartCoroutine(AutoSorting());
    }

    private IEnumerator AutoSorting()
    {
        while (true)
        {
            Sorting();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Sorting()
    {
        if (_spriteRenderer == null) return;

        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }
}