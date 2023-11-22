using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakUtca : MonoBehaviour
{
    public GameObject fal;
    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("zsakUtca")){
            Instantiate(fal, transform.position, Quaternion.identity); 
        }    
    }
}
