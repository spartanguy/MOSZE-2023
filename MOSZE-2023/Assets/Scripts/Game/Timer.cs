using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Timer : MonoBehaviour {

    private float timer = 0;
    
    public static Timer Instance { get; set; }

    private void Awake() {
        Instance = this;
    }

    public void StartTimer() {
        timer = 0;
    }

    void Update() {
        if (!Game.Instance.playing) return;
        timer += Time.deltaTime;
    }

    public int GetMinutes() {
        return (int) Mathf.Floor(timer / 60);
    }
    
}
