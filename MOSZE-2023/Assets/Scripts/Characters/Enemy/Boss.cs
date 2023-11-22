using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    public static Boss Instance { get; set; }
    protected Transform player;
    public float desiredDist;
    public float moveAwayDist;
    public int maxHp;
    public bool changed = false;
    public GameObject marker;
    public GameObject burn;
    public bool putBurn = true; 
    public Vector2 markPos;
    public GameObject weapon;
    public GameObject changeWeapon;
    protected SpriteRenderer firepointSprite;

    private void Awake() {
        Instance = this;
        health = 200;
        player = Player.Instance.transform;
        gun = Guns.GetGun(3);
        firepointSprite = firepoint.GetChild(0).GetComponent<SpriteRenderer>();
        firepointSprite.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        desiredDist = 200;
        moveAwayDist = 0;
        health = health + Game.Instance.GetEnemyHealth();
        maxHp = health;
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
        if(health <= maxHp/10 && changed == false)
        {
            PhaseTwo();
            changed = true;
        }
        if (putBurn)
        {
            Burn();
            putBurn = false;    
            Invoke("SetPutBurn",5f);
        }
    }
    public void PhaseTwo()
    {
        gun = Guns.GetGun(1);
        desiredDist = 3;
        moveAwayDist = desiredDist/2;
        gun = Guns.GetGun(2);
        firepointSprite.sprite = changeWeapon.GetComponent<SpriteRenderer>().sprite;
    }
    public override void killCharacter(GameObject chara)
    {
        Game.Instance.score += 1000;
        Destroy(chara);
    }
    public void Burn()
    {
        markPos = player.position;
        GameObject mark = Instantiate(marker,markPos,Quaternion.identity);
        Invoke("SetBye",2f);
        Destroy(mark,2f);
    }
    public void SetBye()
    {
        GameObject burns = Instantiate(burn,markPos,Quaternion.identity);
        Destroy(burns,10f);
    }
    public void SetPutBurn()
    {
        putBurn = true;
    }
}
