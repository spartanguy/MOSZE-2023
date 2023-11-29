using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseList
{
    private List<string> phrases = new List<string>
    {
        "Hello traveller! Can You Do Something For me?",
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


    private void Awake() {
        dialogHandler = (DialogHandler)dialogBubble.GetComponent(typeof(DialogHandler));
        monologe = Game.Instance.mainList.getPhrase();
        quest = Quests.GetRandomQuest();
    }

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
                if (Input.GetKeyDown(KeyCode.X))
                {
                    isCompleted = true;
                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    isFailed = true;
                }
            }
            else
            {
            dialogBubble.gameObject.SetActive(false);
            }
        }
    }

    void Interact()
    {
        if (!isAccepted && !isCompleted && !isFailed)
        {
            dialogHandler.Setup(monologe);
            dialogBubble.gameObject.SetActive(true);
            Invoke("AfterMonologe",5f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                isAccepted = true;
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
    private void AfterMonologe()
    {
        dialogHandler.Setup(quest.GetQuestDescription());
        dialogBubble.gameObject.SetActive(true);
    }    
}
