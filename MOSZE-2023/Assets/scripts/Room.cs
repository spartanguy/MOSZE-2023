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
    private List<GameObject> falak;

    
    public GameObject falV;
    public GameObject falF;

    public int falIrany;

    private void Awake() {
        enemies = new List<GameObject>();
        roomSize = 6f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (started){
            Kamera_kontroller.instance.aktualSzoba = this;
            return;
        }
        if (other.gameObject.CompareTag("jatekos")) {
            Kamera_kontroller.instance.aktualSzoba = this;
            started = true;
            if (falIrany == 2){
                falak.Add(Instantiate(falV, transform.position, Quaternion.identity));
            } else if (falIrany == 1){
               falak.Add(Instantiate(falF, transform.position, Quaternion.identity));
            };
            SpawnEnemies();
            
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
            Debug.Log("Goodbye");
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
        int n = Game.Instance.GetEnemyAmount();
        for (int i = 0; i < n; i++) {
            SpawnOneEnemy();
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
        for (int i = 0; i < falak.Count; i++) {
            Destroy(falak[i]);
        }
    }
    public Vector3 szobaKozepe()
    {
        return new Vector3(this.transform.position.x, this.transform.position.y);
    }
}
