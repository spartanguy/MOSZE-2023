using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    //itt kell beállítani, hogy függőleges vagy vízszintes irányú ajtót kell majd spawnolni
    public string ajtoForg; 
                              
    //ez a kod felelős az esetleg egymásra spawnolo szobák törléséért
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Szoba")){     
            Destroy(other.gameObject);
        }
    }
}