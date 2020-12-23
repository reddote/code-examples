using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIButtonController : MonoBehaviour{
    
    public void PlayButton(){
        SaveGame.control.LevelChanger();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReplayButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartPlayButton(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}