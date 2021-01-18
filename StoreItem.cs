using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{

    public int Cost,ItemID,RecipeNum,UpgradeNum;

    public StoreMenu Menu;

    public ChooseSellSoda Cs;

    public enum ItemType
    {

        Disposable,Recipe,Upgrade

    }

    public ItemType Type;

    public void Buy()
    {
        if (Stats.ChangeMoney(Cost))
        {
            Stats.KACHING.Play();
            if(Type == ItemType.Disposable)
            {

                Items.Add(ItemID, 1);

            } else if(Type == ItemType.Recipe)
            {

                SodaMachine.CreateRecipe(SodaMachine.Recipes[RecipeNum]);
                Menu.RemoveItem(Menu.ItemsForSale.IndexOf(ItemID));
                
                

            } else if(Type == ItemType.Upgrade)
            {

                Cs.MLevel /= UpgradeNum;


            }
            
        }
        else
        {

            Stats.DisplayMessage("Not enough money :(");

        }
    }
}
