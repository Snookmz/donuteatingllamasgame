using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D _enemyRigidBody;
    public Collider2D EnemyCollider;
    public float arcLength = 2f;
    public float leftSpeed = -2f;
    public float ySpeed = 20f;
    public float deathDelay = 0.2f;
    public Animator animator;

    private AudioSource _deathSound;
    private float _enemyArcTop;
    private float _enemyArcBottom;
    private Vector3 _initialPosition;
    private Vector3 _currentPosition;
    private float _previousYSpeed;
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private Camera _mainCamera;
    private Bounds _bounds;

    void Start()
    {
        _mainCamera = Camera.main;
        _bounds = _mainCamera.OrthographicBounds();
        EnemyCollider = GetComponent<Collider2D>();
        _enemyRigidBody = GetComponent<Rigidbody2D>();
        _initialPosition = _enemyRigidBody.transform.position;
        _enemyArcTop = _initialPosition.y + arcLength;
        _enemyArcBottom = _initialPosition.y - arcLength;
        _previousYSpeed = ySpeed;
        animator = gameObject.GetComponent<Animator>();
        _deathSound = GetComponent<AudioSource>();

        _bounds.min = _bounds.min - new Vector3(2, 0, 0);

    }

    private void FixedUpdate()
    {
        if (!_enemyRigidBody.Equals(null)) 
        {
            _currentPosition = _enemyRigidBody.transform.position;
            _enemyRigidBody.velocity = GenerateNewPosition();

            if (_currentPosition.x < _bounds.min.x)
            {
                Destroy(gameObject);
            }
        }

    }

    private Vector2 GenerateNewPosition()
    {
        Vector2 newVelocity = new Vector2();
        if (_currentPosition.y >= _enemyArcTop)
        {
            newVelocity = new Vector2(leftSpeed, -ySpeed);
            _previousYSpeed = -ySpeed;

        } else if (_currentPosition.y <= _enemyArcBottom)
        {
            newVelocity = new Vector2(leftSpeed, ySpeed);
            _previousYSpeed = ySpeed;
        } else if (animator.GetBool(IsDead))
        {
            _enemyRigidBody.velocity = Vector2.zero;
        }
        else
        {
            newVelocity = new Vector2(leftSpeed, _previousYSpeed);
        }
        
        // var newVelocity = new Vector2(_leftSpeed, 0.2f);
        return newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag($"PlayerLegs"))
        {
            PerformDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag($"Ground"))
        {
            _enemyRigidBody.velocity = new Vector2(leftSpeed, ySpeed);
            _previousYSpeed = ySpeed;
        } else if (collision.gameObject.CompareTag($"PlayerLegs"))
        {
            PerformDeath();
        }
    }

    private void PerformDeath()
    {
        // Debug.Log("Enemy, PerformDeath");
        animator.SetBool(IsDead, true);
        _deathSound.Play();
        
        Destroy(_enemyRigidBody);
        Destroy(EnemyCollider);
        StartCoroutine(DestroyAfterDelay(deathDelay));
    }
    
    private IEnumerator DestroyAfterDelay (float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        Destroy(animator);
    }
    
}