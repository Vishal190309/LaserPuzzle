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
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }

    public void OnHelpButtonClicked()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true); 
    }

    public void OnBackClick()
    {
        helpMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuitButtonCliked()
    {
        Application.Quit();
    }
}
