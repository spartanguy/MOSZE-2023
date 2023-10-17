using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth_upgrade : Item
{
    public override void Upgrade() {
        Player.Instance.SetHp(1);
        Destroy(gameObject);
    }
}
