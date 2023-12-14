using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Room class felel a szobák viselkedéséért.
Ez az egyik legkomplexebb class a játékban.
A szoba típusok alapján beállítja a szobák összeállítását, illetve a Questeket.*/
public class Room : MonoBehaviour
{
    /* started változó vizsgálja a szoba el van e már kezdve
    started változó vizsgálja be van e már fejezeve
    weapondrop lista a miniboss szobák teljesítéséért járo fegyvereket tartalmazza
    enemies lista a szoba allenfeleit
    boxes, akkor van bent valami hogyha a megadaott questtel rendelkezik az NPC
    szobatype alapján határozzuk meg melyik szoba mi lesz.
    */
    private bool started;
    private bool finished = false;
    public List<GameObject> weaponDrop;
    private float roomSize;
    [SerializeField] 
    private List<GameObject> enemies;
    [SerializeField] 
    private List<GameObject> ajtok;

    public List<GameObject> ajtoHely; 

    public string szobaType = "harc";
    public string prefabName;

    public GameObject vajto;
    public GameObject fajto;
    public List<GameObject> boxes;
    Transform parent;
    string QuestName;


    private void Awake() {
        enemies = new List<GameObject>();
        boxes = new List<GameObject>();
        roomSize = 6f;
        if(transform.position.x == 0 && transform.position.y == 0){
            szobaType = "kezdo";
        }
    }

