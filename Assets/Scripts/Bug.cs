using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{

    private Animator _animator;
    public float speed = 20f;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Transform _playerTransform;
    private Transform _transform;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _transform = gameObject.transform;
        _animator.SetFloat(Speed, speed);
        
        

    }

    void FixedUpdate()
    {
        Vector2 target = new Vector2(_playerTransform.position.x, _transform.position.y);
        transform.position = Vector2.MoveTowards(_transform.position, target, speed * Time.fixedDeltaTime);
        

    }
}