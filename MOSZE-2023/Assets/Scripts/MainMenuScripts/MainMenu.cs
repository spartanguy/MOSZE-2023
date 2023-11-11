using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Levels to Load")]
    public string newGameLevel;
    private string levelToLoad;

    [SerializeField] private GameObject noSavedGameDialog = null;
    public void NewGameYes(){

        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameYes(){

        if(PlayerPrefs.HasKey("SavedLevel")) {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        } else {
            noSavedGameDialog.SetActive(true);
        }
    }
    public void Exit(){
        Application.Quit();
    }
}
