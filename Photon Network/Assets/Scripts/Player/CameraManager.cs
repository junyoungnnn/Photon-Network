using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraRotationLimit = 37.5f;
    [SerializeField] float currentRotationX;
    [SerializeField] float scrollSpeed = 100.0f;
    [SerializeField] float sensitivity = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRotation * sensitivity;

        currentRotationX -= cameraRotationX;

        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);

        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);

    }
}
