using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falTorol : MonoBehaviour
{
    //hogy a spawn szoba egyik kijárata se záródjon be semmiképpen se az esetleg oda spwnoló falakat ezzel töröljük
    void OnTriggerEnter2D (Collider2D other){
            if(other.gameObject.CompareTag("fal")){         
            Destroy(other.gameObject);                      
        }

    }

}
