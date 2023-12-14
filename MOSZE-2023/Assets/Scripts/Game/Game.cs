using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

/*A jaték lefolyását irányító class.*/
public class Game : MonoBehaviour
{
    /* A Game egy singleton osztály, hogy könnyen tudjon rá hivatkozni a többi osztály.
    mainList a mainList a phraseList osztály objektuma, ebből kapnak az NPC-k történet elemeket.
    player, játékos objektum.
    Spwn, játékos születési pontja.
    score, a játékban elért pontot számolja.
    enemies, egy lista amiben tároljuk az ellenfél objektumokat.
    Boss, a boss játékobjektum.
    sceneName, aktuális scene neve.
    win, lose ezek a játékobjektumok akkor jelennek meg, mikor a játékos veszít vagy nyer.*/
    public PhraseList mainList;
    public GameObject player;
    private Vector3 Spwn;
    public int score = 0;
    public List<GameObject> enemies;
    public GameObject Boss;
    public bool playing;
    public static Game Instance { get; set; }
    public string sceneName;
    public GameObject win;
    public GameObject lose;
    public GameObject storyCanvas;

    //Kezdéskor beállítódik a scene neve és elindul a játék.
    private void Awake() {
        sceneName = SceneManager.GetActiveScene().name;
        mainList = new PhraseList();
        Instance = this;
        Spwn = new Vector3(0,0,0);
        StartGame();
    }

    //Lerakódik a játékos és a playing true-ra válik.
    public void StartGame() {
        Instantiate(player, Spwn, Quaternion.identity);
        Player.Instance.moveSpeed = 0;
        Invoke("",0.1f);
    }


    void FixedUpdate() {
        if (!playing) {
            if(Input.GetKeyDown(KeyCode.E)){
                storyCanvas.SetActive(false);
                playing = true;
                Player.Instance.moveSpeed = 5;
                Timer.Instance.timer = 0;
            }
        }
    }

    //timer segítségével meghatározzuk a firemultiplayer értékét.
    public int GetFirerateMultiplier() {
        int f = Mathf.FloorToInt(Timer.Instance.GetMinutes() / 3); //#3
        if (f == 0){return 0;}
        else {return f;}
    }

    //timer segítségével meghatározzuk a speedMultiplayer értékét.
    public int GetSpeedMultiplier() {
         int f = Mathf.FloorToInt(Timer.Instance.GetMinutes() / 3); //#1
        if (f == 0){return 0;}
        else {return f;}
    }

    //timer segítségével meghatározzuk a az ellenfelek számát.
    public int GetEnemyAmount() {
        int min = Timer.Instance.GetMinutes();
        int mi = 1 + (int) Mathf.Floor(min / 3);
        int ma = (int) Mathf.Floor(min / 1.5f);
        if (ma < 3) ma = 3;
        if (ma > 10) ma = 10;
        if (mi < 2) mi = 2;
        return Random.Range(mi, ma);
    }

    //Kiválasztunk egyet az ellenfelek listájából.
    public GameObject GetEnemy() {
        int e = Random.Range(0,enemies.Count);
        return enemies[e];
    }

    //timer segítségével meghatározzuk a enemyhealth értékét.
    public int GetEnemyHealth() {
        int min = Timer.Instance.GetMinutes() / 3; //3
        return min;
    }

    //Score getter
    public int GetScore() {
        return score;
    }
}
