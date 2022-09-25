using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    float _horizontalDirection;
    float _verticalDirection = 5f;
    Vector3 _lastMousePosition;
    Vector3 _firstMousePosition;

    Vector3 _mousePosition;
    public float HorizontalDirection { get => _horizontalDirection; private set => _horizontalDirection = value; }
    public float VerticalDirection { get => _verticalDirection; set => _verticalDirection = value; }
    public Vector3 MousePosition { get => _mousePosition; set => _mousePosition = value; }

    public void GetInputData() // Get Swerve Mechanic Input
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            VerticalDirection = 1f;
            _lastMousePosition = Input.mousePosition;
            HorizontalDirection = Input.GetAxis("Mouse X");
        }
        if (Input.GetMouseButtonUp(0))
        {
            _firstMousePosition = Vector3.zero;
            _lastMousePosition = Vector3.zero;
            VerticalDirection = 0;
            HorizontalDirection = 0;
        }
    }
}
