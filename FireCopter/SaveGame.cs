using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGame : MonoBehaviour
{ 
    //Gamecontroller değişkenleri
    public static SaveGame control;
    
    //Nesneler
    public HelicopterController helicopterController;
    public GameControl gameControl;
    public MissionWaypoint waterTarget;
    public FireWaypoint fireTarget;
    public Transform firstFire;
    public HouseWaypoint houseTarget;
    public GameObject[] levelPrefabs;


    //kayıt edilen değişkenleri
    public int level;
    public bool isFailed = false;

    void Awake(){ 
        if (control != null)
        {
            return;

        }
        control = this;
        Load();
        MapSpawner();
        gameControl.trees = FindObjectsOfType<FireSpreadController>();
        firstFire = gameControl.FindFirstFire();
    }
    
    void Start(){
        TargetWaypoint();
        Time.timeScale = 0;
    }

    private void Update(){
        
    }

    //level Changer
    public void LevelChanger(){
        level += 1;
        if (level > 9){
            level = 0;
        }
        Save();
    }

    //Targets waypoint maker
    public void TargetWaypoint(){
        waterTarget.target = GameObject.FindWithTag("MisWater").transform;
        fireTarget.target = firstFire;
    }
    
    //Harita spawnlatıcı
    private void MapSpawner(){
        Instantiate(levelPrefabs[level], Vector3.zero, Quaternion.Euler(-90f, 0f,0f));
        helicopterController.HelicopterSpawner();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        PlayerData data = new PlayerData();

        data.level = level;
        data.isFailed = isFailed;

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
       
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat",
                                        FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            level = data.level;
            isFailed = data.isFailed;

        }
    
    }
}
[Serializable]
class PlayerData
{
    public int level;
    public bool isFailed;
}