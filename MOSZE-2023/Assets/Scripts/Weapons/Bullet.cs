using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A lövedékek viszelkedéséért felelős class.
public class Bullet : MonoBehaviour
{
    //damage a fegyver alapsebzése.
    private int damage = 1;

    //layerezés megvizsgálása után, a lövedék megsebzi az eltalált karaktert, majd elpusztul.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){return;}
        if ((gameObject.layer == LayerMask.NameToLayer("PlayerBullet") && other.tag == "Player") || (gameObject.layer == LayerMask.NameToLayer("EnemyBullet") && other.tag == "Enemy") || (other.gameObject.layer == LayerMask.NameToLayer("pickup")) || (other.gameObject.layer == LayerMask.NameToLayer("SzobaCollider"))) return;
        else if (other.tag == "Player" || other.tag == "Enemy")
        {
            Character a = (Character)other.gameObject.GetComponent(typeof(Character));
            GameObject b = other.gameObject;
            a.Damage(damage,b);
            DestroyBullet();
            
        }
        else DestroyBullet();
    }

    //lövedék elpusztulása.
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //lövedék sebzésének beállitása gun class alapján.
    public void SetDamage(int d) {
        damage = d;
    }
}
