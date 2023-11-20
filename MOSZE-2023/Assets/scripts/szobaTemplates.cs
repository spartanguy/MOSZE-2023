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
    private int szobaDb, szobaHely;




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

        if (varakIdo<=0 && spawnedNPC == false){
            szobaDb  =((Random.Range(0, szobak.Count))/2)+1;
            Debug.Log(szobaDb);
            for(int j=0; j<=szobaDb; j++){
                szobaHely = (Random.Range(0, szobak.Count))+1;
                Debug.Log(szobaHely);
                Instantiate(NPC, szobak[szobaHely].transform.position, Quaternion.identity);
                if(j==szobaDb){
                    spawnedNPC=true;
                }
            }
        }
    }
}