using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float timer = 0;

    public static Timer Instance { get; set; }

    public TMP_Text timerText;

    private void Awake() {
        Instance = this;
    }

    public void StartTimer() {
        timer = 0;
    }
    
    void Update() {
        if (!Game.Instance.playing) return;
        timer += Time.deltaTime;
        DisplayTime(timer);
    }

    void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    public int GetMinutes() {
        return (int) Mathf.Floor(timer / 60);
    }
    public int GetSeconds(){
        return (int) Mathf.Floor(timer % 60);
    }
}
