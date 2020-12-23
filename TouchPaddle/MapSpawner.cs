using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour{
    public static MapSpawner instance;
    
    //Gameobjects which spawns in the beginning
    public GameObject startIsland;
    public GameObject endIsland;
    
    //Spawn Point Transform
    public Transform firsIslandSpawnPoint;
    public Transform secondIslandSpawnPoint;

    private void Awake(){
        if (instance != null){
            return;
        } else{
            instance = this;
        }

        secondIslandSpawnPoint.position = new Vector3(0,0,100f);
        MapObjectsSpawner();
    }
    
    public void MapObjectsSpawner(){
        Instantiate(startIsland, firsIslandSpawnPoint);
        Instantiate(endIsland, secondIslandSpawnPoint);
    }
}