    /*Minden különböző fajtájú szoba beállítása
    NPC szobán belül a Quest is beállításra kerül és ez alapján kerülnek be az asettek*/
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (started && other.tag == "Player"){
            Kamera_kontroller.instance.aktualSzoba = this;
            return;
        }
        if (other.gameObject.CompareTag("Player")) {
            started = true;
            if (szobaType == "harc" && other.tag == "Player"){
                Kamera_kontroller.instance.aktualSzoba = this;
                SpawnDoors();
                SpawnEnemies();     
            }

            if(szobaType == "NPC" && other.tag == "Player"){
                started = true;
                Kamera_kontroller.instance.aktualSzoba = this;
                parent = transform.parent;
                GameObject npc = parent.GetChild(parent.childCount-1).gameObject;
                Npc npcScript =  (Npc) npc.GetComponent((typeof(Npc)));
                QuestName = npcScript.quest.GetQuestName();
                if (QuestName == "Moving The Chest")
                {
                    GameObject prefab = Resources.Load<GameObject>("mapPrefab/Chest") as GameObject;
                    Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 2, roomSize / 2),Random.Range(-roomSize / 2, roomSize / 2), 0));
                    GameObject chest = Instantiate(prefab,p,Quaternion.identity);
                    chest.transform.parent = this.transform.parent;
                    Box chestScript = (Box)chest.gameObject.GetComponent((typeof(Box)));
                    chestScript.keyQuestThing = true;
                }
                else if (QuestName == "Destroy the Items In The Room")
                {
                    GameObject prefab = Resources.Load<GameObject>("mapPrefab/Box") as GameObject;
                    for (int i = 0; i < 4; i++)
                    {
                        Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 2, roomSize / 2),Random.Range(-roomSize / 3, roomSize / 3), 0));
                        boxes.Add(Instantiate(prefab,p,Quaternion.identity));
                    }
                }
            }
            if(szobaType == "BOSS" && other.tag == "Player"){
                started = true;
                Kamera_kontroller.instance.aktualSzoba = this;
                SpawnDoors();
                Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 3, roomSize / 3),Random.Range(-roomSize / 3, roomSize / 3), 0));
                enemies.Add(Instantiate(Game.Instance.Boss, p, Quaternion.identity));      
            }

            if(szobaType == "MINIBOSS" && other.tag == "Player"){
                started = true;
                Kamera_kontroller.instance.aktualSzoba = this;
                SpawnDoors();
                Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 3, roomSize / 3),Random.Range(-roomSize / 3, roomSize / 3), 0));
                enemies.Add(Instantiate(Game.Instance.enemies[0], p, Quaternion.identity));
                GameObject miniBoss = enemies[0];
                Ranged script = (Ranged) miniBoss.GetComponent((typeof(Ranged)));
                script.health += 15;
                script.attackSpeedBuff += 1;   
            }
        }
    }

    /*Különböző kritériumok vizsgálata*/
    private void FixedUpdate() {
        if (!started && szobaType != "kezdo")
        {
            return;
        }
        CheckEnemyList();
        CheckBoxList();
        if (enemies.Count == 0 && !finished) 
        {
            FinishRoom();
            return;
        }
        if (boxes.Count == 0 && szobaType == "NPC" && QuestName == "Destroy the Items In The Room")
        {
            parent = transform.parent;
            GameObject npc = parent.GetChild(parent.childCount-1).gameObject;
            Npc npcScript =  (Npc) npc.GetComponent((typeof(Npc)));
            npcScript.isCompleted = true;
        }
    }

    //törli az üres elemeket az ellenfelek listából
    private void CheckEnemyList() {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null)
                enemies.RemoveAt(i);
        }
    }

    //törli az üres elemeket a boxes listából
    private void CheckBoxList() {
        for (int i = 0; i < boxes.Count; i++) {
            if (boxes[i] == null)
                boxes.RemoveAt(i);
        }
    }

    //Az ellenfelek megjelenítéséért felelős funkció
    private void SpawnEnemies() {
        if (GameObject.FindGameObjectWithTag("Szoba")){
            int n = Game.Instance.GetEnemyAmount();
            for (int i = 0; i < n; i++) {
                SpawnOneEnemy();
            }
        }
    }

    //Egy ellenfél megjelenéséért felelős funkció
    private void SpawnOneEnemy() {
        GameObject e = SelectEnemy();
        Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 3, roomSize / 3),Random.Range(-roomSize / 3, roomSize / 3), 0));
        enemies.Add(Instantiate(e, p, Quaternion.identity));
    }

    // A game classból kiválasztjuk az ellenfelet (ranged vagy melee)
    private GameObject SelectEnemy() {
        return Game.Instance.GetEnemy();
    }

    //Ha a szoba teljeseült leveszi az ajtókat, ha miniboss szoba volt adja a fegyver jutalmat
    void FinishRoom()
    {
        if (szobaType == "MINIBOSS")
        {
            int szam = Random.Range(0,weaponDrop.Count);
            Instantiate(weaponDrop[szam],transform.position,Quaternion.identity);
        }
        for (int i = 0; i < ajtok.Count; i++) {
            Destroy(ajtok[i]);
        }
        finished = true;
    }

    //kamerakontrollert segítő függvény, megkeresi a szoba közepét
    public Vector3 szobaKozepe()
    {
        return new Vector3(this.transform.position.x, this.transform.position.y);
    }

    //Ajtók behelyezése a szobába
    private void SpawnDoors()
    {
        for (int i=0; i<ajtoHely.Count; i++){
            if (ajtoHely[i].GetComponent<destroyer>().ajtoForg == "v")
            {
                ajtok.Add(Instantiate(vajto, ajtoHely[i].transform.position, Quaternion.identity));
            } 
            else if (ajtoHely[i].GetComponent<destroyer>().ajtoForg == "f")
            {
                ajtok.Add(Instantiate(fajto, ajtoHely[i].transform.position, Quaternion.identity));
            }
        }
    }

    //Az egyik küldetés célját lehelyező függvény
    public void SpawnBoxDestination()
    {
        GameObject prefab = Resources.Load<GameObject>("mapPrefab/BoxDestination") as GameObject;
        Vector3 p = (transform.position + new Vector3(Random.Range(-roomSize / 2, roomSize / 2),Random.Range(-roomSize / 2, roomSize / 2), 0));
        GameObject boxDest = Instantiate(prefab,p,Quaternion.identity);
        boxDest.transform.parent = this.transform.parent;
    }
}
