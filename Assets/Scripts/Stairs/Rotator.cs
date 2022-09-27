using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    InputSystem InputSystem;
    [SerializeField] PlayerJumper playerJumper;
    private void Awake()
    {
        InputSystem = GetComponent<InputSystem>();
        // _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float SwipeSpeed = Mathf.Clamp(InputSystem.HorizontalDirection, -4f, 4f);
        InputSystem.GetInputData();
        print(SwipeSpeed);

        if (GameManager.Instance.GameState == GameStates.InGameStart)
        {
            if (playerJumper.comboCounter > 5)
            {
                transform.Rotate(Vector3.up * SwipeSpeed * 2f);
            }
            else if (playerJumper.comboCounter > 3)
            {
                transform.Rotate(Vector3.up * SwipeSpeed * 2f);
            }
            else
            {
                // transform.Rotate(Vector3.up * SwipeSpeed);
                // _rb.angularVelocity = Vector3.up * InputSystem.HorizontalDirection;
                _rb.rotation = Quaternion.Euler(_rb.rotation.eulerAngles + Vector3.up * SwipeSpeed);
            }
        }
    }
}
