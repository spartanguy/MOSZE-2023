using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public override void Upgrade()
    {
        Player.Instance.shield += 4;
        Destroy(gameObject);

    }
}
