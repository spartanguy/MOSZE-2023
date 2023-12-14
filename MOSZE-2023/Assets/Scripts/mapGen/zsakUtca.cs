using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakUtca : MonoBehaviour
{
    //a random szoba spawn miatt előfordulhat olyan, hogy egyik szoba egy másiknak kívülről nekivezetné a játékost,
    //ezért az ilyen zsakutcákat egy a szoba falával megegyező de a kijáratot elfedő méretű darabbal lezárjuk
    public GameObject fal;
    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("zsakUtca")){
            Instantiate(fal, transform.position, Quaternion.identity); 
        }    
    }
}
