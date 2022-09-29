using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Events
    public event Action OnGameBegin;
    public event Action OnGameStart;
    public event Action OnGameWin;
    public event Action OnJump;
    public event Action OnDestruct;
    public event Action OnSecondBoost;
    public event Action OnThirtBoost;
    public event Action OnButtonClick;
    #endregion

    #region  Sounds
    [Header("Sounds")]
    public AudioSource JumpSound;
    public AudioSource DestructSound;
    public AudioSource SecondBoost;
    public AudioSource ThirtBoost;
    public AudioSource GamePlay;
    public AudioSource ButtonClicksSound;
    public AudioSource WinSound;
    #endregion

    #region Singleton
    public GameObject BounceText;
    public GameObject StairText;
    public bool IsJumpable;
    public int ComboCounter;
    public int JumpPower;
    public int _jumpPower;
    public int JumpPowerIncrease;

    public int StairsCount;
    public int BounceCount;
    public int PoolSize;
    public bool IsDestructable;

    public static GameManager Instance { get; private set; }
    public GameStates GameState { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    private void Start()
    {
        InitializeGameBegin();
    }
    public void InitializeGameBegin()
    {
        GameState = GameStates.InGameBegin;
        OnGameBegin?.Invoke();
    }
    public void IntializeGameStart()
    {
        GameState = GameStates.InGameStart;
        OnGameStart?.Invoke();
    }
    public void IntializeGameWin()
    {
        GameState = GameStates.InGameWin;
        OnGameWin?.Invoke();
    }
    public void IntializeSecondBoost()
    {
        OnSecondBoost?.Invoke();
    }
    public void IntializeThirtBoost()
    {
        OnThirtBoost?.Invoke();
    }
    public void IntializeButtonClick()
    {
        OnButtonClick?.Invoke();
    }
    public void IntializeJump()
    {
        OnJump?.Invoke();
    }
    public void IntializeDestruct()
    {
        OnDestruct?.Invoke();
    }
}
