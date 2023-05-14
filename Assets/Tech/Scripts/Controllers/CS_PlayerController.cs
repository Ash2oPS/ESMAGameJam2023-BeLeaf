using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CS_PlayerController : MonoBehaviour
{
    [Header("---Angry Stats---")]
    [SerializeField] private float _angryMovementSpeed;

    [SerializeField] private float _angryShootDelay;
    [SerializeField] private float _angrySpreadWidth;
    [SerializeField] private int _angryNumberOfShotBullets;

    [Header("---Other Stats---")]
    [SerializeField] private float _invincibilityDuration;

    [Header("---References---")]
    [SerializeField] private CS_Movement _movement;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private CS_Shotgun _shotgun;

    //private

    private bool _canMove = true;
    private bool _isInvincible;

    //public
    public bool CanMove => _canMove;

    public CS_Movement Movement => _movement;

    public CS_Shotgun Shotgun => _shotgun;
    public bool IsInvincible => _isInvincible;

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

    public void SetAngry()
    {
        _movement.SetMovementSpeed(_angryMovementSpeed);
        _shotgun.SetValues(_angryShootDelay, _angrySpreadWidth, _angryNumberOfShotBullets);
    }

    public void SetInvincible()
    {
        StartCoroutine(BeingInvincible());
    }

    private IEnumerator BeingInvincible()
    {
        _isInvincible = true;
        float timer = 0f;

        int index = 0;
        bool isOpac = false;

        SetOpacity(0.25f);

        while (timer < _invincibilityDuration)
        {
            if (index % 20 == 0)
            {
                isOpac = !isOpac;
                float op = isOpac ? 1f : 0.25f;
                SetOpacity(op);
            }
            yield return new WaitForEndOfFrame();
            timer = Mathf.Clamp(timer + Time.deltaTime, 0f, _invincibilityDuration);
            index++;
        }

        SetOpacity(1f);
        _isInvincible = false;
    }

    private void SetOpacity(float value)
    {
        Debug.Log("oui");
        _spriteRenderer.color = new Color(1f, 1f, 1f, value);
    }
}