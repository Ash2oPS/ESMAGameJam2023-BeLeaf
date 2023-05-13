using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CS_BulletPool : MonoBehaviour
{
    [SerializeField] private CS_Shotgun _shotgun;
    [SerializeField] private CS_Bullet _bulletPrefab;
    [SerializeField] private int _numberOfGeneratedBullets;
    [SerializeField][HideInInspector] private List<CS_Bullet> _pullableBullets;

    private void Awake()
    {
        AssignPullableBullets();
    }

    public void Generate()
    {
        _shotgun ??= FindObjectOfType<CS_Shotgun>();
        _pullableBullets ??= new List<CS_Bullet>();

        Clear();

        for (int i = 0; i < _numberOfGeneratedBullets; i++)
        {
            CS_Bullet bullet = Instantiate(_bulletPrefab, transform);
            bullet.transform.position = new Vector3(100f, 100f, 0f);
            _pullableBullets.Add(bullet);
        }
    }

    private void Clear()
    {
        _shotgun.ClearPullableBullets();

        foreach (CS_Bullet bullet in _pullableBullets)
        {
            DestroyImmediate(bullet.gameObject);
        }

        _pullableBullets.Clear();
    }

    private void AssignPullableBullets()
    {
        foreach (CS_Bullet bullet in _pullableBullets)
        {
            _shotgun.AddPullableBullet(bullet);
        }
    }
}

#region Editor

#if UNITY_EDITOR

[CustomEditor(typeof(CS_BulletPool))]
public class CSE_BulletPool : Editor
{
    private CS_BulletPool _bulletPool;

    private void OnEnable()
    {
        _bulletPool = target as CS_BulletPool;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            _bulletPool.Generate();
        }
    }
}

#endif //UNITY_EDITOR

#endregion Editor