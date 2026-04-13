// Developed by Tushar. All Rights Reserved (c) VLab IIT Kanpur
// Description: A component for Interacting Custom reticle.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReticle : MonoBehaviour
{
    public Transform outer;
    public float rotationSpeed;
    void Start()
    {
        
    }

    
    void Update()
    {
        outer.Rotate(2f * rotationSpeed * Time.deltaTime * Vector3.forward);
        transform.Rotate(-rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
