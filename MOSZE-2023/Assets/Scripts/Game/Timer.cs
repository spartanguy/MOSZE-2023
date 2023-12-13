using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Timer class felel az idő elteltének nyomonkövetéséért.
public class Timer : MonoBehaviour {

    /*timer maga az idő követés értéke.
    A timer is egy singleton class, hogy könnyen tudjon rá hivatkozni a többi class.
    tiertext egy textmesh ami megjelenik a képernyőn.*/
    public float timer = 0;
    public static Timer Instance { get; set; }
    public TMP_Text timerText;
    private void Awake() {
        Instance = this;
    }

    //Timer indítása.
    public void StartTimer() {
        timer = 0;
    }
    
    //timer képernyőn létének frissitése.
    void Update() {
        if (!Game.Instance.playing) return;
        timer += Time.deltaTime;
        DisplayTime(timer);
    }

    //A képernyőn elhelyezett timer formázása.
    void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    // visszaadja a perceket.
    public int GetMinutes() {
        return (int) Mathf.Floor(timer / 60);
    }

    //visszaadja a másodperceket.
    public int GetSeconds(){
        return (int) Mathf.Floor(timer);
    }
}
