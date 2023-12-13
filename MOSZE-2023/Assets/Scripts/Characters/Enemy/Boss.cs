using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Charater classból származtatott, fő ellenfél irányításáért felelős class.
public class Boss : Character
{
    /*palyer, játékos pozíciója.
    moveAwayDistance, az az érték aminél távolabb szeretne állni a játékostól.
    desiredDist az az érték aminél közelebb szeretne állni a játékostól.
    maxHp maximum élet.
    changed, átváltott-e már a második fázisba.
    marker, jelölő objektum.
    burn, égetés játlobjektum.
    putBurn lerakhat-e másik burn objektumot.
    markPos marker pozíciója.
    weapon, fegyver objektum.
    changeWeapon, masodik fázisban lévő fegyver.
    firepointSprite a karakterre rögzített fegyver spriteja.*/
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

    /*Létrejövetelkor megkap egy élet értéket, beállítódik a "kergetni" kívánt játékos.
    Ezek után kap egy random fegyvert a weaponListből, majd a fegyver kinézetét, és a karakter fegyver értékét beállítjuk ezére.
    Beállítódik a desiredDiste és a moveAwayDistet majd az erősítéseket.*/
    private void Awake() {
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
    /*Frissül a játékos pozíciója, majd ez alapján mozog a karakter, ha elég közel ér lő. 
    Ezen kívül megadaott időközönként markereket rak a pályára a játékos pozíciójára, ami megadott időn belül burnné változik, ami sebzi a játékost ha benne áll.
    Ha egy megadott élet alá esik átvált a következő stádiumba.*/
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

    //Másik stádiumba változáskor uj fegyvert kap, és átállítódik a desiredDiste és a moveAwayDist.
    public void PhaseTwo()
    {
        gun = Guns.GetGun(1);
        desiredDist = 3;
        moveAwayDist = desiredDist/2;
        gun = Guns.GetGun(2);
        firepointSprite.sprite = changeWeapon.GetComponent<SpriteRenderer>().sprite;
    }

    //Felülírja a karakter killCharacter funkcíóját. Ha meghal a játékos pontot kap érte, és a játék véget ér a játékos győzelmével.
    public override void killCharacter(GameObject chara)
    {
        Game.Instance.score += 1000;
        Destroy(chara);
        Time.timeScale = 0f;
        Game.Instance.win.SetActive(true);
    }

    //A boss által lerakott burn objektum viselkedése, a markert változtatja burnné.
    public void Burn()
    {
        markPos = player.position;
        GameObject mark = Instantiate(marker,markPos,Quaternion.identity);
        Invoke("SetBye",2f);
        Destroy(mark,2f);
    }

    //A boss által lerakott burn objektum viselkedése, a markert változtatja burnné.
    public void SetBye()
    {
        GameObject burns = Instantiate(burn,markPos,Quaternion.identity);
        Destroy(burns,10f);
    }

    //Vizsgálja mikor rakhat a boss le uj burnt.
    public void SetPutBurn()
    {
        putBurn = true;
    }
}
