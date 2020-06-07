using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{

    private Rigidbody2D _orcRigidBody;
    private Collider2D _orcCollider;
    private Camera _mainCamera;
    private Bounds _bounds;
    private Animator _animator;
    [SerializeField] public float speed = 20f;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsKilling = Animator.StringToHash("IsKilling");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private AudioSource _deathSound;
    private Vector3 _currentPosition;
    public float deathDelay = 0.2f;
    public GameObject particlePrefab;
    private Transform _playerTransform;
    private bool _isFlipped;


    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _mainCamera = Camera.main;
        _bounds = _mainCamera.OrthographicBounds();
        _orcCollider = GetComponent<Collider2D>();
        _orcRigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Speed, speed);
        _deathSound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
        // transform.position = transform.position + new Vector3(_direction * (speed * Time.deltaTime), 0f, 0f);
        
        if (gameObject != null && _orcRigidBody != null)
        {
            Vector2 target = new Vector2(_playerTransform.position.x, _orcRigidBody.position.y);
            transform.position = Vector2.MoveTowards(_orcRigidBody.position, target, speed * Time.fixedDeltaTime);
            LookAtPlayer();
  
            _currentPosition = _orcRigidBody.transform.position;
            if (_currentPosition.x < _bounds.min.x)
            {
                Destroy(gameObject);
            }               
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.otherCollider.CompareTag("EnemyHead"))
        {
            PerformDeath();
        } else if (other.gameObject.CompareTag("Player") && other.otherCollider.CompareTag("EnemyBody"))
        {
            _animator.SetBool(IsKilling, true);
        }
    }
    
    private void PerformDeath()
    {
        // Debug.Log("Enemy, PerformDeath");
        _animator.SetBool(IsDead, true);
        _deathSound.Play();

        // var explosion = Instantiate(particlePrefab, gameObject.transform);
        var explosion = Instantiate(particlePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        
        explosion.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(_orcRigidBody);
        Destroy(_orcCollider);
        StartCoroutine(DestroyAfterDelay(gameObject, deathDelay));
        StartCoroutine(DestroyAfterDelay(explosion, deathDelay + 2f));
    }
        
    private IEnumerator DestroyAfterDelay (GameObject ob, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(ob);
        // Destroy(_animator);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > _playerTransform.position.x && _isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < _playerTransform.position.x && !_isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }

}