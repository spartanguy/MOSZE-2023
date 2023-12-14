using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*A character class felel a jatékos, és az ellenfelek tevékenységéért, ebből vannak származtatva.*/
public class Character : MonoBehaviour
{
    /*firepoint, a karakterre rögzített fegyver pizícióját tárolja.
    gunEnd  a karakterre rögzített fegyver lövési pontjának pizícióját tárolja.
    moveSpeed, mozgási sebesség.
    rb a karakter szimulált "teste".
    bulletprefab, egy objektumot kell belehelyezni, ez fog lovéskor lövedékként szolgálni.
    gun, a gun class egyik eleme, a játékos fegyvere.
    readyToFire, vizsgálja hogy a játékos készen áll-e a lövésre.
    pickup, vizsgálja hogy a játékos készen áll-e uj tárgy felvételére.
    health, karakter élete.
    speedBuff, attackSpeedBuff, shield a játékban megszerzett erősítések száma.*/
    public Transform firepoint;
    public Transform gunEnd;
    public float moveSpeed = 5;
    public Rigidbody2D rb;    
    public GameObject bulletPrefab;
    public Gun gun;
    public bool readyToFire = true;
    public bool pickup = true;
    [SerializeField]
    public int health = 5;
    public int speedBuff, attackSpeedBuff, shield;

    //rb-t egyenlővé tesszük a karakter objektumon elhelyezett RigiBodyval.
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //JKarakter mozgatásáért szolgál az rb-n keresztül.
    public void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    /*Karakter lövéséért felel, megvizsgáljuk van-e fegyver és tudunk-e lőni.
    Ha ezekre a válasz igen, megnézzük az aktuális fegyver hány golyót lő egyszerre, majd ennyi a buletPrefabban elhelyezett objektumot jelenítünk meg.
    Beállítjuk a megfelelő layerre, majd megadjuk a méretét.
    Ha mindez megtörtént, lendületet adunk neki a megfelelő irányba, majd szünetetltetjük a lövése lehetőséget.*/
    public void Shoot()
    {
        if (gun == null) return;
        if (!readyToFire) return;
        readyToFire = false;
        for (int i = 0; i < gun.GetBullets(); i++) {
            GameObject b = Instantiate(bulletPrefab, gunEnd.position, firepoint.transform.rotation);
            if (gameObject.layer == LayerMask.NameToLayer("Player")) 
            {
                b.layer = LayerMask.NameToLayer("PlayerBullet");
            }
            else b.layer = LayerMask.NameToLayer("EnemyBullet");
            Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
            
            ((Bullet) b.GetComponent(typeof(Bullet))).SetDamage(gun.GetDamage());

            float s = gun.GetDamage() / 2;
            if (s > 0.5f) s = 0.5f;
            b.transform.localScale *= 1 + s;

            Vector2 dir = firepoint.transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gun.GetSpread(), gun.GetSpread());
            brb.velocity = (dir + pdir) * gun.GetSpeed();
        }

        float f = gun.GetFireRate();
        Invoke(nameof(FireCooldown), gun.GetFireRate()*getAttackSpeedBuff());
    }

    //Vizsgálja az egér koordinátáját és az alapjány forgatja és tükrözi a karaktert a megfelelő irányba.
    public void Aim(Vector3 target) 
    {
        Vector3 dir = (firepoint.transform.position - target).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firepoint.transform.eulerAngles = new Vector3(0,0,angle + 180 );
        if (angle >= -90 && angle <= 90)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            firepoint.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            firepoint.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    //Tüzelés ujra engedélyezése.
    public void FireCooldown() 
    {
        readyToFire = true;
    }

    //Tárgyfelvétel ujra engedéyezése.
    public void PickupCooldown() 
    {
        pickup = true;
    }

    //AttackSpeedBuff getter
    public float getAttackSpeedBuff()
    {
        return (float)(1 - (attackSpeedBuff * 0.05));
    }
    //Sebesség beállítása a speedBuff alapján.
    public void SetSpeed()
    {
        moveSpeed += (float)(speedBuff*0.20);
    }

    //Karakter halála, virtuális class, midnen ebből származtatott classban felül van írva.
    public virtual void killCharacter(GameObject chara)
    {
        Destroy(chara);
    }

    /*Karakter sebződése. Ha van páncél, először az sérül.
    Amennyiben az élet 0-ra esik, meghíjuk a killCharactert.*/
    public void Damage(int damage, GameObject go) {
        if (shield > 0)
        {
            if (damage > shield)
            {
                int carryOn = 0;
                carryOn = damage - shield;
                shield = 0;
                health -= carryOn;
            }
            else {shield -= damage;}
        }
        else {health -= damage;}
        if (health <= 0)
        {
            killCharacter(go);
        }
        BuffManager.Instance.setBuffs();
    }

    //Ui
    public int getSpeedBuffUI(){
        return speedBuff;
    }

    //UI
    public int getAttackSpeedBuffUI(){
        return attackSpeedBuff;
    }
    
    //Ui      
    public int getShieldUI(){
        return shield;
    }
}