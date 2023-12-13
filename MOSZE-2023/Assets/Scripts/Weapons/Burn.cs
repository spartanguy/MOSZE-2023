using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Burn a fő ellenfél által lerakott "csapda".
public class Burn : MonoBehaviour
{
    //Ez vizsgálja mennyi időnként sebez a burn.
    public bool tick = false;

    //Ha a benne álló karakter játékos és tick igaz akkor a játékos megsebzódik.
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

    //tick visszaállítása.
    public void SetTick()
    {
        tick = false;
    }
}
