using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Orge_WanderState : FSM_State
{
    [NonSerialized]
    public OrgeControl parent;
    private Transform trans_point;
    public float speed_move = 1;

    public override void OnEnter()
    {
        parent.agent_.isStopped = false;
        base.OnEnter();
        trans_point = SceneConfig.instance.GetPointMove();
        Debug.LogError("OnEnter wander");
        parent.agent_.speed = 1;
    }
    public override void OnEnter(object data)
    {
        parent.agent_.isStopped = false;
        parent.time_delay_agent = 0;
        string mess = (string)data;
        Debug.LogError(" On wander with mess :" + mess);
        parent.agent_.speed = 1;
    }
    public override void UpdateState()
    {
        parent.time_delay_agent += Time.deltaTime;
        float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);
        if (dis <= parent.range_Detect)
        {
            parent.GotoState(parent.chaseState);
        }
        else
        {
            if (parent.isMoveAgent)
            {
                parent.agent_.SetDestination(trans_point.position);
                if (parent.agent_.remainingDistance <= 0.1f && parent.time_delay_agent > 0.3f)
                {
                    parent.time_delay_agent = 0;
                    trans_point = SceneConfig.instance.GetPointMove();
                }
                parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
                RotateAgent();
            }
            else
            {
                Vector3 dir = trans_point.position - parent.trans.position;
                dir.Normalize();
                if (dir != Vector3.zero)
                {
                    Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
                    parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 2);
                }
                parent.trans.Translate(Vector3.forward * Time.deltaTime * speed_move);
                parent.dataBinding.Speed = 1;
                if (Vector3.Distance(parent.trans.position, trans_point.position) <= 0.1f)
                {
                    trans_point = SceneConfig.instance.GetPointMove();
                }
            }
        }
    }
    private void RotateAgent()
    {
        Vector3 dir = parent.agent_.steeringTarget - parent.trans.position;
        dir.Normalize();
        if (dir.magnitude > 0)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 20);
        }
    }

    public override void Exit()
    {
        parent.agent_.isStopped = true;
        Debug.LogError("OnWander exit");
    }
}
