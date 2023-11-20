using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaSpawner : MonoBehaviour
{
    public int megnyitIrany;
    private szobaTemplates templates;
    private int random;
    private bool spawned=false;
    public float waitTime = 4f;



    void Start(){
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.1f);
    }

    void Spawn(){
        templates = GameObject.FindGameObjectWithTag("Szoba").GetComponent<szobaTemplates>();
        if(spawned == false){
            if(megnyitIrany==1){
                random = Random.Range(0, templates.megnyitLent.Length);
                Instantiate(templates.megnyitLent[random], transform.position, templates.megnyitLent[random].transform.rotation);
            }else if(megnyitIrany==2){
                random = Random.Range(0, templates.megnyitBal.Length);
                Instantiate(templates.megnyitBal[random], transform.position, templates.megnyitBal[random].transform.rotation);
            }else if(megnyitIrany==3){
                random = Random.Range(0, templates.megnyitFent.Length);
                Instantiate(templates.megnyitFent[random], transform.position, templates.megnyitFent[random].transform.rotation);
            }else if(megnyitIrany==4){
                random = Random.Range(0, templates.megnyitJobb.Length);
                Instantiate(templates.megnyitJobb[random], transform.position, templates.megnyitJobb[random].transform.rotation);
            }    
        spawned=true;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
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
