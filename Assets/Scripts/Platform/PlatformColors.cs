using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColors : MonoBehaviour
{
    [SerializeField] List<GameObject> coloredSpheres = new List<GameObject>();
    List<PlatformColors> platformsInContact = new List<PlatformColors>();
    List<Color> _assignedColors;
    public List<Color> assignedColors {get{return _assignedColors;}}
    [SerializeField] LayerMask platformMask;
    Quaternion startRotation;
    public List<Color> colorsMissingTest;

    public void CheckSurroundings()
    {
        for(int i = 0; i < coloredSpheres.Count; i++)
        {
            Vector3 vectorOut = new Vector3(coloredSpheres[i].transform.position.x, coloredSpheres[i].transform.position.y, transform.position.z) - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, vectorOut , out hit, 1.5f, platformMask))
            {
                if(hit.collider.gameObject.GetComponent<PlatformColors>()!=null)
                {
                    platformsInContact.Add(hit.collider.gameObject.GetComponent<PlatformColors>());
                }
            }
        }
    }

    public void ColorPlatform()
    {
        startRotation = transform.rotation;
        int colorIterator = 0;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.GetComponent<Renderer>().material.SetColor("_Color", _assignedColors[colorIterator]);
            sphere.GetComponent<SphereDetection>().SetActiveColor(_assignedColors[colorIterator]);
            colorIterator++;
        }
    }

    public void ResetPlatform()
    {
        transform.rotation = startRotation;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
            sphere.SetActive(true);
        }
    }

    public bool CheckForGameLoss()
    {
        List<Color> allColorsMissing = GetPlatformActiveColors();
        List<Color> allColorsMissingHelper = new List<Color>(allColorsMissing);
        foreach(Color colorMissing in allColorsMissingHelper)
        {
            foreach(PlatformColors platformInContact in platformsInContact)
            {
                if(platformInContact.GetPlatformActiveColors().Contains(colorMissing))
                {
                    allColorsMissing.Remove(colorMissing);
                }
            }
        }
        if(allColorsMissing.Count!=0)
        {
            return true;
        }
        return false;
    }

    public void SetAssignedColors(List<Color> listToSet)
    {
        _assignedColors = new List<Color>(listToSet);
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
        colorsMissingTest = new List<Color>(colorsToMatch);
        return colorsToMatch;
    }

    public int GetPlatformActiveColorsCount()
    {
        return GetPlatformActiveColors().Count;
    }

}
