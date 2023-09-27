using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCfg
{
    public string name = "Default Player";

    public int maxHp = 10;
    public int curHp;
    public float maxMana = 10;
    public float curMana;
    public int maxExp = 10;
    public int curExp = 1;
    public float speed_regenMana = 1f;

    public float curSpeed = 4;
    public float curSpeedAtk = 1;
    public string dayCreate;
    public int level = 1;
    public int coin = 0;
    public int damage = 1;
    public int defend = 0;
    public int damageEquip;
    public int defendEquip;

    public int[] arr_Question;
    public bool[] arr_isCompleteQues;
    public int[] arr_quantyQues;

    public string slotEquipment_1;
    public string slotEquipment_2;
    public string slotEquipment_3;
    public string slotEquipment_4;

    //public string[] arr_Enemy;
    //public int[] arr_killEnemy;

    public void SetDefaultCfg()
    {
        curHp = maxHp;
        curMana = maxMana;

        #region arr enemy
        //ListEnemy LE = Resources.Load<ListEnemy>("Enemy/ListEnemy");
        //List<EnemyCfg> List_Enemy = LE.List_Enemy;

        //arr_Enemy = new string[List_Enemy.Count];
        //arr_killEnemy = new int[List_Enemy.Count];
        //for (int i = 0; i < List_Enemy.Count; i++)
        //{
        //    arr_Enemy[i] = List_Enemy[i].name;
        //    arr_killEnemy[i] = 0;
        //}
        #endregion

        ListQues LQ = Resources.Load<ListQues>("Question/ListQuestion");
        List<QuesCfg> List_Ques = LQ.List_Ques;

        arr_Question = new int[List_Ques.Count];
        arr_isCompleteQues = new bool[List_Ques.Count];
        arr_quantyQues = new int[List_Ques.Count];
        for (int i = 0; i < List_Ques.Count; i++)
        {
            arr_Question[i] = List_Ques[i].numberQuestion;
            arr_isCompleteQues[i] = false;
            arr_quantyQues[i] = 0;
        }
    }
    
    public void UpdateQuantyQues(GameObject prefab)
    {
        ListQues LQ = Resources.Load<ListQues>("Question/ListQuestion");
        List<QuesCfg> List_Ques = LQ.List_Ques;

        for (int i = 0; i < List_Ques.Count; i++)
        {
            if (prefab.name == List_Ques[i].prefab.name && arr_isCompleteQues[i] == false && level >= List_Ques[i].levelQues)
                arr_quantyQues[i] += 1;
        }
    }

    public void UpdateItemEquipment()
    {
        ResetDamageAndDefend();
        ListItem LI = Resources.Load<ListItem>("ListItem");

        if (slotEquipment_1 != null)
            for (int i = 0; i < LI.itemList.Count; i++)
                if (slotEquipment_1 == LI.itemList[i].name) { damageEquip = LI.itemList[i].damage; break; }

        if (slotEquipment_2 != null)
            for (int i = 0; i < LI.itemList.Count; i++)
                if (slotEquipment_2 == LI.itemList[i].name) { defendEquip = LI.itemList[i].defend; break; }

        //slot 3 ... slot4
        UpdateDamageAndDefend();
    }

    private void UpdateDamageAndDefend()
    {
        damage += damageEquip;
        defend += defendEquip;
    }

    private void ResetDamageAndDefend()
    {
        damage -= damageEquip;
        defend -= defendEquip;
        damageEquip = 0;
        defendEquip = 0;
    }

    public void LevelUp()
    {
        maxExp *= 5;
        damage += 1;
        maxHp += 5;
        maxMana += 10;
        speed_regenMana += 0.2f;
        curSpeedAtk += 0.1f;
        curSpeed += 0.5f;
        defend += 1;
    }
}
