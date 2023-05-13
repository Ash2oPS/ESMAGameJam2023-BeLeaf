using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyMovement : CS_Movement
{
    [SerializeField] private float _bounceHeight;
    [SerializeField] private float _delayBeforeBounce, _bounceDuration;

    [SerializeField] private AnimationCurve _bounceCurve;
    [SerializeField] private AnimationCurve _yScaleCurve;
    [SerializeField] private AnimationCurve _xScaleCurve;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Coroutine _currentMovementCoroutine, _currentBounceCoroutine;
    private float _bounceFactor;
    private float _bounceLoopTimer, _bounceTimer;
    private float _spriteBasePos;
    private bool _canJump;

    private void Start()
    {
        StartBouncing();
    }

    public void StartBouncing()
    {
        _spriteBasePos = _spriteRenderer.transform.localPosition.y;

        if (_currentMovementCoroutine != null) StopCoroutine(_currentMovementCoroutine);

        _canJump = true;
        _currentMovementCoroutine = StartCoroutine(BounceLooping());
    }

    public void StopBouncing()
    {
        if (_currentMovementCoroutine == null) return;

        StopCoroutine(_currentMovementCoroutine);
        _currentMovementCoroutine = null;

        if (_currentBounceCoroutine == null) return;

        StopCoroutine(_currentBounceCoroutine);
        _currentBounceCoroutine = null;
    }

    private void StartOneBounce(Vector2 direction)
    {
        if (_currentBounceCoroutine != null) StopCoroutine(_currentBounceCoroutine);

        _currentBounceCoroutine = StartCoroutine(Bounce(direction));
    }

    private void StopOneBounce()
    {
        if (_currentBounceCoroutine == null) return;

        StopCoroutine(_currentBounceCoroutine);
        _currentBounceCoroutine = null;
    }

    protected override void Move()
    {
        if (_currentDirection == Vector2.zero) return;

        transform.position += new Vector3(_currentDirection.x, _currentDirection.y, 0f) * _movementSpeed * Time.deltaTime * _bounceFactor;
    }

    #region Coroutines

    private IEnumerator BounceLooping()
    {
        while (true)
        {
            yield return new WaitUntil(() => _canJump);
            yield return new WaitForSeconds(_delayBeforeBounce);
            StartOneBounce(_currentDirection);
        }
    }

    private IEnumerator Bounce(Vector2 direction)
    {
        _bounceTimer = 0f;
        _canJump = false;

        while (_bounceTimer < _bounceDuration)
        {
            _bounceFactor = _bounceCurve.Evaluate(_bounceTimer / _bounceDuration);
            _spriteRenderer.transform.localScale = new Vector3(_xScaleCurve.Evaluate(_bounceTimer / _bounceDuration), _yScaleCurve.Evaluate(_bounceTimer / _bounceDuration), 1f);
            _spriteRenderer.transform.localPosition = new Vector3(0f, _spriteBasePos + _bounceFactor * _bounceHeight, 0f);
            yield return new WaitForEndOfFrame();
            _bounceTimer = Mathf.Clamp(_bounceTimer + Time.deltaTime, 0f, _bounceDuration);
        }

        _bounceFactor = 0f;
        _spriteRenderer.transform.localPosition = new Vector3(0f, _spriteBasePos + _bounceFactor * _bounceHeight, 0f);
        _spriteRenderer.transform.localScale = new Vector3(_xScaleCurve.Evaluate(_bounceTimer / _bounceDuration), _yScaleCurve.Evaluate(_bounceTimer / _bounceDuration), 1f);

        _canJump = true;

        _currentBounceCoroutine = null;
    }

    #endregion Coroutines
}