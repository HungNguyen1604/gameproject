using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MachineGunWeapon : WeaponBehavior
{
    public override void Init()
    {
        iWeaponHandle = new IMachineGun();
        iWeaponHandle.Init(this);
    }
    public override void Setup(GunDataIngame data)
    {
        base.Setup(data);
    }
    public void StartReload()
    {
        StopCoroutine("ReloadProgress");
        StartCoroutine("ReloadProgress");
    }
    IEnumerator ReloadProgress()
    {
        audioSource_.PlayOneShot(sfx_reload);
        isReloading = true;
        OnReload?.Invoke(reload_time);
        characterDataBinding.PlayReloadGun();
        yield return new WaitForSeconds(reload_time);
        isReloading = false;
        number_bullet = clip_size;
        OnBulletChange?.Invoke(number_bullet);
    }
}
public class IMachineGun : IWeaponHandle
{
    MachineGunWeapon wp;
    public void FireHandle()
    {
        CreateBullet();
        wp.characterDataBinding.PlayFireGun();
        //throw new System.NotImplementedException();
        if (wp.number_bullet <= 0)
        {
            ReloadHandle();
        }
    }
    private void CreateBullet()
    {
        wp.accuracy += wp.drop_accuracy;
        wp.accuracy = Mathf.Clamp(wp.accuracy, wp.min_accuracy, wp.max_accuracy);

        wp.shellControl.Fire();
        wp.audioSource_.PlayOneShot(wp.sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
        //AudioClip sfx = sfx_fires[UnityEngine.Random.Range(0, sfx_fires.Length)];
        //audioSource_.PlayOneShot(sfx);
        //StartCoroutine("SFXProgress");

        Transform bl = BYPoolManager.instance.dic_pool[wp.name_bullet_pool].Spawned();

        bl.position = wp.gunDataIngame.positionFire.GetPosFire(out Vector3 dir);
        float accuracy_val = wp.accuracy * 0.08f;
        float x = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
        float y = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
        Quaternion q = Quaternion.Euler(x, y, 0);
        bl.forward = q * dir;

        BulletControl bl_control = bl.GetComponent<BulletControl>();
        bl_control.Setup(new Bulletdata { damage = 2, name_pool = wp.name_bullet_pool });
    }
    public void Init(WeaponBehavior wp)
    {
        this.wp = (MachineGunWeapon)wp;
        //throw new System.NotImplementedException();
    }

    public void ReloadHandle()
    {
        //throw new System.NotImplementedException();
        wp.StartReload();
    }
}