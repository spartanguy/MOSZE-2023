using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Box egy objektum a játékban amik általában küldetésekhez köthetőek, de fedezékként is használhatóak.
public class Box : MonoBehaviour
{
    //keyQuestThing, megkülönbözető jelzés a tárgyra hogy az egyik quest ne keverje össze a msikkal.
    public Transform parent;
    int health = 5;
    public bool keyQuestThing = false;

    //2 esetet vizsgálunk, az egyikben ha elpustul a tárgy elvesztjünk a küldetést,
    //A másikban pedig az a cél hogy elpusztítsuk őket.
    public void Damage(int damage, GameObject go) {
        
        health -= damage;
        if (health <= 0)
        { 
            if (keyQuestThing == true)
            {
                parent = transform.parent;
                GameObject npc = parent.GetChild(parent.childCount-3).gameObject;
                Npc npcScript =  (Npc) npc.GetComponent((typeof(Npc)));
                if(npcScript.isCompleted != true)
                {
                    npcScript.isFailed = true;
                }
            }
            Destroy(go);
        }
    }
}
