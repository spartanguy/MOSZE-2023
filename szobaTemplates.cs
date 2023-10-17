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

    public List<GameObject> szobak;

    public float varakIdo;
    public GameObject BOSS;
    private bool spawnedBOSS;

    void Update(){
        if(varakIdo<=0 && spawnedBOSS==false){
            for(int i=0; i<szobak.Count; i++){
                if(i==szobak.Count-1){
                    Instantiate(BOSS, szobak[i].transform.position, Quaternion.identity);
                    spawnedBOSS=true;
                }
            }
        }else{
            varakIdo-=Time.deltaTime;
        }
    }
}
