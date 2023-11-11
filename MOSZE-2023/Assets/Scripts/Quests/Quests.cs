using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public static Quest quest1;
    public static Quest quest2;

    public static List<Quest> quests;

    private void Start() {
        quest1 = new Quest("Duck Logic", "Gondolkodj gyerek");
        quest2 = new Quest("Kick Ass", "Csapkodj");
        quests = new List<Quest>();
        quests.Add(quest1);
        quests.Add(quest2);
    }

    public static Quest GetQuest(int n) {
        return quests[n];
    }

    public static Quest GetRandomQuest() {
        return quests[Random.Range(0, quests.Count)];       
    }
}
