using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Orge_AttackState : FSM_State
{
    [NonSerialized]
    public OrgeControl parent;
    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.LogError(" Enter attack state");
    }
    public override void UpdateState()
    {
        if (parent.time_count_attack >= parent.attack_speed)
        {
            parent.dataBinding.Attack = true;
            parent.time_count_attack = 0;
        }
    }
    public override void OnAnimationMiddle()
    {
        float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);
        Vector3 dir = parent.characterControl.trans.position - parent.trans.position;
        float dot = Vector3.Dot(dir.normalized, parent.trans.forward);
        if (dot > 0.5f && dis <= parent.range_Attack)
        {
            parent.characterControl.OnDamage(parent.damage);
        }
    }
    public override void OnAnimationExit()
    {
        parent.GotoState(parent.wanderState, "helo");
        //Debug.LogError(" OnAnimationExit");

    }
    public override void Exit()
    {
        base.Exit();
        Debug.LogError(" Exit attack state");
    }
}