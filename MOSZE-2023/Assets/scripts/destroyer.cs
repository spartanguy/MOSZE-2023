using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    public string fallIrany;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Szoba")){
            Destroy(other.gameObject);
            }
        }
}