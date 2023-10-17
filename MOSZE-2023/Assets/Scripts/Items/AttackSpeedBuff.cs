using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBuff : Item
{
    public override void Upgrade()
    {
        Player.Instance.attackspeedBuff++;
        Destroy(gameObject);
    }
}
