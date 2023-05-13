using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerController : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private CS_Movement _movement;

    //public
    public CS_Movement Movement => _movement;

    private void Update()
    {
        CallMovement();
    }

    private void CallMovement()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _movement.RegisterMove(dir);
    }
}