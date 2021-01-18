using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseSellSoda : Resource
{
    [SerializeField] GameObject Menu;

    //[SerializeField] int[] ItemsForSale;
    List<int> ItemsForSale = new List<int>();

    [SerializeField] GameObject ItemPrefab;

    [SerializeField] Scrollbar HorizScroll;

    float Ratio;

    [SerializeField] GameObject EmptyNotif;

    [SerializeField] Transform Content;

    public AudioSource CashSound;

    public TextMeshProUGUI Money, Time;

    public float MLevel = 1f;

    private void Start()
    {
        
        MLevel = 1;
    }

    public void InitializeShop()
    {
        ItemsForSale.Clear();

        for(int i = 0; i < Content.childCount; i++)
        {
            Destroy(Content.GetChild(0).gameObject);
        }

        for(int i = 0; i < NewInv.Frames; i++)
        {
            for(int j = 0; j < Items.Sodas.Length; j++)
            {
                if (Items.ITEMS[i] == Items.Sodas[j])
                {
                    ItemsForSale.Add(Items.ITEMS[i]);
                }
            }
        }

        bool showEmptyNotif = true;
        for (int i = 0; i < ItemsForSale.Count; i++)
        {
            GameObject temp = Instantiate(ItemPrefab, Content);
            temp.SetActive(true);
            Transform obj = temp.transform.GetChild(0);
            obj.GetComponent<Image>().sprite = Items.PICS[ItemsForSale[i]];
            obj.GetComponent<Tooltip>().tooltip = Items.NAMES[ItemsForSale[i]];
            //temp.transform.GetChild(0).GetComponent<ChooseSellSodaHelper>().ID = ItemsForSale[i];
            //temp.transform.GetChild(0).GetComponent<ChooseSellSodaHelper>().ListToAddTo = ListToAddTo;
            obj.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + Items.ITEMQUANTITY[Items.IndexOfXinY(ItemsForSale[i] , Items.ITEMS)];

            SellSodas S = obj.GetComponent<SellSodas>();
            S.ID = ItemsForSale[i];
            S.List = this;

            S.SodaInfo = Items.SodaInfo[ Items.IndexOfXinY( ItemsForSale[i] , Items.Sodas ) ];

            showEmptyNotif = false;
        }
        EmptyNotif.SetActive(showEmptyNotif);

        //Amount of cells that can fit into 1 frame (should be 6 if noone changed anything)
        Ratio = HorizScroll.transform.parent.GetComponent<RectTransform>().sizeDelta.x / ItemPrefab.GetComponent<RectTransform>().sizeDelta.x;

        HorizScroll.size = Ratio / (ItemsForSale.Count > Ratio ? ItemsForSale.Count : 1);
        //print(" did " + (ItemsForSale.Length - 2f + (Ratio / 2f)) );

        ItemPrefab.SetActive(false);
    }

    public void UpdateNumbers()
    {

        bool showEmptyNotif = true;
        for (int i = 0; i < ItemsForSale.Count; i++)
        {
            GameObject temp = Content.GetChild(i).gameObject;
            Transform obj = temp.transform.GetChild(0);
            //if ( !(Items.IndexOfXinY(ItemsForSale[i] , Items.ITEMS) == -1) && !( Items.ITEMQUANTITY[Items.IndexOfXinY(ItemsForSale[i] , Items.ITEMS )] <= 0 ))
            if ( !(Items.IndexOfXinY(ItemsForSale[i] , Items.ITEMS) == -1))
            {
                obj.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + Items.ITEMQUANTITY[Items.IndexOfXinY(ItemsForSale[i] , Items.ITEMS)];

                SellSodas S = obj.GetComponent<SellSodas>();
                S.ID = ItemsForSale[i];
                S.List = this;

                showEmptyNotif = false;
            }
            else
            {
                NameIndic.Indicate("");
                Time.text = "Sell Time:";
                Money.text = "Sell Price:";
                Destroy(temp);
                ItemsForSale.RemoveAt(i);
                
            }
        }
        EmptyNotif.SetActive(showEmptyNotif);
    }

    void AddMissingItems()
    {
        List<int> Temp = new List<int>();
        for (int i = 0; i < NewInv.Frames; i++)
        {
            for (int j = 0; j < Items.Sodas.Length; j++)
            {
                if (Items.ITEMS[i] == Items.Sodas[j])
                {
                    Temp.Add(Items.ITEMS[i]);
                }
            }
        }

        for(int i = 0; (i < Temp.Count) && (Temp.Count != ItemsForSale.Count); i++)
        {

            if(Temp[i] != ItemsForSale[i])
            {
                GameObject temp = Instantiate(ItemPrefab, Content);
                temp.SetActive(true);
                Transform obj = temp.transform.GetChild(0);
                obj.GetComponent<Image>().sprite = Items.PICS[Temp[i]];
                obj.GetComponent<Tooltip>().tooltip = Items.NAMES[Temp[i]];
                //temp.transform.GetChild(0).GetComponent<ChooseSellSodaHelper>().ID = ItemsForSale[i];
                //temp.transform.GetChild(0).GetComponent<ChooseSellSodaHelper>().ListToAddTo = ListToAddTo;
                obj.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + Items.ITEMQUANTITY[Items.IndexOfXinY(Temp[i], Items.ITEMS)];

                SellSodas S = obj.GetComponent<SellSodas>();
                S.ID = Temp[i];
                S.List = this;

                S.SodaInfo = Items.SodaInfo[Items.IndexOfXinY(Temp[i], Items.Sodas)];
            }
        }

        ItemsForSale = Temp;

    }

    public void MoveScroll()
    {
        float move = -((1 / (HorizScroll.size)) * HorizScroll.transform.parent.GetComponent<RectTransform>().sizeDelta.x * HorizScroll.value);
        move *= (float)(ItemsForSale.Count - Ratio) / (float)ItemsForSale.Count;

        ItemPrefab.transform.parent.GetComponent<RectTransform>().localPosition = new Vector2(move, 0);

    }

    public override void Use(float Amount)
    {
        if(ItemsForSale.Count == 0)
        {
            InitializeShop();
        }
        UpdateNumbers();
        AddMissingItems();
        ToggleMenu();
        
    }

    public void ToggleMenu()
    {
        Menu.transform.localPosition = (Menu.transform.localPosition.y > -100) ? new Vector3(0, -999999, 0) : Vector3.zero;
    }

}
