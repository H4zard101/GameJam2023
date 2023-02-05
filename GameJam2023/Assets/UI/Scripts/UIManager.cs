using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseMenuShow()
    {
        pauseMenu.SetActive(true); 
    }

    public void PauseMenuClose()
    {
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextScene(int index = 1)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void OpenCreditsScene()
    {
        SceneManager.LoadScene("Credits");

    }
}
