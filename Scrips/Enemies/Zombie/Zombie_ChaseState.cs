using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_ChaseState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public float speed_move = 2;
    private float time_Delay;
    public override void OnEnter()
    {
        base.OnEnter();

        time_Delay = 0;
        parent.enabled = true;
        parent.agent_.speed = 2;
        parent.agent_.Warp(parent.trans.position);
        parent.agent_.isStopped = false;
        parent.agent_.stoppingDistance = parent.range_Attack - 0.1f;
    }
    public override void UpdateState()
    {
        time_Delay += Time.deltaTime;
        Vector3 cur_point = parent.characterControl.trans.position;

        parent.agent_.SetDestination(cur_point);

        float dis = Vector3.Distance(parent.trans.position, cur_point);
        if (parent.agent_.remainingDistance <= parent.range_Attack && time_Delay > 0.2f)
        {
            parent.GotoState(parent.attackState);
        }
        else
        {
            parent.RotateAgent();
            parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
        }
        /*
               float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);

               if (dis <= parent.range_Attack)
               {
                   parent.GotoState(parent.attackState);
               }

               else
               {
                   Vector3 dir = parent.characterControl.trans.position - parent.trans.position;
                   dir.Normalize();
                   if (dir != Vector3.zero)
                   {
                       Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
                       parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 2);

                   }
                   parent.trans.Translate(Vector3.forward * Time.deltaTime * speed_move);
                   parent.dataBinding.Speed = 2;

               }
        */
    }
    public override void Exit()
    {
        base.Exit();
        parent.agent_.isStopped = true;
        parent.dataBinding.Speed = 0;
        parent.agent_.stoppingDistance = 0;
    }
}

