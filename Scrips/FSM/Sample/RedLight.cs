using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RedLight : FSM_State
{
    [NonSerialized]
    public TrafficLight parent;
    public float timer;
    public override void OnEnter()
    {
        Debug.LogError(" Enter Red");
        timer = 0;
        parent.icon_light.color = Color.red;
    }
    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            parent.GotoState(parent.greenState);
        }
        parent.time_lb.text = ((int)timer).ToString();
    }
    public override void Exit()
    {
        Debug.LogError(" Exit Red");
    }
}
