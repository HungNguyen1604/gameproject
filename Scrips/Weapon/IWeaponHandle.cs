using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponHandle
{
    void Init(WeaponBehavior wp);
    void FireHandle();
    void ReloadHandle();

}
