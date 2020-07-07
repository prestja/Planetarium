using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private CelestialBody sun;
    [SerializeField] private float distance = 30.0f;
    [SerializeField] private float pitch = 0.0f;
    [SerializeField] private float yaw = 0.0f;

    private void Start() {
        
    }

    private void Update() {
        distance -= Input.GetAxis("Mouse ScrollWheel") * 4.0f;
        if (Input.GetMouseButton(1)) {
            yaw += Input.GetAxis("Mouse X") * 4.0f;
            pitch += Input.GetAxis("Mouse Y") * 4.0f;
        }

        distance = Mathf.Clamp(distance, 30.0f, 300.0f);
        pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);
    }

    private void LateUpdate() {
        Vector3 fwd = Vector3.forward * distance;
        fwd = Quaternion.Euler(pitch, yaw, 0) * fwd;

        transform.position = fwd;
        transform.LookAt(Vector3.zero);
    }

    private void OnDrawGizmos() {
        Vector3 fwd = Vector3.forward * distance;
        fwd = Quaternion.Euler(pitch, yaw, 0) * fwd;
        Gizmos.DrawLine(fwd, Vector3.zero);
    }
}
