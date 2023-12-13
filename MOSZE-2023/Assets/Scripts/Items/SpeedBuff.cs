using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ettől a fejlesztéstől a karakter gyorsasága növekszik.
public class SpeedBuff : Item
{
    public override void Upgrade()
    {
        //Möveljuk a karakter speedBuff értékét majdmeghívjuk a SetSpeed funkcióját és elpusztítjuk a tárgyat.
        Player.Instance.speedBuff++;
        Player.Instance.SetSpeed();
        Destroy(gameObject);
    }
}
