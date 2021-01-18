using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public int Money;
    public float Energy;
    public uint Time;

    [SerializeField] AudioSource BadSound,ClickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        int Mtemp = Stats.Money ;
        float Etemp = Stats.Energy;

        if(!(Stats.ChangeMoney(Money)&& Stats.ChangeEnergy(Energy)))
        {

            Stats.Money = Mtemp;
            Stats.Energy = Etemp;
            Stats.ChangeMoney(0);
            Stats.ChangeEnergy(0);
            BadSound.Play();
        }
        else
        {
            ClickSound.Play();
            Stats.ChangeTime(Time);

            Stats.BLACKBAR.Play("BlackBarsDown");
        }


        
    }
}
