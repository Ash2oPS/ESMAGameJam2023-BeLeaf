using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CS_Destruction : MonoBehaviour
{
    [SerializeField] private float delay;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(oui());
    }

    private IEnumerator oui()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}