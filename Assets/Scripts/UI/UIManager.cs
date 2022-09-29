using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnEnable()
    {
        GameManager.Instance.OnGameBegin += ShowStartPanel;

        GameManager.Instance.OnGameStart += ShowGamePanel;

        GameManager.Instance.OnGameWin += ShowWinPanel;
    }
    private void Update()
    {
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
        _startPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);
    }
    void HideStartPanel()
    {

    }
    void HideGamePanel()
    {

    }
    void HideWinPanel()
    {

    }
    public void StartButton()
    {
        BombImage.transform.DOScale(Vector3.one * 33, 1f).OnComplete(() =>
        {
            GameManager.Instance.IntializeGameStart();
            _player.GetComponent<Rigidbody>().isKinematic = false;

            GameManager.Instance.BounceCount = -1;
            GameManager.Instance.StairsCount = 0;
        });
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameBegin -= ShowStartPanel;

        GameManager.Instance.OnGameStart -= ShowGamePanel;

        GameManager.Instance.OnGameWin -= ShowWinPanel;
    }
}
