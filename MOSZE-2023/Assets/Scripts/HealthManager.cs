using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Image[] plusHearts;
    public Sprite fullheart;
    public Sprite emptyheart;

    public int maxHp = 5;
    public int health = 5;
    public int plusHP = 2;
    public int maxPlusHP = 5;

    void FixedUpdate() {
        maxHp = Player.Instance.getMaxHp();
        health = Player.Instance.getHealth();

        foreach (Image kep in hearts) {
            kep.sprite = emptyheart;
        }

        for (int i = 0; i < health; i++){
            hearts[i].sprite = fullheart;
        }
        
        for (int j = 0; j < maxPlusHP; j++) {
            if(j<plusHP) {
                plusHearts[j].gameObject.SetActive(true);
            } else {
                plusHearts[j].gameObject.SetActive(false);
            }
        }
    }
}
