using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class MainMenu : MonoBehaviour
{
    public string newGameLevel;
    private string levelToLoad;

    [SerializeField] private GameObject noSavedGameDialog = null;
    // Ez a funkcio vált át a MainMenu Sceneről a MainGame Scene-re, amikor a New Game gombra nyom a user
    public void NewGameYes(){
        SceneManager.LoadScene("NewGame");
    }
    // Ez a funkció fogja ellenőrizni, hogy van-e mentett játék, ha van akkor betölti azt.
    public void LoadGameYes(){
        string fileLocation = Path.Combine(Application.dataPath, "savefiles.json");
        if(File.Exists(fileLocation)) {
            SceneManager.LoadScene("LoadedGame");
        } else {
            noSavedGameDialog.SetActive(true);
        }
    }
    //Ezzel a funkcióval lép ki a user a játékból, az Exit gomb megnyomásával
    public void Exit(){
        Application.Quit();
    }
}
