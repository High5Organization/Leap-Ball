using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CamFallow : MonoBehaviour
{
    [SerializeField] PlayerJumper playerJumper;
    [SerializeField] Volume volume;
    [SerializeField] Camera Camera;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpeedUpEffect;
    private void Start()
    {
        GameManager.Instance.IsDestructable = false;
        Bloom bloom;
        Camera.fieldOfView = 60;
        SpeedUpEffect.SetActive(false);

        if (volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = 1;
        }
    }
    private void Update()
    {
        if (playerJumper.comboCounter > 5 && GameManager.Instance.GameState == GameStates.InGameStart)
        {
            Bloom bloom;
            SpeedUpEffect.SetActive(true);
            // StartCoroutine(Destruct());

            float fieldOfView = Mathf.Lerp(Camera.fieldOfView, 90, Time.deltaTime * 10);
            Camera.fieldOfView = fieldOfView;

            if (volume.profile.TryGet<Bloom>(out bloom))
            {
                bloom.intensity.value = 9;
            }

            float cameraZAxis = Mathf.Lerp(Camera.transform.position.z, -30, Time.deltaTime * 3);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, cameraZAxis);
        }
        else if (playerJumper.comboCounter > 3 && GameManager.Instance.GameState == GameStates.InGameStart)
        {
            float fieldOfView = Mathf.Lerp(Camera.fieldOfView, 70, Time.deltaTime * 10);
            Camera.fieldOfView = fieldOfView;

            float cameraZAxis = Mathf.Lerp(Camera.transform.position.z, -27, Time.deltaTime * 3);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, cameraZAxis);
        }
        else
        {
            Bloom bloom;

            GameManager.Instance.IsDestructable = false;

            SpeedUpEffect.SetActive(false);

            float fieldOfView = Mathf.Lerp(Camera.fieldOfView, 60, Time.deltaTime * 10);
            Camera.fieldOfView = fieldOfView;

            float cameraZAxis = Mathf.Lerp(Camera.transform.position.z, -25, Time.deltaTime * 10);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5, cameraZAxis);
            
            if (volume.profile.TryGet<Bloom>(out bloom))
            {
                bloom.intensity.value = 1;
            }
        }
        // IEnumerator Destruct()
        // {
        //     print("bi daha");
        //     GameManager.Instance.IsDestructable = true;

        //     yield return new WaitForSeconds(2f);

        //     GameManager.Instance.IsDestructable = false;
        // }
        // _rb.angularVelocity = Vector3.up * InputSystem.HorizontalDirection*2;
    }
}
