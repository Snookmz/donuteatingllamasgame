﻿using UnityEditor.SceneManagement;
 using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera mainCamera;
    private Bounds _bounds;

    private Vector3 _randomPosition;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, 2);
        mainCamera = Camera.main;
        _bounds = mainCamera.OrthographicBounds();
        // enemyPrefab = gameObject.GetComponent<GameObject>();
    }

    void Spawn()
    {
        _bounds = mainCamera.OrthographicBounds();
        Instantiate(enemyPrefab, GenerateRandomVector3WithinBounds(_bounds), Quaternion.identity);
    }

    private Vector3 GenerateRandomVector3WithinBounds(Bounds b)
    {
        return new Vector3(b.max.x, Random.Range(-b.max.y + 1, b.max.y - 1), 0);
    }

}
