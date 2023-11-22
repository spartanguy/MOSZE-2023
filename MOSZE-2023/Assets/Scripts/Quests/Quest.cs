using UnityEngine;

public class Quest
{
    private string questName;
    private string questDescription;

    public Quest(string name, string description)
    {
        questName = name;
        questDescription = description;
    }

    public string GetQuestName()
    {
        return questName;
    }
    public string GetQuestDescription()
    {
        return questDescription;
    }
}

