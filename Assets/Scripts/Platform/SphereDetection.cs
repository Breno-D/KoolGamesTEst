using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetection : MonoBehaviour
{
    [SerializeField] LayerMask sphereLayer;
    Transform platformTransform;
    Color activeColor;

    void Start()
    {
        platformTransform = transform.parent.parent;
    }

    void FixedUpdate()
    {
        Vector3 vectorOut = transform.position - platformTransform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, vectorOut , out hit, 1f, sphereLayer))
        {
            if(hit.collider.gameObject.GetComponent<SphereDetection>().GetActiveColor() == activeColor)
            {
                Debug.Log("mesmacor");
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
}
