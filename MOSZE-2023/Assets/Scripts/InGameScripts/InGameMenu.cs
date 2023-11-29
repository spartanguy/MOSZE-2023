using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject ingameMenu;

    //Ez a funkció figyeli, hogy megnyomták-e az IngameMenu előhozó billentyűt.
    void Update () {
        if(Input.GetKeyDown(KeyCode.L)) {
            if(GameIsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }
    //Ez a funkció fogja az IngameMenu-t eltuntetni, és ezzel folytatódik aj áték
    public void Resume(){
        ingameMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //Ez a funkció állítja le a játékot az InGameMenu elhozatalakor.
    void Pause() {
        ingameMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    //Ezzel a funkcióval lehet visszamenni a MainMenübe
    public void BackToMainmenu() {
        SceneManager.LoadScene("NewMainMenu");
    }
}
