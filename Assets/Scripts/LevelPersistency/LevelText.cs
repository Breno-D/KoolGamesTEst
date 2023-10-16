using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    TextMeshProUGUI tmproComponent;

    void Start()
    {
        tmproComponent = GetComponent<TextMeshProUGUI>();
        int levelToShow = LevelPersistency.instance.GetCurrentLevel()+1;
        tmproComponent.text = "Level: "+ levelToShow.ToString();
    }


}
