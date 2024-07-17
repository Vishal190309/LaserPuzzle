using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Action<int> OnObjectCountUpdated;
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (GetLevelStatus("Level1") == LevelStatus.Locked)
        {
            SetLevelStatus("Level1", LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus status = (LevelStatus)PlayerPrefs.GetInt(level,0);
        return status;
    }

    public void SetLevelStatus(string level, LevelStatus status)
    {
        PlayerPrefs.SetInt(level, (int)status);

    }

    public void SetCurrentLevelComplete()
    {
        SetLevelStatus(SceneManager.GetActiveScene().name, LevelStatus.Completed);

        string nextSceneName = NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SetLevelStatus(nextSceneName, LevelStatus.Unlocked);
    }

    private string NameFromIndex(int BuildIndex)
    {
        if (BuildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
            int slash = path.LastIndexOf('/');
            string name = path.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            return name.Substring(0, dot);
        }
        return "";
    }
}
