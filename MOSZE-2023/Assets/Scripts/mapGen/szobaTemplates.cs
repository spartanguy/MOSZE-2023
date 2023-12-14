using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class szobaTemplates : MonoBehaviour, IDataPersistence
{
    //szobák megnyitási irány szerint csoportosítva, innen valasztja ki a szobaSpawner hogy random melyiket helyezi le 
    public GameObject[] megnyitFent;
    public GameObject[] megnyitJobb;
    public GameObject[] megnyitLent;
    public GameObject[] megnyitBal;
    public GameObject zaro;

    public List<GameObject> szobak;

    //várakozási idő, ami után a különleges (NPC, BOSS, MINIBOSS) szobákat helyezi el a program
    public float varakIdo;

    //a különleges szobák változói
    public GameObject BOSS;
    private bool spawnedBOSS;

    public GameObject NPC;
    private bool spawnedNPC;

    public GameObject MINIBOSS;
    private bool spawnedMiniboss;

    private int szobaDb, szobaHely;
    private bool isSaved = false;



    //a fent említett különleges szobák elhelyezései random, vagy a BOSS esetén az utolsó spawnolt szobába
    void Update(){

        /*new game után a szobákat elkezdi spawnolni, majd ha lejárt a várakozási idő, és még nincs boss, az utolsó lespawnolt szobába beletesszük
          ugyanez igaz az npc és miniboss szobákra, annyi kikötéssel, hogy ott még ellenőrizzük, hogy ne tegyen egymásra két ugyanolyan szobát, és 
          limitálva van darabszámuk is */
    
        if(Game.Instance.sceneName == "NewGame")
        {
            if(varakIdo<=(-3) && spawnedBOSS==false ){
                        Room szob = (Room)szobak[szobak.Count-1].gameObject.GetComponentInChildren(typeof(Room));
                        szob.szobaType = "BOSS";
                        spawnedBOSS=true;
                        szobaLista szobaL = (szobaLista)szobak[szobak.Count-1].gameObject.GetComponent(typeof(szobaLista));
                        szobaL.szobaFoglalt = true;
            }else{
                varakIdo-=Time.deltaTime;
            };

            if (varakIdo<=0 && spawnedNPC == false){
                szobaDb  =((szobak.Count-2)/2);
                for(int j=0; j<=szobaDb; j++){
                    szobaHely = (Random.Range(1, szobak.Count-2));
                    if (szobak[szobaHely] != null)
                    {
                        szobaLista szobaL = (szobaLista)szobak[szobaHely].gameObject.GetComponent(typeof(szobaLista));
                        if(szobaL.szobaFoglalt == false){
                            Instantiate(NPC, szobak[szobaHely].transform.position, Quaternion.identity);
                            Room szob = (Room)szobak[szobaHely].gameObject.GetComponentInChildren(typeof(Room));
                            szob.szobaType = "NPC";
                            szobaL.szobaFoglalt = true;
                        }
                    }
                    if(j==szobaDb){
                        spawnedNPC=true;
                    }
                }
            }

            if (varakIdo<=(-1) && spawnedMiniboss == false){
                szobaDb  = 3;
                for(int j=0; j<=szobaDb; j++){
                    szobaHely = (Random.Range(1, szobak.Count-2));
                    if (szobak[szobaHely] != null)
                    {
                        szobaLista szobaL = (szobaLista)szobak[szobaHely].gameObject.GetComponent(typeof(szobaLista));
                        if(szobaL.szobaFoglalt == false){
                            Room szob = (Room)szobak[szobaHely].gameObject.GetComponentInChildren(typeof(Room));
                            szob.szobaType = "MINIBOSS";
                            szobaL.szobaFoglalt = true;
                        } else {
                            while(szobaL.szobaFoglalt == true){
                                szobaHely = (Random.Range(1, szobak.Count-2));
                                szobaL = (szobaLista)szobak[szobaHely].gameObject.GetComponent(typeof(szobaLista));
                            } 
                            Room szob = (Room)szobak[szobaHely].gameObject.GetComponentInChildren(typeof(Room));
                            szob.szobaType = "MINIBOSS";
                            szobaL.szobaFoglalt = true;
                        }
                    }
                    if(j==szobaDb){
                        spawnedMiniboss=true;
                    }
                }
            }

        }
        if (varakIdo <= -5 && isSaved == false)
        {
            DataPersistenceManager.instance.SaveGame();
            isSaved = true;
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
                if (szob.szobaType == "NPC"){
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