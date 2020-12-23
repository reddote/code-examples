using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBack : MonoBehaviour
{
    public static CameraBack control;

    public CameraDelay cameraDelay;
    public Vector3 upDistance;
    public bool isBack = false;
    private float _coolDown = 2f;
    public float distanceDump;
    public Vector3 velocity = Vector3.one;

    private void Awake(){
        if (control != null)
        {
            return;

        }
        control = this;
    }

    private void Update(){
        if (isBack){
            _coolDown -= Time.deltaTime;
            CameraMoveUp();
        }

        if (_coolDown < 0){
            cameraDelay.enabled = true;
            enabled = false;
        }
    }
    
    private void CameraMoveUp(){
        Vector3 toPos = upDistance;
        Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDump);
        transform.position = curPos;
    }
}
