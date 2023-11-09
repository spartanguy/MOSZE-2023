using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Character
{
    public static Ranged Instance { get; set; }
    protected Transform player;
    public float desiredDist;
    public float moveAwayDist;

    private void Awake() {
        Instance = this;
        player = Player.Instance.transform;
        gun = Guns.GetRandomGun();
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

    protected float GetFireRange(Gun gun)
    {
        if (gun.GetDescription() == "Pistol"){return 5;}
        if (gun.GetDescription() == "Shotgun"){return 3;}
        if (gun.GetDescription() == "Rifle"){return 3;}
        if (gun.GetDescription() == "Sniper"){return 8;}
        else {return 0;}
    }
}
