using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        _lastCameraPosition = _cameraTransform.position;

    }
}
