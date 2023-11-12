using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseList
{
    private List<string> phrases = new List<string>
    {
        "Carpe diem.",
        "Actions speak louder than words.",
        "Where there's a will, there's a way.",
        "The early bird catches the worm.",
        "All that glitters is not gold.",
        "A penny for your thoughts.",
        "Don't count your chickens before they hatch.",
        "Every cloud has a silver lining.",
        "When in Rome, do as the Romans do.",
        "Beauty is in the eye of the beholder.",
        "Rome wasn't built in a day.",
        "A picture is worth a thousand words.",
        "Don't put all your eggs in one basket.",
        "Honesty is the best policy.",
        "You can't have your cake and eat it too.",
        "The grass is always greener on the other side.",
        "Don't cry over spilled milk.",
        "Actions speak louder than words.",
        "A stitch in time saves nine.",
        "The proof of the pudding is in the eating."
    };

    public string getRandomPhrase()
    {
        string phrase = phrases[Random.Range(0,phrases.Count)];
        phrases.Remove(phrase);
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
        monologe = Game.Instance.mainList.getRandomPhrase();
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
