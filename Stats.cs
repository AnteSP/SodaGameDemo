using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] Text moneyT;
    static Text MONEYTEXT;
    [SerializeField] Image EnergyBar;
    static Image ENERGYBAR;
    [SerializeField] Text TimeT;
    static Text TIMETEXT;
    [SerializeField] Text DayT;
    static Text DAYTEXT;

    static string[] Days = { "Sunday" , "Monday", "Tueasday", "Wednesday", "Thursday", "Friday", "Saturday"};

    static public int Money;
    static public float Energy = 100;
    public static int EnergyLimit = 100;
    static uint Time = 480;
    static uint Day = 1;
    static Vector2 Initial;

    static int Digits;

    float Timer;

    [SerializeField] GameObject ActionPrefab;
    static GameObject ACT;
    [SerializeField] Animator BlackBar;
    public static Animator BLACKBAR;
    [SerializeField] GameObject DeadlinePrefab;
    static GameObject DEADLINE;

    static List<Deadline> Deds = new List<Deadline>();

    [SerializeField] GameObject Message;
    static GameObject MESSAGE;

    public static bool CanGoUp;

    public static bool ChoosingSells;

    public static bool AllowSelecting = true;

    [SerializeField] AudioSource Kaching;
    static public AudioSource KACHING;

    static int timePassage = 100;

    // Start is called before the first frame update
    void Start()
    {
        DAYTEXT = DayT;
        TIMETEXT = TimeT;
        MONEYTEXT = moneyT;
        ENERGYBAR = EnergyBar;
        ACT = ActionPrefab;
        DEADLINE = DeadlinePrefab;
        MESSAGE = Message;
        Initial = EnergyBar.rectTransform.localPosition;
        Digits = moneyT.text.Length;
        BLACKBAR = BlackBar;
        KACHING = Kaching;

        ChangeTime(0);
        ChangeMoney(0);
        ChangeEnergy(0);

        CreateDeadline("Pay 1000 for the soda machine", 7*24*60, -1000, 0,"your friend came by to collect the 1000 money you agreed to pay. It went well! The soda machine is now all yours!");
        DEADLINE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (AllowSelecting)
        {

            UpdateSelectableIcon();

        }

        Timer += UnityEngine.Time.deltaTime;
        ChangeTime( (Mathf.RoundToInt(Timer*60) % timePassage == 0 ) ? (uint)1 : 0);

    }

    public static void AccelerateTime(float a)
    {
        a = 1 - a;
        timePassage = (int)Mathf.Ceil(a * a * 100);


    }

    void UpdateSelectableIcon()
    {

            bool UseResource = Input.GetKeyUp(KeyCode.Space) && ObjectDepth.Selected.name != "Player" && ObjectDepth.Space.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TimeGone");

            if (UseResource)
            {
                ObjectDepth.Space.GetComponent<Animator>().SetBool("Pressed", true);

                Animator temp = ObjectDepth.Space.GetComponent<Animator>();
                temp = ObjectDepth.Space.GetChild(0).GetComponent<Animator>();
                temp.SetTrigger("Go");
                temp.SetFloat("Speed", 1f / (ObjectDepth.Selected.GetComponent<Resource>().CollectTime + 0.01f));

                if (ObjectDepth.Selected.tag == "Machine")
                {

                    ObjectDepth.Selected.GetComponent<Resource>().Use(1);

                }
                else if (ObjectDepth.Selected.tag == "SodaMachine")
                {
                    ObjectDepth.Selected.GetComponent<SodaMachine>().ToggleMenu();

                }

                
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ObjectDepth.Space.GetComponent<Animator>().SetBool("Pressed", true);

            }

            CanGoUp = Input.GetKeyUp(KeyCode.Space) ? true : CanGoUp;

        }

    public static bool ChangeMoney(int Amount)
    {

        bool temp = Money + Amount < 0;
        
        Money = temp ? 0 : Money + Amount;
        
        string Temp = "";
        int overflow = 9;

        for (int i = 1; i < Digits; i++)
        {
            if (Money >= Mathf.Pow(10, i))
            {
            }
            else
            {
                Temp += " ";
            }
            overflow += 9 * Mathf.RoundToInt(Mathf.Pow(10, i));
        }

        Temp += Money;

        bool Overflown = Temp.Length > Digits;

        MONEYTEXT.text = Overflown? overflow + "": Temp;
        Money = Overflown ? overflow : Money;

        MONEYTEXT.text = Money + "";

        return (!temp);
    }

    public static bool ChangeEnergy(float Amount)
    {

        bool temp = Energy + Amount < 0;

        Energy = temp ? 0 : Energy + Amount;

        Energy = Energy + Amount > EnergyLimit ? EnergyLimit : Energy;

        ProgressBar.DoLine(ENERGYBAR.rectTransform, Initial, 142, Energy / EnergyLimit, true);

        ENERGYBAR.transform.parent.Find("Text").GetComponent<Text>().text = Energy + "/" + EnergyLimit;
        
        return (!temp);
    }

    public static void ChangeTime(uint Amount)
    {

        Day += (uint) Mathf.FloorToInt((float)(Time + Amount) / (24f * 60f));
        //Amount -= (24*60) * (uint)Mathf.FloorToInt((float)Amount / (24f * 60f));
        uint DayTemp = Day;
        while(DayTemp >= 7)
        {
            DayTemp -= 7;


        }

        DAYTEXT.text = "Day: " + Day + " " + Days[DayTemp];

        Time -= ( 24*60 * (uint)Mathf.FloorToInt((float)(Time + Amount) / (24f * 60f)));
        Time += Amount;

        string Hour = Mathf.FloorToInt((float)Time / 60f) +"";
        Hour = Hour.Length < 2 ? "0" + Hour : Hour;

        string Minute = Mathf.FloorToInt((float)Time % 60f) + "";
        Minute = Minute.Length < 2 ? "0" + Minute : Minute;


        TIMETEXT.text = Hour + ":" + Minute;

        CheckDeadlines((int)Amount);

    }

    public static void CreateAction(string Title,string Description,int Changemoney,float ChangeEnergy,uint ChangeTime)
    {
        GameObject Action = Instantiate(ACT, ACT.transform.parent);
        Action.transform.Find("Title").GetComponent<Text>().text = Title;
        Action.transform.Find("Description").GetComponent<Text>().text = Description;
        Action temp = Action.transform.GetComponentInChildren<Action>();
        temp.Money = Changemoney;
        temp.Energy = ChangeEnergy;
        temp.Time = ChangeTime;

    }

    public static void CreateDeadline(string Title,int Minutes,int ChangeMoney,float ChangeEnergy,string FinishText)
    {
        GameObject Ded = Instantiate(DEADLINE, DEADLINE.transform.parent);
        /*
        bool Lessthan3days = Minutes < 24 * 60 * 3;
        bool Lessthanhour = Minutes < 61;
        print((Lessthanhour) ? Minutes : Minutes / 60);
        string TimeLeft = (Lessthan3days ? ( (Lessthanhour) ? Minutes : Minutes/60 ) : Minutes/(24*60)) + (Lessthan3days ? ((Lessthanhour) ? " Minutes" : " Hours")  : " Days");
        
        string temp = Title + " (Due in " + TimeLeft + ")";
        print(temp);
        Ded.transform.Find("Text").GetComponent<Text>().text = "fuck";
        */
        Ded.SetActive(true);
        Deds.Add( new Deadline(ChangeMoney,Minutes,Ded,Title,FinishText));
        //Deds[Deds.Count - 1].Object.SetActive(true);
        /*
        Deds[Deds.Count - 1].Money = ChangeMoney;
        Deds[Deds.Count - 1].Energy = ChangeEnergy;
        Deds[Deds.Count - 1].Minutes = Minutes;
        Deds[Deds.Count - 1].Object = Ded;
        Deds[Deds.Count - 1].Title = Title;
        Deds[Deds.Count - 1].FinishText = FinishText;
        */
    }

    static void CheckDeadlines(int TimePassed)
    {
        for(int i = 0; i < Deds.Count; i++)
        {
            if(Deds[i].Minutes < TimePassed)
            {
                if(!(ChangeMoney(Deds[i].Money) ))
                {
                    GameOver();
                    Destroy(Deds[i].Object);
                    Deds.Remove(Deds[i]);
                }
                else
                {
                    DisplayMessage(Deds[i].FinishText);
                    Destroy(Deds[i].Object);
                    Deds.Remove(Deds[i]);
                }
            }
            else
            {

                Deds[i].Minutes -= TimePassed;
                Deds[i].Refresh();

            }
            

        }


    }

    static public void GameOver()
    {

        print("GAME OVER");
        DisplayMessage("YOU FAILED TO MEET YOUR DEADLINE\n\nconsider this a gameover... But feel free to still explore around");

    }

    static public void DisplayMessage(string text)
    {
        
        MESSAGE.SetActive(text != null);
        MESSAGE.transform.Find("Text").GetComponent<Text>().text = text;
    }

    static public void PendMessage(string text)
    {

        ForceHandlesBack.PendingMessage = text;
    }

}

