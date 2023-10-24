using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour {
    
    public GameObject player;
    private Vector3 Spwn;
    public GameObject[] enemies;
    public bool playing;
    public static Game Instance { get; set; }
    private void Awake() {
        Instance = this;
        Spwn = new Vector3(0,0,0);
    }
    public void StartGame() {
        GameObject p = Instantiate(player, Spwn, Quaternion.identity);
        Timer.Instance.StartTimer();
        playing = true;
    }

    public float GetFirerateMultiplier() {
        float f = Timer.Instance.GetMinutes() / 3;
        if (f == 0) f = 1;
        
        if (f == 5) f = 5;
        return 5 / f;
    }

    public float GetSpeedMultiplier() {
        float f = Timer.Instance.GetMinutes() / 30;
        if (f > 0.5f) f = 0.5f;
        
        return 0.5f + f;
    }

    public int GetEnemyAmount() {
        int min = Timer.Instance.GetMinutes();
        int mi = 1 + (int) Mathf.Floor(min / 2);
        int ma = (int) Mathf.Floor(min / 1.5f);
        if (ma < 3) ma = 3;
        if (ma > 10) ma = 10;
        if (mi < 2) mi = 2;
        print("min: " + mi + ", m: " + ma);
        return Random.Range(mi, ma);
    }

    public GameObject GetEnemy() {
        int min = Timer.Instance.GetMinutes() / 4;
        int m = 2 + min;
        if (m > enemies.Length) m = enemies.Length;
        print("max: " + m);
        int max = Random.Range(0, m);
        print("r: " + max);

        return enemies[max];
    }

    public int GetEnemyHealth() {
        int min = Timer.Instance.GetMinutes() / 3;
        return min;
    }
}
