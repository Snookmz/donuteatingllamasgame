using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Robot : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;	
    [SerializeField] private float runSpeed = 20f;
    private float _horizontalMove;
    private Animator _animator;
    private Rigidbody2D _robotRigidBody;
    private Vector3 _mVelocity = Vector3.zero;
    private Bounds _bounds;
    private Camera _mainCamera;
    private GameObject[] prefabs = new GameObject[2];
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private Vector3 _currentPosition;
    private int wayPointIndex = 0;

    public Transform[] wayPoints;
    public TrailRenderer robotTrail;
    public float minRespawn = 1f;
    public float maxRespawn = 3f;
    public float jumpForce = 400f;
    public GameObject orcPrefab;
    public GameObject coinPrefab;
    
    void Start()
    {

        prefabs[0] = orcPrefab;
        prefabs[1] = coinPrefab;
        _currentPosition = transform.position;
        
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetFloat(Speed, runSpeed);
        _robotRigidBody = gameObject.GetComponent<Rigidbody2D>();
        // robotTrail = gameObject.GetComponent<TrailRenderer>();
        // robotTrail.enabled = true;

        Instantiate(robotTrail, transform);       
        // InvokeRepeating("LaunchOrc", (float)(Random.Range(minRespawn, maxRespawn)), (float)(Random.Range(minRespawn, maxRespawn)));
        InvokeRepeating("LaunchPrefab", (Random.Range(minRespawn, maxRespawn)), (Random.Range(minRespawn, maxRespawn)));

    }

    private void Update()
    {
        Move();
    }

    void FixedUpdate()
    {

    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].transform.position,
            runSpeed * Time.deltaTime);

        if (transform.position.x == wayPoints[wayPointIndex].transform.position.x)
        {
            if (wayPoints.Length > wayPointIndex + 1)
            {
                wayPointIndex += 1;
            }
        }
    }
    
    private void OldMove()
    {
        _robotRigidBody.MovePosition((Vector2)transform.position + (new Vector2(runSpeed * Time.deltaTime, 0)));
 
        if (gameObject.transform.position.x % 15 < 1)
        {
            _robotRigidBody.AddForce(new Vector2(0f, jumpForce));
            _animator.SetBool(IsJumping, true);
            // Debug.Log("Robot, robotTrail enabled: " + robotTrail.enabled );
            robotTrail.enabled = false;
            // Debug.Log("Robot, robotTrail enabled after: " + robotTrail.enabled );
        }
        else
        {
            if (!robotTrail.enabled)
            {
                robotTrail.enabled = true;
            }
            _animator.SetBool(IsJumping, false);
        }       
    }

    private void LaunchPrefab()
    {
        _currentPosition = gameObject.transform.position;
        var r = Random.Range(0, 2);
        Instantiate(prefabs[r], _currentPosition, Quaternion.identity);       
    }
    
    private void LaunchOrc()
    {
        var currentPosition = gameObject.transform.position;
        Instantiate(orcPrefab, currentPosition, Quaternion.identity);
    }
}