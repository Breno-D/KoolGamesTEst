using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColors : MonoBehaviour
{
    [SerializeField] List<GameObject> coloredSpheres = new List<GameObject>();
    [SerializeField] Dictionary<PlatformColors, int> platformsInContact = new Dictionary<PlatformColors, int>();
    [SerializeField] List<Color> assignedColors;
    Quaternion startRotation;

    public void StartPlatform()
    {
        startRotation = transform.rotation;
        int colorIterator = 0;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.GetComponent<Renderer>().material.SetColor("_Color", assignedColors[colorIterator]);
            sphere.GetComponent<SphereDetection>().SetActiveColor(assignedColors[colorIterator]);
            colorIterator++;
        }
    }

    public void ResetPlatform()
    {
        transform.rotation = startRotation;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.SetActive(true);
        }
    }

    public void SetAssignedColors(List<Color> listToSet)
    {
        assignedColors = new List<Color>(listToSet);
    }

    public List<Color> GetPlatformActiveColors()
    {
        List<Color> colorsToMatch = new List<Color>();
        foreach(GameObject sphere in coloredSpheres)
        {
            if(sphere.activeSelf)
            {
                colorsToMatch.Add(sphere.GetComponent<SphereDetection>().GetActiveColor());
            }
        }
        return colorsToMatch;
    }
}
