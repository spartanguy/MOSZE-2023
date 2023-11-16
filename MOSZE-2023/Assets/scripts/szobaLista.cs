using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaLista : MonoBehaviour
{
    public szobaTemplates templates;

    void Start(){
        templates=GameObject.FindGameObjectWithTag("Szoba").GetComponent<szobaTemplates>();
        templates.szobak.Add(this.gameObject);
    }
    
}
