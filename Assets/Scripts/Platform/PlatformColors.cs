using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColors : MonoBehaviour
{
    [SerializeField] List<GameObject> coloredSpheres = new List<GameObject>();
    [SerializeField] List<Color> assignedColors;
    Quaternion startRotation;
    void Start()
    {
        startRotation = transform.rotation;
        int colorIterator = 0;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.GetComponent<Renderer>().material.SetColor("_Color", assignedColors[colorIterator]);
            sphere.GetComponent<SphereDetection>().SetActiveColor(assignedColors[colorIterator]);
            colorIterator++;
        }
        Invoke("ResetPlatform", 3f);
    }

    public void ResetPlatform()
    {
        transform.rotation = startRotation;
        foreach(GameObject sphere in coloredSpheres)
        {
            sphere.SetActive(true);
        }
    }
}
