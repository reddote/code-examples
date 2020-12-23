using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDelay : MonoBehaviour{

    public Transform helicopter;
    public Vector3 defaultDistance;
    public float distanceDump;
    public Vector3 velocity = Vector3.one;

    private void LateUpdate(){
        SmoothFollow();
    }

    private void SmoothFollow(){
        Vector3 toPos = helicopter.position + (helicopter.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDump);
        transform.position = curPos;
        
        transform.LookAt(helicopter, -helicopter.right);
    }
} 