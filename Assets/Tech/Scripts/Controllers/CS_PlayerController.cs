using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerController : MonoBehaviour
{
    [Header("---References---")]
    [SerializeField] private CS_Movement _movement;

    [SerializeField] private CS_Shotgun _shotgun;

    //private

    private bool _canMove = true;

    //public
    public bool CanMove => _canMove;

    public CS_Movement Movement => _movement;

    public CS_Shotgun Shotgun => _shotgun;

    private void Update()
    {
        CallMovement();

        GetKeyDownInputs();
    }

    private void CallMovement()
    {
        if (!_canMove) return;

        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _movement.RegisterMove(dir);
    }

    private void GetKeyDownInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) _shotgun.TryShooting();
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }
}