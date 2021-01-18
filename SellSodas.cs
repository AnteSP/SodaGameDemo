using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellSodas : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler 
{

    public int ID;
    public ChooseSellSoda List;
    public Soda SodaInfo;
    public float TimeLeft;
    bool go;

    void Start()
    {

        TimeLeft = SodaInfo.GetTime();
        print(List.MLevel);
    }

    void Update()
    {
        if (go)
        {
            List.Time.text = "Sell Time: " + (SodaInfo.GetTime() * List.MLevel) + " seconds (" + (int)(TimeLeft) + " left)";
            List.Money.text = "Sell Price: " + SodaInfo.GetPrice();
        }
        

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData p)
    {
        List.Time.text = "Sell Time: ";
        List.Money.text = "Sell Price: ";
        go = false;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData p)
    {
        
        List.Time.text = "Sell Time: " + (SodaInfo.GetTime() * List.MLevel) + " seconds (" + (int)(TimeLeft) + " left)";
        List.Money.text = "Sell Price: " + SodaInfo.GetPrice();
        go = true;
    }

}
