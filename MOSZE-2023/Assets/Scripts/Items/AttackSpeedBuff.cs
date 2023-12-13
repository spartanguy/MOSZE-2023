using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ettől a fejlesztéstől a karakter támadási sebessége növekszik.
public class AttackSpeedBuff : Item
{
    public override void Upgrade()
    //Möveljuk a karakter attackSpeedBuff értékét elpusztítjuk a tárgyat.
    {
        Player.Instance.attackSpeedBuff++;
        Destroy(gameObject);
    }
}
