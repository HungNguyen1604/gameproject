using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField]
    public UserInfo info;
    [SerializeField]
    public MissionData missionData;
    [SerializeField]
    public Inventory inventory;
}
[Serializable]
public class UserInfo
{
    public string username;
    public int level;
    public int exp;
    public List<int> guns_equip = new List<int>();
}
[Serializable]
public class Inventory
{
    public int cash;
    [SerializeField]
    public Dictionary<string, WeaponData> dic_weapon = new Dictionary<string, WeaponData>();
}
[Serializable]
public class MissionData
{
    [SerializeField]
    public Dictionary<string, MissionDataItem> dic_mission = new Dictionary<string, MissionDataItem>();
}
[Serializable]
public class WeaponData
{
    public int id;
    public int level;
}
[Serializable]
public class MissionDataItem
{
    public int id;
    public int star;
}

public class DataPath
{
    public const string CASH = "inventory/cash";
    public const string DIC_WEAPON = "inventory/dic_weapon";
    public const string INFO = "info";
}
