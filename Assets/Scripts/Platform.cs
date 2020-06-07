using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    private bool _shake;
    private bool _fallen;

    public Rigidbody2D rb;
    public float platformFallWait = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MakePlatformFall());
        }
    }

    private void Update()
    {
        if (_shake && !_fallen)
        {
            var _currentPosition = transform.position;
            var rand = Random.insideUnitCircle * 0.02f;
            transform.position = _currentPosition + new Vector3(0, rand.y, 0);
            // transform.position = _currentPosition + new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
            // var x = _currentPosition.x * Mathf.Sin(Time.time * 1) * 2;
            // var x = _currentPosition.x + 0.1f;
            // transform.position = new Vector3(x, _currentPosition.y, _currentPosition.z);
        }
    }

    private IEnumerator MakePlatformFall()
    {
        _shake = true;
        yield return new WaitForSeconds(platformFallWait);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        // rb.gravityScale = 1f;
        _shake = false;
        _fallen = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}