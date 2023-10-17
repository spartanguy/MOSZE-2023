using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : Item
{
    public override void Upgrade() {
        Player.Instance.Heal();
        Destroy(gameObject);
    }
}
