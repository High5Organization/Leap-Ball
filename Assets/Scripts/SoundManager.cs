using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject JumpSoundPitch;
    private void Start()
    {
        GameManager.Instance.JumpSound.GetComponent<AudioSource>().pitch = 0.4f;
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameBegin += GameSound;

        GameManager.Instance.OnJump += JumpSound;

        GameManager.Instance.OnDestruct += Destruct;

        GameManager.Instance.OnSecondBoost += SecondBoost;

        GameManager.Instance.OnThirtBoost += ThirtBoost;

        GameManager.Instance.OnButtonClick += ButtonClickSound;

        GameManager.Instance.OnGameWin += WinSound;
    }
    public void GameSound()
    {
        GameManager.Instance.GamePlay.Play();
    }
    public void SecondBoost()
    {
        GameManager.Instance.SecondBoost.Play();
    }
    public void ThirtBoost()
    {
        GameManager.Instance.ThirtBoost.Play();
    }
    public void WinSound()
    {
        GameManager.Instance.WinSound.Play();
    }
    public void ButtonClickSound()
    {
        GameManager.Instance.ButtonClicksSound.Play();
    }
    public void JumpSound()
    {
        GameManager.Instance.JumpSound.Play();
    }
    public void Destruct()
    {
        GameManager.Instance.DestructSound.Play();
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameBegin -= GameSound;

        GameManager.Instance.OnJump -= JumpSound;

        GameManager.Instance.OnDestruct -= Destruct;

        GameManager.Instance.OnSecondBoost -= SecondBoost;
        GameManager.Instance.OnThirtBoost -= ThirtBoost;

        GameManager.Instance.OnButtonClick -= ButtonClickSound;

        GameManager.Instance.OnGameWin -= WinSound;
    }
}
