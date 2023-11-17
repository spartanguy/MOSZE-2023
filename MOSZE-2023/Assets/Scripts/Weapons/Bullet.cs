using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){return;}
        if ((gameObject.layer == LayerMask.NameToLayer("PlayerBullet") && other.tag == "Player") || (gameObject.layer == LayerMask.NameToLayer("EnemyBullet") && other.tag == "Enemy") || (other.gameObject.layer == LayerMask.NameToLayer("pickup"))) return;
        else if (other.tag == "Player" || other.tag == "Enemy")
        {
            Character a = (Character)other.gameObject.GetComponent(typeof(Character));
            GameObject b = other.gameObject;
            a.Damage(damage,b);
            DestroyBullet();
            
        }
        else DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public void SetDamage(int d) {
        damage = d;
    }
}
