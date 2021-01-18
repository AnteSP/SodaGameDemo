using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreMenu : Resource
{

    [SerializeField] GameObject Menu;

    public List<int> ItemsForSale = new List<int>();
    public List<StoreItem.ItemType> ItemTypes = new List<StoreItem.ItemType>();
    public List<int> RecipeNums = new List<int>();
    public List<int> UpgradeNums = new List<int>();

    [SerializeField] GameObject ItemPrefab;

    [SerializeField] Scrollbar HorizScroll;

    [SerializeField] ChooseSellSoda Cs;

    float Ratio;

    void UpdateShop()
    {
        for(int i = 0; i < ItemPrefab.transform.parent.childCount - 1; i++)
        {
            Destroy(ItemPrefab.transform.parent.GetChild(i + 1).gameObject);
        }

        for (int i = 0; i < ItemsForSale.Count; i++)
        {

            GameObject temp = Instantiate(ItemPrefab, ItemPrefab.transform.parent);
            temp.SetActive(true);

            Transform obj = temp.transform.GetChild(0);

            StoreItem SMen = obj.GetComponent<StoreItem>();

            SMen.Menu = this;
            SMen.Cs = Cs;
            SMen.Cost = -Items.Price[ItemsForSale[i]];
            SMen.ItemID = ItemsForSale[i];
            SMen.Type = ItemTypes[i];
            SMen.RecipeNum = RecipeNums[i];
            SMen.UpgradeNum = UpgradeNums[i];


            obj.GetComponent<Image>().sprite = Items.PICS[ItemsForSale[i]];
            obj.GetComponent<Tooltip>().tooltip = Items.NAMES[ItemsForSale[i]] + "\n(" + Items.DESCRIPTS[ItemsForSale[i]] + ")";
            
            temp.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "[ " + Items.Price[ItemsForSale[i]];
            
        }

        //Amount of cells that can fit into 1 frame (should be 6 if noone changed anything)
        Ratio = HorizScroll.transform.parent.GetComponent<RectTransform>().sizeDelta.x / ItemPrefab.GetComponent<RectTransform>().sizeDelta.x;

        HorizScroll.size = Ratio/(  ItemsForSale.Count > Ratio ? ItemsForSale.Count : 1);
        //print(" did " + (ItemsForSale.Length - 2f + (Ratio / 2f)) );

        ItemPrefab.SetActive(false);
    }

    public void MoveScroll()
    {
        float move = -((1 / (HorizScroll.size)) * HorizScroll.transform.parent.GetComponent<RectTransform>().sizeDelta.x * HorizScroll.value);
        move *= (float)(ItemsForSale.Count - Ratio) / (float)ItemsForSale.Count;

        ItemPrefab.transform.parent.GetComponent<RectTransform>().localPosition = new Vector2(move, 0);

    }

    public override void Use(float Amount)
    {

        Menu.SetActive(!Menu.activeSelf);
        UpdateShop();
    }

    public void RemoveItem(int ItemForSaleIndex)
    {


        ItemsForSale.RemoveAt(ItemForSaleIndex);
        ItemTypes.RemoveAt(ItemForSaleIndex);
        RecipeNums.RemoveAt(ItemForSaleIndex);
        UpgradeNums.RemoveAt(ItemForSaleIndex);
        NameIndic.Indicate("");
        HorizScroll.value = 0;
        UpdateShop();

    }
}
