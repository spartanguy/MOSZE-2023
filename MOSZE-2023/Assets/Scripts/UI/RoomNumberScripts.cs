using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomNumberScripts : MonoBehaviour
{
    int roomNumber = 0;
    public TMP_Text roomNumberText;

    void Update(){
        roomNumber = szobaTemplates.Instance.GetRoomNumber();
        DisplayRoomNumber();
    }

    void DisplayRoomNumber() {
        roomNumberText.text = roomNumber.ToString();
    }
}
