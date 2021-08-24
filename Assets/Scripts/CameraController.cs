using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    
    private static readonly float cameraRotationSpeed = 10.0f;
    
    private float verticalCameraInput;
    
    void Update()
    {
        GatherInput();
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    private void GatherInput()
    {
        verticalCameraInput = Input.GetAxis("Mouse X");
    }

    private void UpdateCamera()
    {
        transform.position = playerGameObject.transform.position;
        transform.Rotate(Vector3.up * verticalCameraInput * cameraRotationSpeed);
    }
}
