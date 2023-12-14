using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A történetelemek eltárolására készült class.
public class PhraseList
{
    /*phrases tárolja a történet részleteket amiket az npck mondhatnak nekünk.*/
    private List<string> phrases = new List<string>
    {
        "I'm glad we ran into each other again, it's been 1334 years since I've seen you.\n I saw you here with your father then",
        "What brings you here?\n You were declared dead in the Norville War 5 years ago...",
        "Be careful on your way!\n You'll have a hard time getting past Jack Winger.",
        "Jack Winger wants your head bad\n you're the only one who can beat him",
        "Since you disappeared our lives have been in ruins...\n Welcome back",
        "They want your life on every corner\n then watch your back on the road",
        "I see you got a chip in you...",
        "They told me about you\n I knew I could trust you! Thanks for your help!",
        "Jack Winger is crippling our lives...\n you are our only hope",
        "Watch out for the soldiers\n they're real dangerous mercenaries"
    };

    //Visszaad egyet a történet elemek közül.
    public string getPhrase(int szam)
    {
        return phrases[szam];
    }

}
/*Az npc-k karakterek akikkel interakcióba lehet lépni. 
Mesélnek kicsit majd ajánlanak egy Questet amit ha a játkéos teljesit jutalmat kap.*/
public class Npc : MonoBehaviour
{
    /*rewardlist a lehetséges jutalmak listája.
    monologe a történet részlet amit a phraseList classtól kap.
    quest a küldetés amit a játékosnak ad, a Quests classtól kapja.
    dialogHandler felel a képen megjelenő beszélgetésért.
    dialogBubble maga a beszélgetésbuborék.
    isAccepted, a player elfogata e a küldetést.
    isRevarded, ha megcsinálta megkapta-e érte a jutalmat.
    isFailed, isCompleted, sikerült-e a küldetés vagy nem.
    interacted, megtörtént-e az első interakció.
    parenten keresztül érjük el a szülő objektumát az NPC-nek, ez más classok eléréséért szükséges
    playerspeed játékos gyorsaságát tárolja.*/
    public List<GameObject> rewardList;
    public string monologe;
    public Quest quest;
    private DialogHandler dialogHandler;
    public Transform dialogBubble;
    public bool isAccepted = false;
    public bool isRewarded = false;
    public bool isCompleted = false;
    public bool isFailed = false;
    public bool interacted = false;
    public Transform parent;
    float playerSpeed; 

    //Megjelenéskor beállitjuk a dialoghandlert, a monologot, és a questet.
    private void Awake() {
        dialogHandler = (DialogHandler)dialogBubble.GetComponent(typeof(DialogHandler));
        monologe = Game.Instance.mainList.getPhrase(Random.Range(0,10));
        quest = Quests.GetRandomQuest();
    }

    //Az updateben vizsgáljuk a játékossal történő interkciókat.
    void Update()
    {
        if (Player.Instance.gameObject != null) 
        {
            Vector2 tp = Player.Instance.gameObject.transform.position;
            Vector2 p = transform.position;                                                         
            float dist = Vector2.Distance(tp, p);
            if (dist < 6)
            {
                if (interacted == false)
                {
                    dialogBubble.gameObject.SetActive(true);
                    dialogHandler.Setup("Press E to interact");
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                    interacted = true;
                }
            }
            else
            {
                dialogBubble.gameObject.SetActive(false);
            }
        }
    }

    /*Maga az interkció menete, a fenti bool értékek alapján.
    Ha a játkos teljesítette a küldetést, a rewardListből kap egy random itemet.
    Megállítjuk a játékost, hogy ne tudjon elmenni amig az NPC beszél.*/
    void Interact()
    {
        if (!isAccepted && !isCompleted && !isFailed)
        {
            playerSpeed = Player.Instance.moveSpeed;
            Player.Instance.moveSpeed = 0;
            dialogHandler.Setup(monologe);
            dialogBubble.gameObject.SetActive(true);
            Invoke("AfterMonologe",5f);
            Invoke("SetPlayerSpeed",5f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                isAccepted = true;
                if (isAccepted && quest.GetQuestName() == "Moving The Chest")
                {
                    parent = transform.parent;
                    GameObject room = parent.Find("ajtok").gameObject;
                    Room roomScript =  (Room) room.GetComponent((typeof(Room)));
                    roomScript.SpawnBoxDestination();
                }
            }
        }
        else if(isAccepted && !isCompleted && !isFailed)
        {
            dialogHandler.Setup("Work on it");
            dialogBubble.gameObject.SetActive(true);
        }
        else if(isCompleted && !isRewarded)
        {
            dialogHandler.Setup("Here is Your Reward");
            dialogBubble.gameObject.SetActive(true);
            isRewarded = true;
            Instantiate(rewardList[Random.Range(0,rewardList.Count)], this.transform.position + this.transform.up, Quaternion.identity);

        }
        else if(isCompleted && isRewarded)
        {
            dialogHandler.Setup("Thank you");
            dialogBubble.gameObject.SetActive(true);
        }
        else if(isFailed)
        {
            dialogHandler.Setup("You failed me");
            dialogBubble.gameObject.SetActive(true);
        }
    }

    //Visszaállítjuk a sebességet ha végig ért a beszéd
    private void SetPlayerSpeed()
    {
        Player.Instance.moveSpeed = playerSpeed;
    }

    //
    private void AfterMonologe()
    {
        dialogHandler.Setup(quest.GetQuestDescription());
        dialogBubble.gameObject.SetActive(true);
    }    
}
