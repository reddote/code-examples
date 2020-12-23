using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCarrier : MonoBehaviour{

    private HelicopterController _playerHelicopter;
    
    void Start(){
        _playerHelicopter = GetComponentInParent<HelicopterController>();
    }

    private void OnTriggerStay(Collider other){
        if (other.gameObject.CompareTag("Water")){
            if (_playerHelicopter.isStopped){
                Debug.Log("Su alıyosun bebişim");
                _playerHelicopter.waterFill = 100f;
            }
        }
    }
}