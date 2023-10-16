using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] GameObject camera2Platforms, camera4Platforms;
    public void AdjustCameraCentralPoint(List<Vector3> platformPositions)
    {
        float sumX = 0;
        float sumY = 0;
        int numberOfPlatforms = 0;
        foreach(Vector3 position in platformPositions)
        {
            sumX += position.x;
            sumY += position.y;
            numberOfPlatforms++;
        }

        float newCameraX = sumX/numberOfPlatforms;
        float newCameraY = sumY/numberOfPlatforms;
        transform.position = new Vector3(newCameraX, newCameraY, transform.position.z);

        ActivateVirtualCamera(numberOfPlatforms);
    }

    void ActivateVirtualCamera(int platformsOnLevel)
    {
        if(platformsOnLevel==4)
        {
            camera4Platforms.SetActive(true);
            camera2Platforms.SetActive(false);
        }
    }
}
