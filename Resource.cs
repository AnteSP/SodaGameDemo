using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int Max;
    public float Current;
    public int ItemID,Quantity;

    public float CollectTime;

    public virtual void Use(float Amount)
    {

        Current = Current - Amount < 0 ? 0 : Current - Amount;

        if (TryGetComponent<Animator>(out Animator anim))
        {
            anim.SetTrigger("Go");

        }

        if(!Items.Add(ItemID, Quantity))
        {

            Stats.DisplayMessage("Not enough Inventory space");

        }

    }

    public void Add(float Amount)
    {

        Current = Current + Amount > Max ? Max : Current + Amount;



    }
}
