using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int scoreBoardData;
    public List<Szobak> szobakData = new List<Szobak>();

    public struct Szobak {
        string prefabName;
        float xCoord;
        float yCoord;
        string RoomType;
    }


    public GameData() {
        this.scoreBoardData = 0;
    }
}



