using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Transform firepoint;
    public float moveSpeed = 5;
    public Rigidbody2D rb;    
    public GameObject bulletPrefab;
    public Gun gun = Guns.pistol;
    public bool readyToFire = true;
    public bool pickup = true;
    [SerializeField]
    public int health = 5;
    public int damageBuff, speedBuff, attackspeedBuff;

    public void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        if (gun == null) return;
        if (!readyToFire) return;
        readyToFire = false;
        for (int i = 0; i < gun.GetBullets(); i++) {
            GameObject b = Instantiate(bulletPrefab, firepoint.position + firepoint.up, transform.rotation);
            Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
            
            ((Bullet) b.GetComponent(typeof(Bullet))).SetDamage(gun.GetDamage());

            float s = gun.GetDamage() / 2;
            if (s > 0.5f) s = 0.5f;
            b.transform.localScale *= 1 + s;

            Vector2 dir = transform.rotation * Vector2.up;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gun.GetSpread(), gun.GetSpread());
            brb.velocity = (dir + pdir) * gun.GetSpeed();
        }

        float f = gun.GetFireRate();
        Invoke(nameof(FireCooldown), gun.GetFireRate()*getAttackSpeedBuff());
    }

    public void Aim(Vector3 target) 
    {
        Vector2 dir = transform.position - target;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0, angle + 90));
    }

    public void FireCooldown() 
    {
        readyToFire = true;
    }
    public void PickupCooldown() 
    {
        pickup = true;
    }

    public float getAttackSpeedBuff()
    {
        return (float)(1 - (attackspeedBuff * 0.05));
    }

    public void Damage(int damage, GameObject go) {
        health -= damage;
        if (health <= 0)
        {
            Destroy(go);
        }
    }
}