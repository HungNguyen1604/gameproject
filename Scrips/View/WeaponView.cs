using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : BaseView
{
    public Text weapon_id;
    public Text weapon_lv;
    public Text weapon_name;
    private ConfigWeaponRecord cf;
    private WeaponData weaponData;
    public override void Setup(ViewParam data)
    {
        WeaponViewParam param = data as WeaponViewParam;
        weaponData = DataAPIController.instance.GetWeaponDataById(param.weaponID);
        cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(param.weaponID);
        weapon_id.text = param.weaponID.ToString();
        weapon_lv.text = weaponData.level.ToString();
        weapon_name.text = cf.Name.ToString();
    }
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
    }
    private void OnWeaponDataChange(object data)
    {
        weaponData = data as WeaponData;
        weapon_lv.text = weaponData.level.ToString();
    }
    public void OnHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    public void UpgradeWP()
    {
        DataAPIController.instance.UpgradelevelWeaponDataById(weaponData.id);
    }
}
