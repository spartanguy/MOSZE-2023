using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Weapon a játékban megtalálhato fegyver objektum amivel a játékos interakcióba léphet.
public class Weapon : MonoBehaviour {
    /*gun, a weaponhoz tartozó fegyver.
    gunNr, a weaponhoz tartozó fegyver száma a Guns class listájában.*/
    private Gun gun;
    public int gunNr;

    //Instance error elkerülése végett.
    private void Start() {
        Invoke("asd",0.1f);
    }

    //Visszadaja a weapon fegyver értékét.
    public Gun GetGun() {
        return gun;
    }

    //Megkapja a hpzzá tartozó gun értréket a Guns classból gunNr alapján.
    public void asd()
    {
        gun = Guns.GetGun(gunNr);
    }
}
