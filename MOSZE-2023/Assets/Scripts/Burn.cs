using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    public bool tick = false;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player")
        {
            if(tick == false)
            {
                Character a = (Character)Player.Instance.gameObject.GetComponent(typeof(Character));
                GameObject b = Player.Instance.gameObject;
                a.Damage(1,b);
                tick = true;
                Invoke("SetTick",1f);
            }
        }        
    }
    public void SetTick()
    {
        tick = false;
    }
}
