using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Camera _mainCamera;
    public Rigidbody2D PlayerRigidbody2D;
    public float speed = 10f;

    private void Start()
    {
        _mainCamera = Camera.main;
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Shoot();
        }
        
    }
    
    private void Shoot()
    {
        var mouseDirection = Input.mousePosition;
        mouseDirection.z = 0.0f;
        mouseDirection = _mainCamera.ScreenToWorldPoint(mouseDirection);
        mouseDirection = mouseDirection - transform.position;
        
        var bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));

        Rigidbody2D bulletInstanceRigidBody2d = bulletInstance.GetComponent<Rigidbody2D>();

        var xy = mouseDirection.x * mouseDirection.x + mouseDirection.y * mouseDirection.y;
        var sqrt = Math.Sqrt(xy);
        float x = Convert.ToSingle((mouseDirection.x) / sqrt);
        float y = Convert.ToSingle((mouseDirection.y) / sqrt);
        bulletInstanceRigidBody2d.velocity = new Vector2(x * speed, y * speed);
        PlayerRigidbody2D.velocity = new Vector2(-x * 5, -y );
        
        // recoil
        
       
    }
}
