using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Guns : MonoBehaviour{   
    public static readonly Gun pistol = new Gun(0.3f, 1, 0.08f, 12f, 1, "Pistol");
    public static readonly Gun shotgun = new Gun(0.45f, 6, 0.25f, 14f, 1, "Shotgun");
    public static readonly Gun rifle = new Gun(0.1f, 1, 0.15f, 15f, 1, "Rifle");
    public static readonly Gun sniper = new Gun(0.8f, 1, 0.0f, 25f, 10, "Sniper");

    public static List<Gun> guns;

    private void Start() {
        guns = new List<Gun>();
        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(rifle);
        guns.Add(sniper);
    }

    public static Gun GetGun(int n) {
        return guns[n];
    }

    public static Gun GetRandomGun() {
        return guns[Random.Range(0, guns.Count)];
    }
}