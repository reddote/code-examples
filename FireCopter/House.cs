using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour{
    public bool isHouseBurning;
    public float distanceChecker;
    //indukator resimleri
    public Sprite notDanger;
    public Sprite danger;
    
    //indukator kontrolcüsü
    public HouseWaypoint houseWayPoint;

    private void Start(){
        houseWayPoint = FindObjectOfType<HouseWaypoint>();
    }

    //raycast shpere etrafındaki ağaçları kontrol ediyor.
    public void SphereChecker(){
        Collider[] hitTrees = Physics.OverlapSphere(transform.position, distanceChecker);
        Debug.Log(hitTrees.Length);
        foreach (var x in hitTrees){
            if (x.GetComponent<FireSpreadController>()){
                if ( x.GetComponent<FireSpreadController>().isFireStarted){
                    //houseWayPoint.img.sprite = danger;
                }else{
                    //houseWayPoint.img.sprite = notDanger;
                }
            }
        }

    }

}