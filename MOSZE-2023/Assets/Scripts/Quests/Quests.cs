using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quests Class tartalmazza az elérhető küldetések listáját.
public class Quests : MonoBehaviour
{
    //Questek létrehozása.
    public static Quest quest1;
    public static Quest quest2;

    //Questek listája
    public static List<Quest> quests;

    //Meghíjuk a konstruktorokat, majd a listához adjuk ezeket.
    private void Start() {
        quest1 = new Quest("Alfa", "Press Button X near me!");
        quest2 = new Quest("Alfa", "Press Button X near me!");
        quests = new List<Quest>();
        quests.Add(quest1);
        quests.Add(quest2);
    }

    //Visszaadja a lista n-edik küldetését.
    public static Quest GetQuest(int n) {
        return quests[n];
    }

    //Visszad egy random küldetést.
    public static Quest GetRandomQuest() {
        return quests[Random.Range(0, quests.Count)];       
    }
}
