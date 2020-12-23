using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour{
    public GameControl gameControl;
    public SaveGame saveGame;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Fire")){
            var temp = other.gameObject.GetComponent<FireSpreadController>();
            temp.IsFireExtinquishing();
            temp.isFireStarted = false;
            gameControl.EndGameCheck();
            if (gameControl.FindFirstFire()){
                saveGame.firstFire = gameControl.FindFirstFire();
                saveGame.TargetWaypoint();
            }
        }
    }
}
