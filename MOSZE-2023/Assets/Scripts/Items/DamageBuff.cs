using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : Item
{
       public override void Upgrade()
    {
        Player.Instance.damageBuff++;
        Destroy(gameObject);
    }
}
