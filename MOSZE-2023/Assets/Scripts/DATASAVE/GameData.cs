using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [System.Serializable]
    public struct RoomData {
        public string prefabName;
        public float xCoord;
        public float yCoord;
        public string roomType;
    }
    public int scoreBoardData;
    public RoomData asd;
    public List<RoomData> RoomDataList;

    public void ListDeclaration()
    {
        RoomDataList = new List<RoomData>();
    }
}



