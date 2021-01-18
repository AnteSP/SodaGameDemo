using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeActionTime : MonoBehaviour
{

    Action A;
    Text T;

    bool Add;

    int Mon;
    uint Tim;
    float En;

    public uint Offset;

    ChangeActionTime add, remove;

    // Start is called before the first frame update
    void Start()
    {
        A = transform.parent.Find("Button").GetComponent<Action>();
        T = transform.parent.Find("Description").GetComponent<Text>();

        Add = GetComponent<Image>().sprite.name == "plus";

        Mon = A.Money;
        Tim = A.Time;
        En = A.Energy;

        add = transform.parent.Find("Add").GetComponent<ChangeActionTime>();
        remove = transform.parent.Find("Remove").GetComponent<ChangeActionTime>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        if (Add)
        {
            Offset += 1;
        }
        else if(Offset != 0)
        {
            Offset -= 1;
        }

        if(T.text.Contains("+" + A.Money))
        {
            T.text = T.text.Replace("+" + A.Money + " Money", "+" + (Mon + (Mon * Offset)) + " Money");
        }
        else
        {
            T.text = T.text.Replace(A.Money + " Money", (Mon + (Mon * Offset)) + " Money");
        }

        if (T.text.Contains("+" + A.Energy))
        {
            T.text = T.text.Replace("+" + A.Energy + " Energy", "+" + (En + (En * Offset)) + " Energy");
        }
        else
        {
            T.text = T.text.Replace(A.Energy + " Energy", (En + (En * Offset)) + " Energy");
        }


        T.text = T.text.Replace("Takes " + A.Time + " Minute(s)", "Takes " + (Tim + (Tim * Offset)) + " Minute(s)");

        //T.text = T.text.Replace("look", "fuck");

        A.Money = (int)(Mon + (Mon * Offset));
        A.Energy = En + (En * Offset);
        A.Time = (Tim + (Tim * Offset));

        add.Offset = Offset;
        remove.Offset = Offset;

    }

    public void ChangeValues(int Money,float Energy,uint Time)
    {
        if (T.text.Contains("+" + A.Money))
        {
            T.text = T.text.Replace("+" + A.Money + " Money", "+" + Money + " Money");
        }
        else
        {
            T.text = T.text.Replace(A.Money + " Money", Money + " Money");
        }

        if (T.text.Contains("+" + A.Energy))
        {
            T.text = T.text.Replace("+" + A.Energy + " Energy", "+" + Energy + " Energy");
        }
        else
        {
            T.text = T.text.Replace(A.Energy + " Energy", Energy + " Energy");
        }


        T.text = T.text.Replace("Takes " + A.Time + " Minute(s)", "Takes " + Time + " Minute(s)");

        A = transform.parent.Find("Button").GetComponent<Action>();
        T = transform.parent.Find("Description").GetComponent<Text>();

        Add = GetComponent<Image>().sprite.name == "plus";

        A.Money = Money;
        A.Energy = Energy;
        A.Time = Time;

        Mon = A.Money;
        Tim = A.Time;
        En = A.Energy;

        
    }
}
