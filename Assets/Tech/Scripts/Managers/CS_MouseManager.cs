using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MouseManager : MonoBehaviour
{
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;

    public Vector3 ScreenPosition => _screenPosition;
    public Vector3 WorldPosition => _worldPosition;

    private void Update()
    {
        _screenPosition = Input.mousePosition;
        _worldPosition = Camera.main.ScreenToWorldPoint(ScreenPosition);
    }
}