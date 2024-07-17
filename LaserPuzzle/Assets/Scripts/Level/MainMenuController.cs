using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelSelectionMenu;
    [SerializeField]
    private GameObject helpMenu;

    public void OnPlayButtonClicked()
    {
        SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }

    public void OnHelpButtonClicked()
    {
        SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
        mainMenu.SetActive(false);
        helpMenu.SetActive(true); 
    }

    public void OnBackClick()
    {
        SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
        helpMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuitButtonCliked()
    {
        SoundManager.Instance.PlaySoundEffect(Sound.BUTTON_CLICK);
        Application.Quit();
    }
}
