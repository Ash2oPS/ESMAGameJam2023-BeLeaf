using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyManager : MonoBehaviour
{
    [Header("---Parameters---")]
    [SerializeField] private float _spawnDelay;

    [SerializeField] private float _regainedHP;

    [SerializeField] private AnimationCurve _spawnDelayCurve;

    [SerializeField] private CS_GameManager _gameManager;

    [Header("---Prefabs---")]
    [SerializeField] private CS_EnemyController _weakEnemyPrefab;

    [SerializeField] private CS_EnemyController _mediumEnemyPrefab, _strongEnemyPrefab;

    private CS_PlayerController _playerController;
    private CS_PlayerStats _playerStats;

    [SerializeField] private int _maxEnemyNumber;
    private int _currentEnemyNumber;

    private void Awake()
    {
        _playerController = FindObjectOfType<CS_PlayerController>();
        _playerStats = FindObjectOfType<CS_PlayerStats>();
    }

    private void Start()
    {
        StartCoroutine(SpawningEnemies());
    }

    private IEnumerator SpawningEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Clamp(_spawnDelayCurve.Evaluate((float)_gameManager.NumberOfMureders / 200), .02f, 1f) * _spawnDelay);
            SpawnEnemy(Random.Range(0, 4));
        }
    }

    private void SpawnEnemy(int typeOfEnemy)
    {
        if (_currentEnemyNumber >= _maxEnemyNumber) return;

        bool isVerticallyLocked = Random.Range(0, 2) == 1;

        float xPos, yPos;

        if (isVerticallyLocked)
        {
            xPos = Random.Range(-10f, 10f);
            yPos = Random.Range(0, 2) == 1 ? 8f : -8f;
        }
        else
        {
            xPos = Random.Range(0, 2) == 1 ? 10f : -10f;
            yPos = Random.Range(-8f, 8f);
        }

        CS_EnemyController enemyToSpawn = _weakEnemyPrefab;

        float playerX = _playerController.transform.position.x;
        float playerY = _playerController.transform.position.y;

        if (playerX + xPos < -7) xPos += 10f;
        else if (playerX + xPos > 25) xPos += -10f;

        if (playerY + yPos < -4) yPos += 8f;
        else if (playerY + yPos > 14) yPos += -8f;

        switch (typeOfEnemy)
        {
            case 0:
                enemyToSpawn = _weakEnemyPrefab;
                break;

            case 1:
                enemyToSpawn = _mediumEnemyPrefab;
                break;

            case 2:
                enemyToSpawn = _strongEnemyPrefab;
                break;
        }

        CS_EnemyController spawnedEnemy = Instantiate(enemyToSpawn, new Vector3(xPos, yPos, 0f), Quaternion.identity);

        spawnedEnemy.SetGameManager(_gameManager, this);

        _currentEnemyNumber++;
    }

    public void DecreaseNumberOfEnemies()
    {
        _currentEnemyNumber--;

        _playerStats.SetHP(_playerStats.PlayerHP + _regainedHP * Mathf.Clamp(_spawnDelayCurve.Evaluate((float)_gameManager.NumberOfMureders / 200), .02f, 1f) * _spawnDelay);
    }
}