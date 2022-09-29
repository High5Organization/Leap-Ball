using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject BombImage;
    [SerializeField] GameObject SettingsBttn;
    [SerializeField] GameObject ExitBttn;
    [SerializeField] TextMeshProUGUI StairCount;
    [SerializeField] TextMeshProUGUI BounceCount;
    private void OnEnable()
    {
        GameManager.Instance.OnGameBegin += ShowStartPanel;

        GameManager.Instance.OnGameStart += ShowGamePanel;

        GameManager.Instance.OnGameWin += ShowWinPanel;
    }
    void ShowStartPanel()
    {
        _startPanel.SetActive(true);
        _gamePanel.SetActive(false);
        _winPanel.SetActive(false);
    }
    void ShowGamePanel()
    {
        _startPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _winPanel.SetActive(false);
    }
    void ShowWinPanel()
    {
        BounceCount.text = "Bounces : " + GameManager.Instance.BounceCount;
        StairCount.text = "Stairs : " + GameManager.Instance.StairsCount;
        _startPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
    }
    public void StartButton()
    {
        GameManager.Instance.IntializeGameStart();

        GameManager.Instance.IntializeButtonClick();
        _player.GetComponent<Rigidbody>().isKinematic = false;

        GameManager.Instance.BounceCount = -1;
        GameManager.Instance.StairsCount = 0;
        GameManager.Instance.IntializeButtonClick();
    }
    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.IntializeButtonClick();
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameBegin -= ShowStartPanel;

        GameManager.Instance.OnGameStart -= ShowGamePanel;

        GameManager.Instance.OnGameWin -= ShowWinPanel;
    }
}
