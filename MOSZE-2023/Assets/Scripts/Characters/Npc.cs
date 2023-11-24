using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseList
{
    private List<string> phrases = new List<string>
    {
        "Carpe diem.",
    };
    private int len;

    public string getPhrase()
    {
        string phrase = phrases[len];
        return phrase;
    }

}

public class Npc : MonoBehaviour
{
    public static Npc Instance { get; set; }
    public List<GameObject> rewardList;
    public string monologe;
    public Quest quest;
    public bool isAccepted = false;
    public bool isRewarded = false;
    public bool isCompleted = false;
    public bool isFailed = false;

    private void Awake() {
        Instance = this;
        monologe = Game.Instance.mainList.getPhrase();
        quest = Quests.GetRandomQuest();
    }

    void Update()
    {
        Vector2 tp = Player.Instance.gameObject.transform.position;
        Vector2 p = transform.position;                                                         
        float dist = Vector2.Distance(tp, p);
        if (dist < 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                isCompleted = true;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                isFailed = true;
            }
        }
    }

    void Interact()
    {
        if (!isAccepted && !isCompleted && !isFailed)
        {
            Debug.Log(monologe);
            Debug.Log(quest.GetQuestName());
            Debug.Log(quest.GetQuestDescription());
            Debug.Log("Will u accept? (press e)");
            if (Input.GetKeyDown(KeyCode.E))
            {
                isAccepted = true;
            }
        }
        else if(isAccepted && !isCompleted && !isFailed)
        {
            Debug.Log("Work on it");
        }
        else if(isCompleted && !isRewarded)
        {
            Debug.Log("Here is your reward!");
            isRewarded = true;
            Instantiate(rewardList[Random.Range(0,rewardList.Count)], this.transform.position + this.transform.up, new Quaternion(0,0,0,0));

        }
        else if(isCompleted && isRewarded)
        {
            Debug.Log("Nice One!");
        }
        else if(isFailed)
        {
            Debug.Log("You fucked up");
        }
    }    
}
