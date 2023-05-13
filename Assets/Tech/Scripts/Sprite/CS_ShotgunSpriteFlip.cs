using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ShotgunSpriteFlip : MonoBehaviour
{
    [SerializeField] private Transform _shotgun;

    private float angle;
    private bool _isFlipped;
    private bool _hasToFlip => angle > 90f && angle < 270f;
    private bool _hasToUnflip => angle < 90f || angle > 270f;

    private void Update()
    {
        TryFlip();
    }

    private void TryFlip()
    {
        angle = Mathf.Abs(transform.localEulerAngles.z);

        if (!_isFlipped && _hasToFlip) Flip(true);
        else if (_isFlipped && _hasToUnflip) Flip(false);
    }

    private void Flip(bool value)
    {
        float scaleY = value ? -1f : 1f;
        _shotgun.localScale = new Vector3(1f, scaleY, 1f);

        _isFlipped = value;
    }
}