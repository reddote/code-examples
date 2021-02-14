using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class HandsDeck{
    public string trump;
    public int[] hands_p1;
    public int[] hands_p2;
    public int[] hands_p3;
    public int[] hands_p4;
}
public class JsonLoader : MonoBehaviour{
    public string jsonFileName;//resources yüklenen json dosyasının adı (uzantısı olmadan)
    public CardPlacer playerOne;
    public CardPlacer playerTwo;
    public CardPlacer playerThree;
    public CardPlacer playerFour;
    private string _cardJson;
    void Start(){
        _cardJson = File.ReadAllText(Application.dataPath + "/Resources/"+ jsonFileName +".json");
        HandsDeck handsDeck = JsonUtility.FromJson<HandsDeck>(_cardJson);
        playerOne.PlaceCards(handsDeck.hands_p1, handsDeck.trump);
        playerTwo.PlaceCards(handsDeck.hands_p2, handsDeck.trump);
        playerThree.PlaceCards(handsDeck.hands_p3, handsDeck.trump);
        playerFour.PlaceCards(handsDeck.hands_p4, handsDeck.trump);
    }

}