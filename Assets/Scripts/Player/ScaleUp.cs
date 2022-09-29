using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleUp : MonoBehaviour
{
    [SerializeField] float _playerSizeIncrease;
    [SerializeField] PlayerJumper playerJumper;

    private void Start()
    {
        _playerSizeIncrease = 0;

    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (GameManager.Instance.GameState == GameStates.InGameStart && other.gameObject.CompareTag("Destruction"))
    //     {
    //         if (GameManager.Instance.ComboCounter > 5)
    //         {
    //             StartCoroutine(GetDestruct());
    //         }
    //     }
    // }
    private void OnCollisionExit(Collision other)
    {
        if (GameManager.Instance.GameState == GameStates.InGameStart && other.gameObject.CompareTag("Stairs"))
        {
            //Player scale up when hit the 3. combo to maximum 
            if (GameManager.Instance.ComboCounter > 5 && _playerSizeIncrease < 1)
            {
                _playerSizeIncrease += 0.2f;
                transform.DOScale(Vector3.one * (1.2f + _playerSizeIncrease), 0.5f);
            }
            else if (GameManager.Instance.ComboCounter < 5)
            {
                _playerSizeIncrease = 0;
                transform.DOScale(Vector3.one * (1.2f + _playerSizeIncrease), 0.5f);
            }
        }
    }
}
