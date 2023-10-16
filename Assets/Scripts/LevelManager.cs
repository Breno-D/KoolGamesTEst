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
    
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int numberOfPlatforms = GenerateNumberOfPlatforms();
        GameObject startingPlatform = Instantiate(platformPrefab, startPos, platformPrefab.transform.rotation);
        startingColors = GenerateListOfColors();
        startingPlatform.GetComponent<PlatformColors>().SetAssignedColors(startingColors);
        startingPlatform.GetComponent<PlatformColors>().StartPlatform();
        platformPositions.Add(startPos);
        GenerateNextPlatforms(numberOfPlatforms);
    }

    void GenerateNextPlatforms(int numberOfPlatforms)
    {
        for(int i=1; i < numberOfPlatforms;i++)
        {
            Vector3 newPlatformPos = GetNewPlatformPosition();
            while(platformPositions.Contains(newPlatformPos))
            {
                newPlatformPos = GetNewPlatformPosition();
            }
            platformPositions.Add(newPlatformPos);
            GameObject newGeneratedPlatform = Instantiate(platformPrefab, newPlatformPos, platformPrefab.transform.rotation);
        }
    }

    List<Color> GenerateListOfColors()
    {
        List<Color> listToReturn = new List<Color>();
        for(int iterator = 0; iterator < 6; iterator++)
        {
            listToReturn.Add(colorsToChooseFrom[Random.Range(0,colorsToChooseFrom.Count)]);
        }
        return listToReturn;
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
                return new Vector3(connectionPosition.x+1.48f, connectionPosition.y+0.9f, connectionPosition.z);
            case 2:
                return new Vector3(connectionPosition.x+1.48f, connectionPosition.y-0.9f, connectionPosition.z);
            case 3:
                return new Vector3(connectionPosition.x, connectionPosition.y-1.7f, connectionPosition.z);
            case 4:
                return new Vector3(connectionPosition.x-1.48f, connectionPosition.y-0.9f, connectionPosition.z);
            case 5:
                return new Vector3(connectionPosition.x-1.48f, connectionPosition.y-0.9f, connectionPosition.z);
        }
        return Vector3.zero;
    }

    int GenerateNumberOfPlatforms()
    {
        int chance = Random.Range(0,101);
        if(chance<=60)
        {
            return 2;
        }
        else if(chance>60 && chance<=80)
        {
            return 4;
        }
        else
        {
            return 6;
        }
    }
}
