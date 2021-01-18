using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Resource
{

    public int Money;

    public override void Use(float Amount)
    {
        if (Items.Add(ItemID, Quantity))
        {
            Stats.ChangeMoney(Money);
            if(Money != 0)
            {
                Stats.KACHING.Play();
            }

            Destroy(gameObject);
        }
        else
        {
            Stats.DisplayMessage("Not enough room for this item");
        }
    }
}
