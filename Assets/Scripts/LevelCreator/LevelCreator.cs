using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int currentLevel;
    public int PoolSize;
    public List<int> levelCount;
    private void Awake()
    {
        for (int i = 0; i < levelCount.Count; i++)
        {
            levelCount[i] = i;
        }
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        // GameManager.Instance.OnGameBegin += GetLevel;
        // GameManager.Instance.OnGameWin += GoNextLevel;
    }
    public void GoNextLevel()
    {
        currentLevel++;
        PoolSize = levelCount[currentLevel] * 20 + 200;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("PoolSize", PoolSize);
    }
    public void GetLevel()
    {
        if (currentLevel > 0)
        {
            PoolSize = PlayerPrefs.GetInt("PoolSize");
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            currentLevel = 0;
            PoolSize = 50;
        }
    }
}
