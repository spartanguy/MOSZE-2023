using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Guns Class tartalmazza az elérhető fegyverek listáját.
public class Guns : MonoBehaviour{   

    // hozzáadott fegyverek.
    public static readonly Gun pistol = new Gun(0.3f, 1, 0.08f, 12f, 1, "Pistol");
    public static readonly Gun shotgun = new Gun(0.7f, 4, 0.5f, 14f, 1, "Shotgun");
    public static readonly Gun rifle = new Gun(0.2f, 1, 0.3f, 15f, 1, "Rifle");
    public static readonly Gun sniper = new Gun(1f, 1, 0.0f, 25f, 10, "Sniper");

    //fegyverek listája.
    public static List<Gun> guns;

    //kezdéskor a fenti fegyvereket belerakjuk a listába.
    private void Start() {
        guns = new List<Gun>();
        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(rifle);
        guns.Add(sniper);
    }

    //Visszaadja a lista n-edik fegyverét.
    public static Gun GetGun(int n) {
        return guns[n];
    }

    //Egy random fegyvert ad vissza.
    public static Gun GetRandomGun() {
        return guns[Random.Range(0, guns.Count)];
    }
}