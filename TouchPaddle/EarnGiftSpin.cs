using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnGiftSpin : MonoBehaviour{
    public RectTransform spinRectTransform;
    public float spinSpeed;

    private bool _spinLeft;
    private bool _spinRight;
    private float _spinSlowByTime = 0;
    
    void Update(){
        SpinRotateCheck();
        SpinRotater();
    }

    private void SpinRotateCheck(){
        if (TouchScreen.swipeLeft){
            _spinLeft = true;
            _spinRight = false;
        }

        if (TouchScreen.swipeRight){
            _spinLeft = false;
            _spinRight = true;
        }
    }

    private void SpinRotater(){
        if (_spinLeft){
            spinRectTransform.Rotate(new Vector3(0,0,spinSpeed * Time.deltaTime));
            SpinSlowByTime();
        }

        if (_spinRight){
            spinRectTransform.Rotate(new Vector3(0,0,-spinSpeed * Time.deltaTime));
            SpinSlowByTime();
        }

        if (spinSpeed <= 0){
            _spinLeft = false;
            _spinRight = false;
        }
    }

    private void SpinSlowByTime(){
        _spinSlowByTime += Time.deltaTime * 5;
        if (_spinSlowByTime >= 100){
            _spinSlowByTime = 100f;
        }
        spinSpeed -= Time.deltaTime * 100;
    }

}