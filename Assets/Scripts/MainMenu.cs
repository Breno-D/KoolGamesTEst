using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] GameObject continueButton, playButton, resetButton;

    void Start()
    {
        if(LevelPersistency.instance.GetCurrentLevel()!=0)
        {
            continueButton.SetActive(true);
            resetButton.SetActive(true);
            playButton.SetActive(false);
        }
    }
    
    public void ShowResetProgressPanel()
    {

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