class Deadline : MonoBehaviour
{
    public int Money;
    public float Energy;
    public int Minutes;
    public GameObject Object;
    public string Title,FinishText;

    public Deadline(int Mon,int Mins,GameObject Obj, string Titl, string FinishTex)
    {
        Money = Mon;
        Minutes = Mins;
        Object = Obj;
        Title = Titl;
        FinishText = FinishTex;
    }

    public void Refresh()
    {

        bool Lessthan3days = Minutes < 24 * 60 * 3;
        bool Lessthanhour = Minutes < 61;
        //string TimeLeft = (Lessthan3days ? Minutes / 60 : Minutes / (24 * 60)) + (Lessthan3days ? " Hours" : " Days");
        string TimeLeft = (Lessthan3days ? ((Lessthanhour) ? Minutes : Minutes / 60) : Minutes / (24 * 60)) + (Lessthan3days ? ((Lessthanhour) ? " Minutes" : " Hours") : " Days");
        Object.transform.Find("Text").GetComponent<Text>().text = Title + " (Due in " + TimeLeft + ")";

    }

}

public class Recipe
{

    public int[] Ingredients;
    public string Name;
    public int BasePrice;
    public Sprite Pic;
    public int ItemID;

    public Recipe(int[] ingredients, int itemID, int baseprice)
    {

        ItemID = itemID;
        Ingredients = ingredients;
        Name = Items.NAMES[ItemID];
        BasePrice = baseprice;
        Pic = Items.PICS[ItemID];
        

    }

}
