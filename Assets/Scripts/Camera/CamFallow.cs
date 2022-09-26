using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFallow : MonoBehaviour
{
    [SerializeField] PlayerJumper playerJumper;
    [SerializeField] Camera Camera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpeedUpEffect;
    private void Start()
    {
        Camera.fieldOfView = 60;
        SpeedUpEffect.SetActive(false);
    }
    private void Update()
    {
        if (playerJumper.comboCounter > 5 && GameManager.Instance.GameState == GameStates.InGameStart)
        {
            SpeedUpEffect.SetActive(true);
            float fieldOfView = Mathf.Lerp(Camera.fieldOfView, 70, Time.deltaTime * 10);
            Camera.fieldOfView = fieldOfView;

            float cameraZAxis = Mathf.Lerp(Camera.transform.position.z, -27, Time.deltaTime * 3);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, cameraZAxis);
        }
        else
        {
            SpeedUpEffect.SetActive(false);
            float fieldOfView = Mathf.Lerp(Camera.fieldOfView, 60, Time.deltaTime * 10);
            Camera.fieldOfView = fieldOfView;

            float cameraZAxis = Mathf.Lerp(Camera.transform.position.z, -25, Time.deltaTime * 10);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, cameraZAxis);

        }
        // _rb.angularVelocity = Vector3.up * InputSystem.HorizontalDirection*2;
    }
}
