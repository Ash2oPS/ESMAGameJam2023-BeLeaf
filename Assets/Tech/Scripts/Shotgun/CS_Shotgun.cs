using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Shotgun : MonoBehaviour
{
    #region Variables & Attributes

    [Header("---Parameters---")]
    [Header("Shoot")]
    [SerializeField] private int _numberOfSpawnedBullet;

    [SerializeField] private float _shootDelay;
    [Range(0f, 1f)][SerializeField] private float _spreadRangeWidth, _spreadRangeHeight;

    [Header("Bullets")]
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private AnimationCurve _bulletVelocityCurve;

    [Header("Recoil")]
    [SerializeField] private AnimationCurve _recoilCurve;

    [SerializeField] private float _recoilPower;
    [SerializeField] private float _recoilDuration;

    [Header("---References---")]
    [SerializeField] private Transform _pivot;

    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] private CS_PlayerController _playerController;
    [SerializeField] private CS_Movement _movement;
    [SerializeField] private CS_SoundManager _soundManager;
    private AudioSource _audioSource;

    [Header("---Prefabs---")]
    [SerializeField] private CS_Bullet _bulletPrefab;

    //private

    private float _timer;
    private bool _canShoot => _timer == 0f;
    private CS_MouseManager _mouseManager;
    private Vector2 _currentDirection;

    #endregion Variables & Attributes

    private void Awake()
    {
        _mouseManager = FindObjectOfType<CS_MouseManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        SetCurrentDirection();
        PivotRotation();
        TimerTick();
    }

    #region Methods

    private void SetCurrentDirection()
    {
        Vector3 dir = (_mouseManager.WorldPosition - _playerController.transform.position);
        _currentDirection = new Vector2(dir.x, dir.y).normalized;
        //Debug.Log(_mouseManager.WorldPosition);
    }

    private void PivotRotation()
    {
        _pivot.right = -_currentDirection;
    }

    public void TryShooting()
    {
        if (!_canShoot) return;

        Shoot();
    }

    #endregion Methods

    #region Timer

    private void TimerReset()
    {
        _timer = _shootDelay;
    }

    private void TimerTick()
    {
        if (_timer == 0f) return;

        _timer = Mathf.Clamp(_timer - Time.deltaTime, 0f, _shootDelay);
    }

    #endregion Timer

    #region Shoot

    private void Shoot()
    {
        TimerReset();

        Vector2 newDir;
        float factor;

        for (int i = 0; i < _numberOfSpawnedBullet; i++)
        {
            //            newDir = new Vector2(_currentDirection.x * factor, _currentDirection.y * factor).normalized;
            SpawnBullet(_currentDirection, _bulletSpeed, _bulletLifeTime, _bulletVelocityCurve);
        }

        Recoil();
        _soundManager.PlaySound(_audioSource);
    }

    private void SpawnBullet(Vector2 direction, float speed, float lifeTime, AnimationCurve velocityCurve)
    {
        CS_Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.identity);
        bullet.OnCreated(direction, speed, lifeTime, velocityCurve);
    }

    #endregion Shoot

    #region Recoil

    private void Recoil()
    {
        StartCoroutine(RecoilCoroutine(_currentDirection * -1f));
    }

    private IEnumerator RecoilCoroutine(Vector2 dir)
    {
        _playerController.SetCanMove(false);
        float recoilTimer = 0f;
        float factor;

        Debug.Log(dir);

        while (recoilTimer < _recoilDuration)
        {
            factor = _recoilCurve.Evaluate(recoilTimer / _recoilDuration);
            _movement.RegisterMove(dir, factor * _recoilPower);
            yield return new WaitForEndOfFrame();
            recoilTimer = Mathf.Clamp(recoilTimer + Time.deltaTime, 0f, 1f);
        }

        _playerController.SetCanMove(true);
    }

    #endregion Recoil
}