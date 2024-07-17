using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevlLoader : MonoBehaviour
{
    private Button button;
    public string LevelName;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        if (LevelName == "Level1")
        {
            SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
            SceneManager.LoadScene(LevelName);
        }
        else
        {
            LevelStatus status = LevelManager.Instance.GetLevelStatus(LevelName);
            switch(status)
            {
                case LevelStatus.Locked:
                    Debug.Log("This level is locked, unlock it to play");
                    break;
                case LevelStatus.Unlocked:
                    SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
                    SceneManager.LoadScene(LevelName);
                    break;
                case LevelStatus.Completed:
                    SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
                    SceneManager.LoadScene(LevelName);
                    break;
            }
        }
    }
}
