using UnityEngine;

//Quest class konstruktorja alapján tudunk küldetéseket hozzáadni a játékhoz.
public class Quest
{
    /*questName a küldetés neve.
    questDescription a küldetés leírása.*/
    private string questName;
    private string questDescription;

    //konstruktor.
    public Quest(string name, string description)
    {
        questName = name;
        questDescription = description;
    }

    //Getterek.
    public string GetQuestName()
    {
        return questName;
    }
    public string GetQuestDescription()
    {
        return questDescription;
    }
}

