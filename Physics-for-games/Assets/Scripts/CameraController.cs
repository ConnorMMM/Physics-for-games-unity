using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target = null;
    public float speed = 180;
    public float minDistance = 2;
    public float maxDistance = 10;
    public float relaxSpeed = 5;
    public float zoomSpeed = 4;
    public float heightOffset = 1.5f;

    private float _setDistance;
    private float _currentDistance;
    

    void Awake()
    {
        _setDistance = minDistance + ((maxDistance - minDistance) * .5f);
        _currentDistance = _setDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // Right drag rotates the camera
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            float dx = -Input.GetAxis("Mouse Y");
            float dy = Input.GetAxis("Mouse X");
            // look up and down by rotating around X-axis
            angles.x = Mathf.Clamp(angles.x + dx * speed * Time.deltaTime, 0, 70);
            // spin the camera round
            angles.y += dy * speed * Time.deltaTime;
            transform.eulerAngles = angles;
        }

        // Zoom in/out with mouse wheel
        _setDistance = Mathf.Clamp(_setDistance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minDistance, maxDistance);

        RaycastHit hit;
        if(Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, _setDistance))
        {
            // Snap the camera right in to where the collision happened
            _currentDistance = hit.distance;
        }
        else
        {
            // Relax the camera back to the desired distance
            _currentDistance = Mathf.MoveTowards(_currentDistance, _setDistance, Time.deltaTime * relaxSpeed);
        }

        // look at the target point
        transform.position = GetTargetPosition() - _currentDistance * transform.forward;
    }

    private Vector3 GetTargetPosition()
    {
        return target.position + heightOffset * Vector3.up;
    }
}
