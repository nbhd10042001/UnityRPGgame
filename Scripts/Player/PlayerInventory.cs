

public class PlayerInventory 
{
    public int OreGreen;
    public int OrePurple;
    public int OreGold;
    public int OreOrange;
    public int OreRed;
    public int OreCyan;
    public int OreGreenSuper;

    public int SwordEvo2;
    public int SwordEvo3;

    public int curCapacity;
    public int maxCapacity = 100;

    //public string[] arr_Ore;
    //public int[] arr_amountOre;

    public void SetDefault()
    {
        #region arr item
        //ListItem LI = Resources.Load<ListItem>("Items/ListItem");
        //List<Item> List_Items = LI.itemList;
        //arr_Ore = new string[List_Items.Count];
        //arr_amountOre = new int[List_Items.Count];
        //for (int i = 0; i < List_Items.Count; i++)
        //{
        //    arr_Ore[i] = List_Items[i].name;
        //    arr_amountOre[i] = 0;
        //}
        #endregion
    }

    public void AddItem(string name, int amount)
    {
        if (name == "OreGreen") { OreGreen += amount; }
        else if (name == "OrePurple") { OrePurple += amount; }
        else if (name == "OreGold") { OreGold += amount; }
        else if (name == "OreOrange") { OreOrange += amount; } 
        else if (name == "OreRed") { OreRed += amount; }
        else if (name == "OreCyan") { OreCyan += amount; }
        else if (name == "OreGreenSuper") { OreGreenSuper += amount; }

        else if (name == "SwordEvo2") { SwordEvo2 += amount; }
        else if (name == "SwordEvo3") { SwordEvo3 += amount; }
    }

    public int GetAmountItem (string name)
    {
        if (name == "OreGreen") { return OreGreen; }
        else if (name == "OrePurple") { return OrePurple; }
        else if (name == "OreGold") { return OreGold; }
        else if (name == "OreOrange") { return OreOrange; }
        else if (name == "OreRed") { return OreRed; }
        else if (name == "OreCyan") { return OreCyan; }
        else if (name == "OreGreenSuper") { return OreGreenSuper; }

        else if (name == "SwordEvo2") { return SwordEvo2; }
        else if (name == "SwordEvo3") { return SwordEvo3; }
        else return 0;
    }

    public void UpdateCurCapacity()
    {
        curCapacity = (OreGreen + OrePurple + OreGold + OreOrange + OreRed + OreCyan + OreGreenSuper 
            + SwordEvo2 + SwordEvo3);
    }

    public void LevelUp()
    {
        maxCapacity += 40;
    }

}
