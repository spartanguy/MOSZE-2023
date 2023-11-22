using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falTorol : MonoBehaviour
{

    void OnTriggerEnter2D (Collider2D other){
            if(other.gameObject.CompareTag("fal")){
            Destroy(other.gameObject);
        }

    }

}
