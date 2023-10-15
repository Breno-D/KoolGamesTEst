using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPersistency : MonoBehaviour
{
    #region Singleton
    public static LevelPersistency instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("levelSaved");
    }
    
    public void UpdateLevel(int levelToSave)
    {
        PlayerPrefs.SetInt("levelSaved", levelToSave);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("levelSaved", 0);
    }
}
