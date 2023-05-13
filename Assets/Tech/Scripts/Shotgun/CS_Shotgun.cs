using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Shotgun : MonoBehaviour
{
    #region Variables & Attributes

    [Header("---Parameters---")]
    [Header("Shoot")]
    [Range(1, 30)][SerializeField] private int _numberOfSpawnedBullet;

    [SerializeField] private float _shootDelay;
    [SerializeField] private float _spreadRangeWidth, _spreadRangeHeight;
    [SerializeField] private float _rangeRandomFactor;

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
    [SerializeField] private List<CS_Bullet> _pullableBullets;

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
            var angle = Vector2.SignedAngle(_currentDirection, Vector2.right);
            angle += ((float)i - (float)_numberOfSpawnedBullet / 2f) * _spreadRangeWidth / (float)_numberOfSpawnedBullet;
            angle += Random.Range(-1f, 1f) * _rangeRandomFactor;
            newDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(-angle * Mathf.Deg2Rad));

            float lifeTime = Random.Range(0.6f, 1f) * _bulletLifeTime;
            float speed = Random.Range(0.6f, 1f) * _bulletSpeed;

            SpawnBullet(i, newDir, speed, lifeTime, _bulletVelocityCurve);
        }

        Recoil();
    }

    private void SpawnBullet(int index, Vector2 direction, float speed, float lifeTime, AnimationCurve velocityCurve)
    {
        _pullableBullets[index].OnCreated(_bulletSpawn.position, direction, speed, lifeTime, velocityCurve);
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

    #region PullableBullets

    public void ClearPullableBullets()
    {
        _pullableBullets.Clear();
    }

    public void AddPullableBullet(CS_Bullet bullet)
    {
        _pullableBullets.Add(bullet);
    }

    #endregion PullableBullets
}