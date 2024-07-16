using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject levelCompleteUI;
    [SerializeField] 
    private GameObject pauseUI;
    [SerializeField] 
    private InputManager inputManager;
    [SerializeField]
    private int TotalOfLaserEndPoints;
    private int currentLaserEndPoints;

    private void Start()
    {
        inputManager.OnPause += OnPause;
    }
    public void increaseNoOfLaserEndPoints()
    {
        currentLaserEndPoints += 1;
        if(currentLaserEndPoints >= TotalOfLaserEndPoints)
        {
           OnLevelComplete();
        }
    }
    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void OnPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void OnLevelComplete()
    {
        levelCompleteUI.SetActive(true);
    }



   
   

   

}
