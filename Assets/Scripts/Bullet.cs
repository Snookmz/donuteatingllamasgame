using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Camera _mainCamera;
    private Bounds _bounds;
    private AudioSource _shootSound;

    private void Start()
    {
        _mainCamera = Camera.main;
        _bounds = _mainCamera.OrthographicBounds();
        _shootSound = GetComponent<AudioSource>();
        _shootSound.Play();

        // expand the bounds a little
        _bounds.max = _bounds.max + new Vector3(2, 2, 0);
        _bounds.max = _bounds.max - new Vector3(2, 2, 0);
    }

    private void FixedUpdate()
    {
        Vector3 bulletPosition = transform.position;
        if (bulletPosition.x > _bounds.max.x || bulletPosition.y  > _bounds.max.y ||
            bulletPosition.x < _bounds.min.x || bulletPosition.y < _bounds.min.y) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyEagle"))
        {
            Destroy(gameObject);
        }
    }
}