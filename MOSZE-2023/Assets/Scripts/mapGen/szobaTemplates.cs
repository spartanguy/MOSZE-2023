using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class szobaTemplates : MonoBehaviour, IDataPersistence
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

    public GameObject NPC;
    private bool spawnedNPC;
    private int szobaDb, szobaHely;

    void Update(){
        if(Game.Instance.sceneName == "NewGame")
        {
            if(varakIdo<=0 && spawnedBOSS==false){
                for(int i=0; i<szobak.Count; i++){
                    if(i==szobak.Count-1){
                        GameObject boss = Instantiate(BOSS, szobak[i].transform.position, Quaternion.identity);
                        Room szob = (Room)szobak[i].gameObject.GetComponentInChildren(typeof(Room));
                        szob.szobaType = "BOSS";
                        boss.SetActive(false);
                        spawnedBOSS=true;
                    };
                }
            }else{
                varakIdo-=Time.deltaTime;
            };

            if (varakIdo<=0 && spawnedNPC == false){
                szobaDb  =((szobak.Count-2)/2);
                for(int j=0; j<=szobaDb; j++){
                    szobaHely = (Random.Range(1, szobak.Count-2));
                    if (szobak[szobaHely] != null)
                    {
                        Instantiate(NPC, szobak[szobaHely].transform.position, Quaternion.identity);
                        Room szob = (Room)szobak[szobaHely].gameObject.GetComponentInChildren(typeof(Room));
                        szob.szobaType = "NPC";
                    }
                    if(j==szobaDb){
                        spawnedNPC=true;
                    }
                }
            }
        }
    }

    public void LoadData(GameData data){
        if(Game.Instance.sceneName == "LoadedGame")
        {
            for(int i = 0; i<data.RoomDataList.Count; i++){
                string path = "mapPrefab/" + data.RoomDataList[i].prefabName;
                GameObject prefab = Resources.Load<GameObject>(path) as GameObject;
                Vector2 coor = new Vector2(data.RoomDataList[i].xCoord,data.RoomDataList[i].yCoord);
                GameObject currentRoom  = Instantiate(prefab,coor,Quaternion.identity);
                Room szob = (Room)currentRoom.gameObject.GetComponentInChildren(typeof(Room));
                szob.szobaType = data.RoomDataList[i].roomType;
                if (szob.szobaType == "NPC")
                {
                    Instantiate(NPC, currentRoom.transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void SaveData(ref GameData data){

        data.ListDeclaration(); 
        for (int i = 0; i<szobak.Count; i++){
            if (szobak[i] == null){szobak.RemoveAt(i);}
            data.asd.xCoord = szobak[i].transform.position.x;
            data.asd.yCoord = szobak[i].transform.position.y;
            Room szob = (Room)szobak[i].gameObject.GetComponentInChildren(typeof(Room));
            data.asd.roomType = szob.szobaType;
            data.asd.prefabName = szob.prefabName; 
            data.RoomDataList.Add(data.asd);
        }   
    }
}