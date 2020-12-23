using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorAnimation : MonoBehaviour
{
    public float rotorSpeed;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,rotorSpeed*Time.deltaTime,0), Space.Self);
    }
}
