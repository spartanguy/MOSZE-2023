using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ezen fejlesztéssel való interakciókor a játékos páncélt szerez.
public class Shield : Item
{
    public override void Upgrade()
    {
        //Möveljuk a karakter shield értékét majd és elpusztítjuk a tárgyat.
        Player.Instance.shield += 8;
        Destroy(gameObject);

    }
}
