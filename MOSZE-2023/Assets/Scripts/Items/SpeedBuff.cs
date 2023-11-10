using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Item
{
    public override void Upgrade()
    {
        Player.Instance.speedBuff++;
        Player.Instance.SetSpeed();
        Destroy(gameObject);
    }
}
