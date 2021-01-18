using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SodaMachine : MonoBehaviour
{
    Animator A;
    [SerializeField] GameObject Menu;
    static public GameObject SodaMenu;

    [SerializeField] Action SellSodas;
    static Action SellS;

    [SerializeField] GameObject RecipePrefab;
    static GameObject RePrefab;

    [SerializeField] Image SodaPreview;
    static Image SodaPrev;
    static TextMeshProUGUI SodaPr;


    bool Open;

    public static int[] Ings = new int[1];
    public static List<Recipe> Recipes = new List<Recipe>();

    //static public bool DoingBottle = false;
    static public int ActiveSoda = -1;

    [SerializeField] Button SellButton;
    static Button SellBut;
    

    // Start is called before the first frame update
    void Start()
    {
        SodaPrev = SodaPreview;
        RePrefab = RecipePrefab;
        SodaPr = SodaPreview.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();

        A = GetComponent<Animator>();
        SodaMenu = Menu;

        SellS = SellSodas;

        SellBut = SellButton;

        //Create All recipes
        Recipes.Add(new Recipe( new int[] { 1, 2 }, 3, 10));
        Recipes.Add(new Recipe( new int[] { 4, 5 , 2 }, 7, 35));

        CreateRecipe(Recipes[0]);
        //CreateRecipe(Recipes[1]);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        Open = !Open;
        SelectRec.Selecting = !SelectRec.Selecting;
        A.SetBool("Open", Open);
        A.SetTrigger("Go");
        
        
    }

    public void ForceMenu(bool state)
    {

        A.SetBool("Open", state);
        A.SetTrigger("Go");
        Open = state;

    }
    /*
    public static void Subtract(string name)
    {
        int contains = -1;
        for (int i = 0; i < Ings.Count; i++)
        {

            contains = Ings[i].name == name ? i : contains;

        }

        if(contains != -1)
        {
            Ings.RemoveAt(contains);

            Destroy(SodaMenu.transform.Find("Component Parent").Find(name).gameObject);
            UpdateTexts();
        }

        
    }

    public static void SubtractBottle(string name)
    {
        int contains = -1;
        for (int i = 0; i < Ings.Count; i++)
        {

            contains = Ings[i].name == name ? i : contains;

        }

        if (contains != -1)
        {
            Ings.RemoveAt(contains);

            Destroy(SodaMenu.transform.Find("Component Parent").Find(name).gameObject);

            GameObject temp = Instantiate(TempSto, SodaMenu.transform.Find("Component Parent"));
            temp.transform.SetAsFirstSibling();
            temp.name = "Add Bottle";

            SellBut.interactable = false;
            SellBut.GetComponent<Tooltip>().tooltip = "Requires a bottle/can";

            UpdateTexts();
        }

        



    }

    public static void Insert(Ingredient I)
    {
        if (DoingBottle)
        {
            InsertBottle(I);
        }
        else
        {
            bool contains = false;
            for (int i = 0; i < Ings.Count; i++)
            {

                contains = Ings[i].name == I.name ? true : contains;

            }

            

            if (SodaMenu.active && new Ingredient(0).name != I.name && !contains && !IsBottle(I.name))
            {
                Transform Parent = SodaMenu.transform.Find("Component Parent");

                GameObject temp = Instantiate(CPref, Parent);
                temp.name = I.name;
                temp.GetComponent<Image>().sprite = I.Pic;
                temp.GetComponent<Tooltip>().tooltip = I.name;

                Parent.Find("Add Something").SetAsLastSibling();


                Slider.ForceBack();

                Ings.Add(I);
                UpdateTexts();

            }
        }

        
    }

    public static bool IsBottle(string name)
    {

        bool isBottle = false;
        switch (name)
        {
            case "Recycled Bottle":
                isBottle = true;
                break;


        }

        return (isBottle);
    }

    public static void InsertBottle(Ingredient I)
    {
        bool contains = false;
        for (int i = 0; i < Ings.Count; i++)
        {

            contains = Ings[i].name == I.name ? true : contains;

        }

        bool isBottle = false;
        switch (I.name)
        {
            case "Recycled Bottle":
                isBottle = true;
                break;


        }

        if (SodaMenu.active && new Ingredient(0).name != I.name && !contains && IsBottle(I.name))
        {
            Transform Parent = SodaMenu.transform.Find("Component Parent");

            GameObject temp = Instantiate(CPref, Parent);
            temp.name = I.name;
            temp.GetComponent<Image>().sprite = I.Pic;
            temp.GetComponent<Tooltip>().tooltip = I.name;
            temp.transform.SetAsFirstSibling();
            temp.transform.GetChild(0).GetComponent<RemoveSComp>().Bottle = true;

            Destroy(Parent.Find("Add Bottle").gameObject);
            SellBut.interactable = true;
            SellBut.GetComponent<Tooltip>().tooltip = "";


            Slider.ForceBack();

            Ings.Add(I);
            
            UpdateTexts();

        }
    }

    static void UpdateTexts()
    {
        float price = 0, sph = 0, rph = 0, ml = 1;
        for (int i = 0; i < Ings.Count; i++)
        {
            price += Ings[i].price;
            ml += (float)Ings[i].Mlevel / 10;

        }

        sph = ml;
        rph = sph * price;

        Price.text = "[" + price;
        SoldPH.text = sph + "";
        RPH.text = "[" + rph;
        ML.text = "ML: " + ml;

        
        ChangeActionTime[] temp = SellS.transform.parent.GetComponentsInChildren<ChangeActionTime>();
        temp[0].ChangeValues((int)price, SellS.Energy, SellS.Time);
        temp[1].ChangeValues((int)price, SellS.Energy, SellS.Time);

        print("CHANGED MONEY " + (int)(price * 100));
    }
    */

    public static void ChooseRecipe(int Ind)
    {
        if(Ind == -1)
        {

            SodaPrev.sprite = Items.PICS[0];
            SodaPrev.GetComponent<Tooltip>().tooltip = "";
            SodaPr.text = "";
            Ings = new int[1];
            SellBut.GetComponent<Tooltip>().tooltip = "Select recipe first";
            SellBut.interactable = false;
        }
        else
        {
            SodaPrev.sprite = Recipes[Ind].Pic;
            SodaPrev.GetComponent<Tooltip>().tooltip = Recipes[Ind].Name;
            SodaPr.text = "Base Price: " + Recipes[Ind].BasePrice;
            print("Tried to change prev image");

            Ings = Recipes[Ind].Ingredients;

            SellBut.interactable = true;
            SellBut.GetComponent<Tooltip>().tooltip = "";
            

        }
        ActiveSoda = Ind;



    }

    public static void CreateRecipe(Recipe R)
    {

        void SetToItem(Transform G, Sprite S, string T)
        {
            G.GetComponent<Image>().sprite = S;
            G.GetComponent<Tooltip>().tooltip = T;
        }

        GameObject temp = Instantiate(RePrefab, RePrefab.transform.parent);
        temp.GetComponent<SelectRec>().RecipeIndex = Recipes.IndexOf(R);
        
        temp.SetActive(true);

        Transform Prev = temp.transform.GetChild(0);
        Transform IngPar = temp.transform.GetChild(1);

        SetToItem(Prev, R.Pic,R.Name + "\nSellTime: " + Items.SodaInfo[Items.IndexOfXinY(R.ItemID, Items.Sodas)].GetTime() + "s" + "\nSellPrice: " + Items.SodaInfo[Items.IndexOfXinY(R.ItemID, Items.Sodas)].GetPrice());

        SetToItem(IngPar.GetChild(0), Items.PICS[R.Ingredients[0]], Items.NAMES[R.Ingredients[0]]);


        for(int i = 1; i < R.Ingredients.Length; i++)
        {
            Instantiate(IngPar.GetChild(0) , IngPar);
            

            SetToItem(IngPar.GetChild(i), Items.PICS[R.Ingredients[i]], Items.NAMES[R.Ingredients[i]]);

        }

    }

}
