using UnityEngine;

//Gun class konstruktorja alapján tudunk fegyvereket hozzáadni a játékhoz.
public class Gun {

    /*fireRate, fegyver lovései sebesség.
    bullets, fegyver hány lövedéket lő.
    spread, mekkora a fegyver "hordása".
    speed, a fegyver lövedékének sebessége.
    damage, fegyver sebzése.
    description, fegyver neve.
    */
    private float fireRate;
    private float bullets;
    private float spread;
    private float speed;
    private int damage;
    private string description;
    
    //konstruktor.
    public Gun(float fireRate, float bullets, float spread, float speed, int damage, string desc) {
        this.fireRate = fireRate;
        this.bullets = bullets;
        this.spread = spread;
        this.speed = speed;
        this.description = desc;
        this.damage = damage;
    }

    //getterek.
    public float GetFireRate() {
        return fireRate;
    }

    public float GetBullets() {
        return bullets;
    }

    public float GetSpread() {
        return spread;
    }

    public float GetSpeed() {
        return speed;
    }

    public string GetDescription() {
        return description;
    }

    public int GetDamage() {
        return damage;
    }

}
