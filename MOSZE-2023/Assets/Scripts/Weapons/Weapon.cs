using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Gun gun;
    public int gunNr;

    private void Start() {
        Invoke("asd",0.1f);
    }

    public Gun GetGun() {
        return gun;
    }

    public void asd()
    {
        gun = Guns.GetGun(gunNr);
    }
}
