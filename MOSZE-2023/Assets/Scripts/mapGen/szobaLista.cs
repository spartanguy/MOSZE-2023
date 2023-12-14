using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaLista : MonoBehaviour
{
    public bool szobaFoglalt = false;
    //kapcsolat a szobaTemplates kóddal
    public szobaTemplates templates;

    //a betöltés és mentéshez a lespawnolt szobákat itt listába szedjük
    void Start(){
        templates=GameObject.FindGameObjectWithTag("Szoba").GetComponent<szobaTemplates>();
        templates.szobak.Add(this.gameObject);
    }
}
