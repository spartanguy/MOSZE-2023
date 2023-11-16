/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ajtoMozg : MonoBehaviour
{
    public GameObject falV;
    public GameObject falF;

    public int falIrany;


    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("jatekos")){
            if (falIrany == 2){
                Instantiate(falV, transform.position, Quaternion.identity);
           } else if (falIrany == 1){
               Instantiate(falF, transform.position, Quaternion.identity);
           }
        }
    }
}*/