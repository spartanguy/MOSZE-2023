using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Character
{
    public static Ranged Instance { get; set; }
    public List<GameObject> weaponList;
    protected Transform player;
    public float desiredDist;
    public float moveAwayDist;
    public GameObject drop;
    public GameObject weapon;
    protected SpriteRenderer firepointSprite;

    private void Awake() {
        Instance = this;
        //Játékos becélzása
        player = Player.Instance.transform;
        //Random fegyver választás
        weapon = weaponList[Random.Range(0,weaponList.Count)];
        //Ellenfél fegyverRenderer megkeresése
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
    protected float GetFireRange(Gun gun)
    {
        if (gun.GetDescription() == "Pistol"){return 5;}
        if (gun.GetDescription() == "Shotgun"){return 3;}
        if (gun.GetDescription() == "Rifle"){return 3;}
        if (gun.GetDescription() == "Sniper"){return 8;}
        else {return 0;}
    }
}
