using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

public class SaveGame : MonoBehaviour
{ 
    //Gamecontroller değişkenleri
    public static SaveGame control;
    
    //Nesneler
    public ShipController ship;

    //kayıt edilen değişkenleri
    public int level;
    public int gold;
    public int paddleLevel;
    public int paddleHealth;
    public int peopleOnSecondIsland;
    public bool isGameStarted = false;
    public bool isGameEnded = false;
    
    //Kayıt Edilmeyen Değişkenler
    public int peopleOnBoard;
    public int maxLevelOfPaddleFromShip;
    public GameObject[] inGameUI;
    public GameObject[] startGameUI;
    public GameObject[] endGameUI;
    public TextMeshProUGUI goldText;

    void Awake(){ 
        if (control != null)
        {
            return;

        }
        control = this;
        Load();
        ship = GameObject.FindWithTag("Player").GetComponent<ShipController>();
        maxLevelOfPaddleFromShip = ship.allPaddles.Length;
        ship.levelOfPaddle = paddleLevel;
        ship.ShipPaddleVisualUpdater();
    }
    
    void Start(){
        GoldUITextUpdater();
    }

    private void Update(){
        if (isGameEnded){
            GameUIActivator(endGameUI, true);
        }
    }
    
    //People board ship
    public void BoardPeopleToShip(int people){
        peopleOnBoard += people;
        ship.numberOfPeople = peopleOnBoard;
    }
    
    //When player reach second island people will do unboard
    public void UnboardPeopleOnShip(int peopleCount){
        peopleOnSecondIsland += peopleCount;
    }

    //Coin UI text will be updated
    public void GoldUITextUpdater(){
        goldText.text = "" + gold;
    }

    
    //If has enough gold 
    public bool CheckGold(int multi){
        if (multi <= 0){
            multi = 1;
        }
        if (gold >= multi){
            return true;
        } else{
            return false;
        }
    }
    
    //This method will make Game UI elements active and disable
    public void GameUIActivator(GameObject[] uiElements, bool modifier){
        if (uiElements.Length > 0){
            foreach (var x in uiElements){
                x.SetActive(modifier);
            }
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        PlayerData data = new PlayerData();

        data.level = level;
        data.gold = gold;
        data.paddleLevel = paddleLevel;
        data.paddleHealth = paddleHealth;
        data.peopleOnSecondIsland = peopleOnSecondIsland;
        data.isGameEnded = isGameEnded;
        data.isGameStarted = isGameStarted;

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

            gold = data.gold;
            level = data.level;
            paddleLevel = data.paddleLevel;
            paddleHealth = data.paddleHealth;
            peopleOnSecondIsland = data.peopleOnSecondIsland;
            isGameStarted = data.isGameStarted;
            isGameEnded = data.isGameEnded;

        }
    
    }
}
[Serializable]
class PlayerData
{
    public int level;
    public int gold;
    public int paddleLevel;
    public int paddleHealth;
    public int peopleOnSecondIsland;
    public bool isGameStarted;
    public bool isGameEnded;
}