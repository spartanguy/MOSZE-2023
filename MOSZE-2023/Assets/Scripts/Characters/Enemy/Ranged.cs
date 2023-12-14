using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Charater classból származtatott, a távolharci ellenfelek irányításáért felelős class.
public class Ranged : Character
{
    /*weaponList, fegyverek listája amíből egyet majd megkap.
    palyer, játékos pozíciója.
    moveAwayDistance, az az érték aminél távolabb szeretne állni a játékostól.
    desiredDist az az érték aminél közelebb szeretne állni a játékostól.
    drop, objektum amit halálkor el tud dobni.
    firepointSprite a karakterre rögzített fegyver spriteja.*/
    public List<GameObject> weaponList;
    protected Transform player;
    public float desiredDist;
    public float moveAwayDist;
    public GameObject drop;
    public GameObject weapon;
    protected SpriteRenderer firepointSprite;

    /*Létrejövetelkor meagdjuk a sebességet, beállítjuk a "kergetni" kívánt játékost.
    Ezek után kap egy random fegyvert a weaponListből, majd a fegyver kinézetét, és a karakter fegyver értékét beállítjuk ezére.
    beálltjuk a desiredDistet és a moveAwayDistet, majd az életet és az erősítéseket.*/
    private void Awake() {
        moveSpeed = 3;
        player = Player.Instance.transform;
        weapon = weaponList[Random.Range(0,weaponList.Count)];
        firepointSprite = firepoint.GetChild(0).GetComponent<SpriteRenderer>();
        Weapon w = (Weapon)weapon.GetComponent(typeof(Weapon));
        w.asd();
        gun = w.GetGun();
        firepointSprite.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        desiredDist = GetFireRange(gun);
        moveAwayDist = desiredDist/2;
        health = health + Game.Instance.GetEnemyHealth();
        attackSpeedBuff = Game.Instance.GetFirerateMultiplier();
        speedBuff = Game.Instance.GetSpeedMultiplier();
        SetSpeed();
        
    }

    //Frissül a játékos pozíciója, majd ez alapján mozog a karakter, ha elég közel ér lő.  
    void FixedUpdate()
    {
        if (player == null) return;
        Vector2 tp = player.position;
        Vector2 p = transform.position;
        float dist = Vector2.Distance(tp, p);
        Vector2 dir = (player.position - transform.position).normalized;
        
        if (dist > desiredDist)
        {
            MoveCharacter(dir);
        }    
        else if (dist < moveAwayDist) 
        {
            MoveCharacter(dir*(-1));
        }   
        else 
        {
            rb.velocity = Vector2.zero;
        }
        Aim(player.position);

        if (dist <= desiredDist)
        {
            Shoot();
        }
    }

    //Felülírja a karakter killCharacter funkcíóját. Ha meghal egy ellenfél a játékos pontot kap érte, és van esély arra hogy halálakor eldob egy droppot.
    public override void killCharacter(GameObject chara)
    {
        int random = Random.Range(0,2);
        Game.Instance.score += 25;
        if (random == 1)
        {
            Instantiate(drop,this.transform.position,Quaternion.identity);
        }
        Destroy(chara);
    }

    //DesiredDist beállytására szolgáló értékek.
    protected float GetFireRange(Gun gun)
    {
        if (gun.GetDescription() == "Pistol"){return 9;}
        if (gun.GetDescription() == "Shotgun"){return 2;}
        if (gun.GetDescription() == "Rifle"){return 6;}
        else {return 0;}
    }
}
