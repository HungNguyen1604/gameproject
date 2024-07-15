using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public Text cash_lb;

    public override void Setup(ViewParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = cash.ToString();
    }
    // Start is called before the first frame update
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);

    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = ((int)dataChange).ToString();
    }
    // Update is called once per frame
    public void OnAddCash()
    {
        DataAPIController.instance.AddCash(50, () =>
        {
            Debug.LogError("Cash add");

        });
    }
    public void OnWeaponView()
    {
        WeaponViewParam weaponViewParam = new WeaponViewParam();
        weaponViewParam.weaponID = 2;
        ViewManager.instance.SwitchView(ViewIndex.WeaponView, weaponViewParam);
    }
    public void LoadGame(int id)
    {
        ConfigMissionRecord configMissionRecord = ConfigManager.instance.configMission.GetRecordByKeySearch(id);

        LoadScenceManager.instance.LoadSceneByName(configMissionRecord.Scene_name, () =>
        {
            gameObject.SetActive(false);
            GameManager.instance.InitMission(configMissionRecord);
        });
    }
}
