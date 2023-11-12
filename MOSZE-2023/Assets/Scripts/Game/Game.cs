using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour {
    public PhraseList mainList;
    public GameObject player;
    private Vector3 Spwn;
    public int score = 0;
    public GameObject[] enemies;
    public bool playing;
    public static Game Instance { get; set; }
    private void Awake() {
        mainList = new PhraseList();
        Instance = this;
        Spwn = new Vector3(0,0,0);
        StartGame();
    }
    public void StartGame() {
        GameObject p = Instantiate(player, Spwn, Quaternion.identity);
        Invoke("",0.1f);
        playing = true;
    }

    public int GetFirerateMultiplier() {
        int f = Mathf.FloorToInt(Timer.Instance.GetMinutes() / 1); //#3
        if (f == 0){return 0;}
        else {return f;}
    }

    public int GetSpeedMultiplier() {
         int f = Mathf.FloorToInt(Timer.Instance.GetMinutes() / 1); //#1
        if (f == 0){return 0;}
        else {return f;}
    }

    public int GetEnemyAmount() {
        int min = Timer.Instance.GetMinutes();
        int mi = 1 + (int) Mathf.Floor(min / 2);
        int ma = (int) Mathf.Floor(min / 1.5f);
        if (ma < 3) ma = 3;
        if (ma > 10) ma = 10;
        if (mi < 2) mi = 2;
        return Random.Range(mi, ma);
    }

    public GameObject GetEnemy() {
        int min = Timer.Instance.GetMinutes() / 4;
        int m = 2 + min;
        if (m > enemies.Length) m = enemies.Length;
        int max = Random.Range(0, m);
        return enemies[max];
    }

    public int GetEnemyHealth() {
        int min = Timer.Instance.GetMinutes() / 1; //3
        return min;
    }
}
