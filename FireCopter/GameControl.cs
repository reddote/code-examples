using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameControl : MonoBehaviour{
    
    public FireSpreadController[] trees;
    [FormerlySerializedAs("endingCG")] public CanvasGroup endingCg;
    public GameObject levelFailed;
    public GameObject levelComplete;
    public GameObject playButton;
    public GameObject replayButton;
    public GameObject[] stars;
    private int _totalTreeValue;
    private int _burntTree;
    private bool _isGameEnded;

    void Start(){
        _totalTreeValue = trees.Length;
        endingCg.alpha = 0;
        endingCg.interactable = false;
    }

    //yanan ağaçları chekliyoruz.
    public void BurntTreeChecker(){
        _burntTree += 1;
    }

    public void EndGameCheck(){
        foreach (var x in trees){
            if (x.GetComponent<FireSpreadController>().isFireStarted){
                _isGameEnded = false;
                break;
            }
            _isGameEnded = true;
        }
        EndGameUI(_isGameEnded);
    }

    private void EndGameUI(bool check){
        if (check){
            float value = 100f * (_totalTreeValue - _burntTree) / (_totalTreeValue);

            endingCg.alpha = 1;
            endingCg.interactable = true;
            playButton.SetActive(true);
            levelComplete.SetActive(true);
            if (value >= 66 ){
                CheckStars(3);
            }else if (value >= 33){
                CheckStars(2);
            }else{
                CheckStars(1);  
            }
        }
    }

    private void CheckStars(int count){
        for (int i = 0; i < count; i++){
            stars[i].SetActive(true);
        }
    }

    public void HouseIsBurning(){
        SaveGame.control.isFailed = true;
        endingCg.alpha = 1;
        endingCg.interactable = true;
        replayButton.SetActive(true);
        levelFailed.SetActive(true);
    }
    
    //ilk yangını başlatan ağaç
    public Transform FindFirstFire(){
        Transform temp = null;
        for (int i = 0; i < trees.Length - 1; i++){
            if (trees[i].isFireStarted){
                temp = trees[i].transform;
            }
        }
        return temp;
    }
}
