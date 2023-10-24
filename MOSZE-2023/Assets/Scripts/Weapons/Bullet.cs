using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Character a = (Character)other.gameObject.GetComponent(typeof(Character));
            GameObject b = other.gameObject;
            a.Damage(damage,b);
            Destroy(gameObject);
        }
        else{Destroy(gameObject);}
    }
    public void SetDamage(int d) {
        damage = d;
    }
}
