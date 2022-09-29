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
    }
    private void Update()
    {
        float SwipeSpeed = Mathf.Clamp(InputSystem.HorizontalDirection, -4f, 4f);
        InputSystem.GetInputData();

        if (GameManager.Instance.GameState == GameStates.InGameStart)
        {
            if (GameManager.Instance.ComboCounter > 5)
            {
                transform.Rotate(Vector3.up * SwipeSpeed * 2f);
            }
            else if (GameManager.Instance.ComboCounter > 3)
            {
                transform.Rotate(Vector3.up * SwipeSpeed * 2f);
            }
            else
            {
                _rb.rotation = Quaternion.Euler(_rb.rotation.eulerAngles + Vector3.up * SwipeSpeed);
            }
        }
    }
}
