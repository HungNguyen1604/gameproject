using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public delegate void ChangeGunHandle(WeaponBehavior wp);
public class WeaponControl : BySingleton<WeaponControl>
{
    public CharacterControl characterControl;
    public List<WeaponBehavior> weapons;
    public List<int> id_wps;
    public Transform anchor_wp;
    private int index_wp = -1;
    private WeaponBehavior cur_wp;
    public UnityEvent<WeaponBehavior> OnChangeGun;

    public List<PositionFire> positionFires;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        InputManager.instance.OnFire.AddListener(OnFire);
        InputManager.instance.OnChangeGun.AddListener(ChangeGun);
        InputManager.instance.OnReload.AddListener(OnReload);
        foreach (int e in id_wps)
        {
            ConfigWeaponRecord cf_wp = ConfigManager.instance.configWeapon.GetRecordByKeySearch(e);

            GameObject go = Instantiate(Resources.Load("Weapons/" + cf_wp.Prefab, typeof(GameObject))) as GameObject;
            WeaponBehavior wp_behaviour = go.GetComponent<WeaponBehavior>();
            //1
            GunDataIngame data = new GunDataIngame();
            data.cf = cf_wp;
            go.transform.SetParent(anchor_wp, false);
            go.SetActive(false);
            // use LinQ
            PositionFire positionFire = positionFires.Where(x => x.gunType == wp_behaviour.gunType).FirstOrDefault();
            positionFire.characterControl = characterControl;
            data.positionFire = positionFire;
            wp_behaviour.Setup(data);
            //2
            // wp_behaviour.Setup(new GunDataIngame { name = e });
            weapons.Add(wp_behaviour);


        }
        ChangeGun();
    }
    public void OnFire(bool isFire)
    {
        cur_wp.OnFire(isFire);
    }
    private void OnReload()
    {
        cur_wp.Reload();
    }
    private void ChangeGun()
    {
        index_wp++;
        if (index_wp >= weapons.Count)
        {
            index_wp = 0;
        }
        WeaponBehavior wp = weapons[index_wp];

        if (cur_wp != null)
        {
            cur_wp.HideGun();
        }
        // cur_wp?.gameObject.SetActive(false);
        cur_wp = wp;
        if (OnChangeGun != null)
        {
            OnChangeGun.Invoke(cur_wp);
        }
        // OnChangeGun?.Invoke(cur_wp);


        cur_wp.ReadyGun();

    }
    // Update is called once per frame
    void Update()
    {


    }
    private void OnDestroy()
    {
        OnChangeGun.RemoveAllListeners();
    }
}
[Serializable]
public class PositionFire
{
    public GunType gunType;
    public Transform pos_fire;
    public Transform aim;
    public CharacterControl characterControl;
    public Vector3 GetPosFire(out Vector3 dir)
    {
        if (characterControl.cur_tran_enemy != null)
        {
            dir = characterControl.cur_tran_enemy.position - pos_fire.position;
            dir.Normalize();
        }
        else
        {
            dir = aim.position - pos_fire.position;
            dir.Normalize();
        }

        return pos_fire.position;
    }
}