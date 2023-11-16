using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Ezzel a methoddal lehet az ingame menubol visszalepni a main menube 

    public static bool GameIsPaused = false;
    public GameObject ingameMenu;

    void Update () {
        if(Input.GetKeyDown(KeyCode.L)) {
            if(GameIsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }
    public void Resume(){
        ingameMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause() {
        ingameMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SaveGameMenu(){
        Debug.Log("Loading menuuu");
    }
    public void BackToMainmenu() {
        SceneManager.LoadScene("NewMainMenu");
    }
}
