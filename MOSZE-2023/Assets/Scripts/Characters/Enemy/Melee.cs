using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Charater classból származtatott, a közelharci ellenfelek irányításáért felelős class.
public class Melee : Character
{
    /*palyer, játékos pozíciója.
    drop, objektum amit halálkor el tud dobni.
    damage, az érték amennyivel megsebzi a játékost
    readyToHit vizsgálja tud e ütni a karakter.*/
    protected Transform player;
    protected int damage;
    protected bool readyToHit;
    public GameObject drop;

    private void Awake() {
        moveSpeed = 5f;
        player = Player.Instance.transform;
        damage = 1;
        readyToHit = true;
        health = health + Game.Instance.GetEnemyHealth();
        speedBuff = Game.Instance.GetSpeedMultiplier();
        SetSpeed();
    }

    //Frissül a játékos pozíciója, majd ez alapján mozog a karakter, ha elég közel ér megsebzi, majd deaktiválja az ütést.. 
    void FixedUpdate()
    {
        if (player == null) return;
        Vector2 tp = player.position;
        Vector2 p = transform.position;                                                         
        float dist = Vector2.Distance(tp, p);
        Vector2 dir = (tp - p).normalized;
        if (dist < 1.1)
        {
            if(readyToHit == false){return;}
            Character a = (Character)Player.Instance.gameObject.GetComponent(typeof(Character));
            GameObject b = Player.Instance.gameObject;
            a.Damage(damage,b);
            readyToHit = false;
            Invoke(nameof(getHitReady), 1f);
        }
        MoveCharacter(dir);
    }
    //Felülírja a karakter killCharacter funkcíóját. Ha meghal egy ellenfél a játékos pontot kap érte, és van esély arra hogy halálakor eldob egy droppot.
    public override void killCharacter(GameObject chara)
    {
        int random = Random.Range(0,4);
        Game.Instance.score += 25;
        if (random == 2)
        {
            Instantiate(drop,this.transform.position,Quaternion.identity);
        }
        Destroy(chara);
    }

    //Ütés ujraaktiválása
    protected void getHitReady()
    {
        readyToHit = true;
    }
}
