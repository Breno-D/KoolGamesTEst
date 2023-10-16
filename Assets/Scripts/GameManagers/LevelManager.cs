using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    bool _canDetectSpheres;
    public bool canDetectSpheres {get { return _canDetectSpheres;} set {_canDetectSpheres = value;}}

    [SerializeField] GameObject platformPrefab;
    [SerializeField] List<Color> colorsToChooseFrom;
    Vector3 startPos = new Vector3(0, -1f, -5f);
    List<Color> startingColors;
    List<Vector3> platformPositions = new List<Vector3>();
    List<PlatformColors> allPlatforms = new List<PlatformColors>();
    
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int numberOfPlatforms = GenerateNumberOfPlatforms();
        GeneratePlatforms(numberOfPlatforms);
    }

    void GeneratePlatforms(int numberOfPlatforms)
    {
        for(int i=0; i < numberOfPlatforms;i++)
        {
            Vector3 newPlatformPos;
            if(platformPositions.Count==0)
            {
                newPlatformPos = startPos;
            }
            else
            {
                newPlatformPos = GetNewPlatformPosition();
            }
            while(platformPositions.Contains(newPlatformPos))
            {
                newPlatformPos = GetNewPlatformPosition();
            }
            platformPositions.Add(newPlatformPos);
            GameObject newGeneratedPlatform = Instantiate(platformPrefab, newPlatformPos, platformPrefab.transform.rotation);
            PlatformColors newGeneratedPlatformColors = newGeneratedPlatform.GetComponent<PlatformColors>();
            newGeneratedPlatformColors.SetAssignedColors(colorsToChooseFrom);
            newGeneratedPlatformColors.ColorPlatform();
            allPlatforms.Add(newGeneratedPlatformColors);
        }
        foreach(PlatformColors platform in allPlatforms)
        {
            platform.CheckSurroundings();
        }
        FindObjectOfType<CameraPosition>().AdjustCameraCentralPoint(platformPositions);
    }

    Vector3 GetNewPlatformPosition()
    {
        int basePlatformToCalculate = Random.Range(0, platformPositions.Count);
        Vector3 connectionPosition = new Vector3(platformPositions[basePlatformToCalculate].x, platformPositions[basePlatformToCalculate].y, platformPositions[basePlatformToCalculate].z);
        int decideSide = Random.Range(0, 6);
        switch(decideSide)
        {
            case 0:
                return new Vector3(connectionPosition.x, connectionPosition.y+1.7f, connectionPosition.z);
            case 1:
                return new Vector3(connectionPosition.x+1.48f, connectionPosition.y+0.85f, connectionPosition.z);
            case 2:
                return new Vector3(connectionPosition.x+1.48f, connectionPosition.y-0.85f, connectionPosition.z);
            case 3:
                return new Vector3(connectionPosition.x, connectionPosition.y-1.7f, connectionPosition.z);
            case 4:
                return new Vector3(connectionPosition.x-1.48f, connectionPosition.y-0.85f, connectionPosition.z);
            case 5:
                return new Vector3(connectionPosition.x-1.48f, connectionPosition.y-0.85f, connectionPosition.z);
        }
        return Vector3.zero;
    }

    int GenerateNumberOfPlatforms()
    {
        int chance = Random.Range(0,101);
        int currentLevel = LevelPersistency.instance.GetCurrentLevel();
        if(chance<=40 || currentLevel == 0)
        {
            return 2;
        }
        else
        {
            return 4;
        }
    }

    public void CheckLevelCompletion()
    {
        int sphereCount = 0;
        foreach(PlatformColors platform in allPlatforms)
        {
            if(platform.CheckForGameLoss())
            {
                FindObjectOfType<GameManager>().LoseLevel();
            }
            sphereCount += platform.GetPlatformActiveColorsCount();
        }
        if(sphereCount==0)
        {
            FindObjectOfType<GameManager>().WinLevel();
        }
    }

    public void ResetWholeLevel()
    {
        foreach(PlatformColors platform in allPlatforms)
        {
            platform.ResetPlatform();
        }
    }
}
