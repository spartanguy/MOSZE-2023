using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffManager : MonoBehaviour
{

    public static BuffManager Instance { get; set; }

    public int speedBuffNumber = 0;
    public int attackSpeedBuffNumber = 0;
    public int shieldNumber = 0;

    public TMP_Text speedBuffText;
    public TMP_Text attackSpeedText;
    public TMP_Text shieldText;
    
    void Awake(){
        Instance = this;
    }

    public void setBuffs() {
        speedBuffNumber = Player.Instance.getSpeedBuffUI();
        attackSpeedBuffNumber = Player.Instance.getAttackSpeedBuffUI();
        shieldNumber = Player.Instance.getShieldUI();

        speedBuffText.text = string.Format("{0}x", speedBuffNumber);
        attackSpeedText.text = string.Format("{0}x", attackSpeedBuffNumber);
        shieldText.text = string.Format("{0}x", shieldNumber);
    }

    
}
