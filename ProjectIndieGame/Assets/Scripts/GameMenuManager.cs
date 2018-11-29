using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {

    public void RestartGame()
    {
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChooseCharacters()
    {
        ResumeTime();
        PlayerPrefs.SetInt("MainMenu", 1);
        SceneManager.LoadScene(0);
    }

    public void ChooseStage()
    {
        ResumeTime();
        PlayerPrefs.SetInt("MainMenu", 2);
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        ResumeTime();
        PlayerPrefs.SetInt("MainMenu", -1);
        SceneManager.LoadScene(0);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
        Pause.Paused = false;
    }
}
