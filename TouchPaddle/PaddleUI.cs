using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleUI : MonoBehaviour{
    public PaddleController paddle;

    void Update(){
        PaddleUIRotater();
    }

    private void PaddleUIRotater(){
        transform.Rotate(paddle.paddleSpeed * Time.deltaTime * new Vector3(0,0,paddle.direction),Space.Self);
    }
}