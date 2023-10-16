using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetection : MonoBehaviour
{
    [SerializeField] LayerMask sphereLayer;
    Transform platformTransform;
    Color activeColor;
    LevelManager levelManager;

    void Start()
    {
        platformTransform = transform.parent.parent;
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        if(levelManager.canDetectSpheres)
        {
            Vector3 vectorOut = transform.position - platformTransform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, vectorOut , out hit, 0.5f, sphereLayer))
            {
                if(hit.collider.gameObject.GetComponent<SphereDetection>().GetActiveColor() == activeColor)
                {
                    hit.collider.gameObject.GetComponent<SphereDetection>().StartSphereAnimation();
                    StartSphereAnimation();
                }
            }
        }
    }

    public void SetActiveColor(Color colorToSet)
    {
        activeColor = colorToSet;
    }

    public Color GetActiveColor()
    {
        return activeColor;
    }

    public void StartSphereAnimation()
    {
        StartCoroutine(DeleteSphere());
    }

    IEnumerator DeleteSphere()
    {
        while(transform.localScale.x<=0.01)
        {
            transform.localScale *= 1.02f;
            yield return 0;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
