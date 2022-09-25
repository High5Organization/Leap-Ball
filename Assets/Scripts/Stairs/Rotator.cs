using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Rigidbody _rb;
    InputSystem InputSystem;
    [SerializeField] PlayerJumper playerJumper;
    private void Awake()
    {
        InputSystem = GetComponent<InputSystem>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputSystem.GetInputData();
        
        if (GameManager.Instance.GameState == GameStates.InGameStart)
        {
            if (playerJumper.comboCounter > 5)
            {
                transform.Rotate(Vector3.up * InputSystem.HorizontalDirection * 2);
            }
            else
            {
                transform.Rotate(Vector3.up * InputSystem.HorizontalDirection);
            }
            // _rb.angularVelocity = Vector3.up * InputSystem.HorizontalDirection*2;
        }
    }
}
