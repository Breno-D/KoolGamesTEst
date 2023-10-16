using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] GameObject continueButton, playButton, resetButton, resetPanel;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if(LevelPersistency.instance.GetCurrentLevel()!=0)
        {
            continueButton.SetActive(true);
            resetButton.SetActive(true);
            playButton.SetActive(false);
        }
    }
    
    public void ShowResetProgressPanel()
    {
        resetPanel.SetActive(true);
    }

    public void ResetAndPlay()
    {
        LevelPersistency.instance.ResetProgress();
        Play();
    }

    public void CloseResetPanel()
    {
        resetPanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
