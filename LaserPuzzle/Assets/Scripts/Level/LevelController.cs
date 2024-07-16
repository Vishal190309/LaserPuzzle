using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int TotalOfLaserEndPoints;
    private int currentLaserEndPoints;
    
    public void increaseNoOfLaserEndPoints()
    {
        currentLaserEndPoints += 1;
        if(currentLaserEndPoints >= TotalOfLaserEndPoints)
        {
            print("LevelFinished");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
