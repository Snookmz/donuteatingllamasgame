using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    private Transform _transform;
    private float _shakeDuration = 0f;
    private float _shakeMagnitude = 0.7f;
    private float _dampingSpeed = 1.0f;
    Vector3 initialPosition;

    private void Awake()
    {
        if (_transform == null)
        {
            _transform = GetComponent<Transform>();
        }
    }

    private void OnEnable()
    {
        initialPosition = _transform.localPosition;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            _transform.localPosition = initialPosition + Random.insideUnitSphere * _shakeMagnitude;
            _shakeDuration = Time.deltaTime * _dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
        }
    }
}
