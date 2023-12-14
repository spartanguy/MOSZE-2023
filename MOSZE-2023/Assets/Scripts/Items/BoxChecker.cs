using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ez a class vizsgálja hogy a küldetés dobozát eljuttaták-e a megfelelő helyre.
public class BoxChecker : MonoBehaviour
{
    public Transform parent;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Box")
        {
            Box a = (Box)other.gameObject.GetComponent(typeof(Box));
            GameObject b = other.gameObject;
            Debug.Log(b);
            if (a.keyQuestThing = true)
            {
                parent = transform.parent;
                GameObject npc = parent.GetChild(parent.childCount-3).gameObject;
                Npc npcScript =  (Npc) npc.GetComponent((typeof(Npc)));
                npcScript.isCompleted = true;
                Destroy(this.gameObject);
                Destroy(b);
            }
        }
    }
}
