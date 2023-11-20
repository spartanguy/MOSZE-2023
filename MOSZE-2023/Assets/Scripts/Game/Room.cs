using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool started;
    private float roomSize;
    [SerializeField] 
    private List<GameObject> enemies;

    private void Awake() {
        enemies = new List<GameObject>();
        roomSize = 3f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (started) return;
        if (other.gameObject.CompareTag("Player")) {
            started = true;
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
        Destroy(this);
    }
}
