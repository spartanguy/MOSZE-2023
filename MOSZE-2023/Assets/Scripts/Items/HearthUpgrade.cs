using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ezen fejlesztéssel való interakciókor a játékos maximális életereje növekszik.
public class Hearth_upgrade : Item
{
    //Meghívjuk a játékos setHP funkcióját, majd elpusztítjuk a tárgyat.
    public override void Upgrade() {
        Player.Instance.SetHp(1);
        Destroy(gameObject);
    }
}
