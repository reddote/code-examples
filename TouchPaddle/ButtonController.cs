using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour{

    public void GameStarter(){
        if (!SaveGame.control.isGameStarted){
            SaveGame.control.isGameStarted = true;
            SaveGame.control.GameUIActivator(SaveGame.control.startGameUI, false);
            SaveGame.control.GameUIActivator(SaveGame.control.inGameUI, true);
            CameraBack.control.isBack = true;
        }
    }

    public void PeopleModifier(){
        int temp = SaveGame.control.peopleOnBoard;
        if (SaveGame.control.CheckGold(temp)){
            ModifierFixer(temp);
            SaveGame.control.BoardPeopleToShip(1);
        }
    }

    public void PaddleHealthModifier(){
        int temp = SaveGame.control.paddleHealth * 2;
        if (SaveGame.control.CheckGold(temp)){
            ModifierFixer(temp);
            SaveGame.control.paddleHealth += 1;
        }
    }

    public void PaddleLevelModifier(){
        int temp = SaveGame.control.paddleLevel * 3;
        if (SaveGame.control.CheckGold(temp)){
            if (SaveGame.control.paddleLevel < SaveGame.control.maxLevelOfPaddleFromShip){
                ModifierFixer(temp);
                SaveGame.control.paddleLevel += 1;
                SaveGame.control.ship.levelOfPaddle = SaveGame.control.paddleLevel;
                SaveGame.control.ship.ShipPaddleVisualUpdater();
            }
        }
    }

    public void LevelModifier(){
        
    }
    
    //clean kod için eklendi.
    private void ModifierFixer(int temp){
        if (temp <= 0){
            temp = 1;
        }
        SaveGame.control.gold -= temp;
        SaveGame.control.GoldUITextUpdater();
    }//buraya kadar
}