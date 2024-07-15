using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAPIController", menuName = "BY/DataAPIController", order = 1)]
public class DataAPIController : ScriptableObject
{
    public static DataAPIController instance;
    [SerializeField]
    private DataModel dataModel;
    public void InitData(Action callback)
    {
        instance = this;
        dataModel.LoadDataLocal((isNew) =>
        {
            Debug.LogError(" isnew : " + isNew);
            callback?.Invoke();
        });
    }
    public int GetCash()
    {
        return dataModel.ReadData<int>(DataPath.CASH);
    }
    public void AddCash(int cash, Action callback)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        num += cash;
        dataModel.UpdateData(DataPath.CASH, num, callback);
    }
    public WeaponData GetWeaponDataById(int id)
    {
        WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
        return wp;
    }
    public void UpgradelevelWeaponDataById(int id)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        if (num >= 30)
        {
            num -= 30;
            dataModel.UpdateData(DataPath.CASH, num, null);
            WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
            wp.level++;
            dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey(), wp, null);
        }

    }
    public UserInfo GetUserInfo()
    {
        return dataModel.ReadData<UserInfo>(DataPath.INFO);
    }
}
