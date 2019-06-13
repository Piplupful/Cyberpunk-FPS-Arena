using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseX = 1, MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseX;

    // Minimum and Maximum vertical rotation as to stop infinite vertical rotation of the camera.
    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    // Horizontal and Vertical sensitivity.
    public float sensHorz = 10.0f;
    public float sensVert = 10.0f;

    // Vertical angle at start.
    public float rotationX = 0;

    void Update()
    {
        if (axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorz, 0);
        }
        else if (axes == RotationAxis.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensVert;

            //Clamps vertical rotation
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        }

        float rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
