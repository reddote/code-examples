using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour{

    public float paddleSpeed;
    [Range(-1,1)]
    public int direction;

    private void Update(){
        if (SaveGame.control.isGameStarted && !SaveGame.control.isGameEnded){
            PaddleRotater();
        }
    }

    private void PaddleRotater(){
        transform.Rotate(paddleSpeed * Time.deltaTime * new Vector3(0,0,direction),Space.Self);
    }
}
