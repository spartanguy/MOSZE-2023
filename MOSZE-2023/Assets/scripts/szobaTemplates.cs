using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaTemplates : MonoBehaviour
{

    public GameObject[] megnyitFent;
    public GameObject[] megnyitJobb;
    public GameObject[] megnyitLent;
    public GameObject[] megnyitBal;
    public GameObject zaro;

    public List<GameObject> szobak = new List<GameObject>();

    public float varakIdo;
    public GameObject BOSS;
    private bool spawnedBOSS;

    public GameObject NPC;
    private bool spawnedNPC;



    void Update(){
        if(varakIdo<=0 && spawnedBOSS==false){
            for(int i=0; i<szobak.Count; i++){
                if(i==szobak.Count-1){
                    Instantiate(BOSS, szobak[i].transform.position, Quaternion.identity);
                    spawnedBOSS=true;
                };
            }
        }else{
            varakIdo-=Time.deltaTime;
        };

        for (int k=0; k<2; k++){
            if(varakIdo<=(varakIdo/2) && spawnedNPC==false){
                for(int j=0; j<(szobak.Count); j++){
                    if(j==szobak.Count-4){
                    Instantiate(NPC, szobak[j].transform.position, Quaternion.identity);
                    spawnedNPC=true;
                        }
                    }       
                }else if(varakIdo<=0 && spawnedNPC==true){
                for(int j=0; j<(szobak.Count); j++){
                    if(j==szobak.Count-4){
                        Instantiate(NPC, szobak[j].transform.position, Quaternion.identity);
                        spawnedNPC=true;
                    }
                } 
            } else {
            varakIdo-=Time.deltaTime;
            }
        };   
    }
}