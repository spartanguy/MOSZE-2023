using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool started;
    private float roomSize;
    [SerializeField] 
    private List<GameObject> enemies;
    [SerializeField] 
    private List<GameObject> ajtok;

    public List<GameObject> ajtoHely; 

    public string szobaType = "harc";


    public GameObject ajto;
    private void Awake() {
        enemies = new List<GameObject>();
        roomSize = 6f;
        if(transform.position.x == 0 && transform.position.y == 0){
            szobaType = "kezdo";
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (started){
            Kamera_kontroller.instance.aktualSzoba = this;
            return;
        }
        if (other.gameObject.CompareTag("jatekos")) {
            started = true;
            if (szobaType == "harc"){
                Kamera_kontroller.instance.aktualSzoba = this;
                for(int i=0; i<ajtoHely.Count; i++){
                    ajtok.Add(Instantiate(ajto, ajtoHely[i].transform.position, Quaternion.identity));
                }
                SpawnEnemies();     
            }

            if(szobaType == "NPC"){
                started = true;
                Kamera_kontroller.instance.aktualSzoba = this;
            }

        }
    }
    private void FixedUpdate() {
        if (!started)
        {
            return;
        }
        CheckEnemyList();
        if (enemies.Count == 0) 
        {
            FinishRoom();
            return;
        }
    }
    private void CheckEnemyList() {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }
    private void SpawnEnemies() {
        if (GameObject.FindGameObjectWithTag("Szoba")){
            int n = Game.Instance.GetEnemyAmount();
            for (int i = 0; i < n; i++) {
                SpawnOneEnemy();
            }
        }
    }
    private void SpawnOneEnemy() {
        GameObject e = SelectEnemy();
        Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 2, roomSize / 2),Random.Range(-roomSize / 2, roomSize / 2), 0));
        enemies.Add(Instantiate(e, p, Quaternion.identity));
    }
    private GameObject SelectEnemy() {
        return Game.Instance.GetEnemy();
    }
    void FinishRoom()
    {
        for (int i = 0; i < ajtok.Count; i++) {
            Destroy(ajtok[i]);
        }
    }
    public Vector3 szobaKozepe()
    {
        return new Vector3(this.transform.position.x, this.transform.position.y);
    }
}
