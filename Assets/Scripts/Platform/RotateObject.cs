using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    bool canRotate = true;
    [SerializeField] float rotationSpeed = 30f;
    LevelManager levelManager;
    GameManager gameManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        if(canRotate && !gameManager.gameEnded)
        {
            canRotate = false;
            StartCoroutine(RotateCoroutine());
        }
    }

    IEnumerator RotateCoroutine()
    {
        float timeToWait = 60f/rotationSpeed;
        while(timeToWait>=0)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
            timeToWait -= Time.deltaTime;
            yield return null;
        }
        levelManager.canDetectSpheres = true;
        yield return new WaitForSeconds(0.5f);
        levelManager.CheckLevelCompletion();
        canRotate = true;
    }
}
