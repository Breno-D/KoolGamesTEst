using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject loseLevelPanel, winLevelPanel;
    bool levelUpdated = false;
    bool _gameEnded = false;
    public bool gameEnded {get{return _gameEnded;}}

    // Update is called once per frame
    public void WinLevel()
    {
        _gameEnded = true;
        winLevelPanel.SetActive(true);
        if(!levelUpdated)
        {
            levelUpdated = true;
            LevelPersistency.instance.UpdateLevel();
        }
    }

    public void LoseLevel()
    {
        _gameEnded = true;
        loseLevelPanel.SetActive(true);
    }

    public void ResetLevel()
    {
        FindObjectOfType<LevelManager>().ResetWholeLevel();
        loseLevelPanel.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
