using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;
    public Sprite plushHeart;

    public int maxHp = 5;
    public int health = 5;
    public int plusHP = 0;
    public int maxPlusHP = 5;

    void FixedUpdate() {
        maxHp = Player.Instance.getMaxHp();
        health = Player.Instance.getHealth();
        plusHP = maxHp-5;

        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].sprite = emptyheart;
        }

        for (int i = 0; i < health; i++){
            if (i < 5)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = plushHeart;
            }
        }
        
        for (int j = 0; j < maxPlusHP; j++) {
            if(j<plusHP) {
                hearts[j+5].gameObject.SetActive(true);
            } else {
                hearts[j+5].gameObject.SetActive(false);
            }
        }
    }
}
