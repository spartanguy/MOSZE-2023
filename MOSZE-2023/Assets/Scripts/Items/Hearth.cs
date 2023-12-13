using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ezen fejlesztéssel való interakciókor a játékos gyógyul.
public class Hearth : Item
{
    //Meghívjuk a játékos heal funkcióját, majd elpusztítjuk a tárgyat.
    public override void Upgrade() {
        Player.Instance.Heal();
        Destroy(gameObject);
    }
}
