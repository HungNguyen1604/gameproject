using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class YellowLight : FSM_State
{
    [NonSerialized]
    public TrafficLight parent;
    public float timer;
    public override void OnEnter()
    {
        Debug.LogError(" Enter Yellow");
        timer = 0;
        parent.icon_light.color = Color.yellow;
    }
    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            parent.GotoState(parent.redState);
        }
        parent.time_lb.text = ((int)timer).ToString();
    }
}
