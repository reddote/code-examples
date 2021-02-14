using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour{
    public SpriteRenderer[] allCards;
    public SpriteRenderer[] playerCards = new SpriteRenderer[13];
    public Transform allCardsArrayBuilder;

    private void Awake(){
        playerCards = transform.GetComponentsInChildren<SpriteRenderer>();
        allCards = allCardsArrayBuilder.GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(allCards.Length);
    }

    public void PlaceCards(int[] info, string trump){
        int lowID = 0;
        int highID = 0;
        int allCounter = 0;
        int playerCounter = 0;
        
        if (trump == "diamond"){
            lowID = 0;
            highID = 12;
        }else if (trump == "clup"){
            lowID = 13;
            highID = 25;
        }else if (trump == "heart"){
            lowID = 26;
            highID = 38;
        }else if (trump == "spade"){
            lowID = 39;
            highID = 51;
        } else{
            Debug.LogError("incorrect data entry (Trump : " + trump+ " )");
        }
        
        foreach (var x in info){
            if (x == 1){
                if (allCounter>=lowID && allCounter<=highID){
                    playerCards[playerCounter].sprite = allCards[allCounter].sprite;
                    playerCards[playerCounter].color = Color.yellow;
                } else{
                    playerCards[playerCounter].sprite = allCards[allCounter].sprite;
                }
                playerCounter++;
                if (playerCounter > 13){
                    break;
                }
            }
            allCounter++;
        }
    }
}