using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaSpawner : MonoBehaviour
{
    //szobak megnyitasi iranyait 1-től 4-ig ebben adjuk meg
    public int megnyitIrany;
    private szobaTemplates templates;
    private int random;
    private bool spawned=false;
    public float waitTime = 4f;



    //a játek inditásáért felelős
    void Start(){
        if(Game.Instance.sceneName == "NewGame")
        {
                Destroy(gameObject, waitTime);
                Invoke("Spawn", 0.1f);
            }
    //    }
    }

    //a fent megadott megnyitási irányt figyelembe véve elkezdi egymás mellé spawnolni a megfelelő szobákat
    void Spawn(){
        templates = GameObject.FindGameObjectWithTag("Szoba").GetComponent<szobaTemplates>();
        if(spawned == false){
            if(megnyitIrany==1){
                random = Random.Range(0, templates.megnyitLent.Length);
                Instantiate(templates.megnyitLent[random], transform.position, Quaternion.identity);
            }else if(megnyitIrany==2){
                random = Random.Range(0, templates.megnyitBal.Length);
                Instantiate(templates.megnyitBal[random], transform.position, Quaternion.identity);
            }else if(megnyitIrany==3){
                random = Random.Range(0, templates.megnyitFent.Length);
                Instantiate(templates.megnyitFent[random], transform.position, Quaternion.identity);
            }else if(megnyitIrany==4){
                random = Random.Range(0, templates.megnyitJobb.Length);
                Instantiate(templates.megnyitJobb[random], transform.position, Quaternion.identity);
            }    
        spawned=true;
        }
    }

    //a spawn szekvencia végén a "semmibe" nyíló szobák után egy szoba méretű de teljesen zárt úgy nevezett záró szobával biztosítjuk, 
    //hogy ne lehessen a játékteret elhagyni
    void OnTriggerEnter2D(Collider2D other){
        if(Game.Instance.sceneName == "NewGame")
        {
            templates = GameObject.FindGameObjectWithTag("Szoba").GetComponent<szobaTemplates>();
            if(other.CompareTag("SpawnPoint")){
                if(other.GetComponent<szobaSpawner>().spawned==false && spawned==false){
                    Instantiate(templates.zaro, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                spawned=true;    
            }
        }
    }
}
