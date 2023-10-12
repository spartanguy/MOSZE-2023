using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        SceneManager.LoadScene("GameOverlay");
    }

    public void ExitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
