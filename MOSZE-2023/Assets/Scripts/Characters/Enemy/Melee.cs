using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Character
{
    public static Melee Instance { get; set; }
    protected Transform player;
    protected int damage;
    protected bool readyToHit;

    private void Awake() {
        Instance = this;
        player = Player.Instance.transform;
        damage = 1;
        readyToHit = true;
        health = health + Game.Instance.GetEnemyHealth();
        speedBuff = Game.Instance.GetSpeedMultiplier();
        SetSpeed();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Vector2 tp = player.position;
        Vector2 p = transform.position;                                                         
        float dist = Vector2.Distance(tp, p);
        Vector2 dir = (tp - p).normalized;
        Debug.Log(dist);
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

    /*void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player")
        {   
            if(readyToHit == false){return;}
            Character a = (Character)other.gameObject.GetComponent(typeof(Character));
            GameObject b = other.gameObject;
            a.Damage(damage,b);
            readyToHit = false;
            Invoke(nameof(getHitReady), 1f);
        }    
    }*/

    protected void getHitReady()
    {
        readyToHit = true;
    }
}
