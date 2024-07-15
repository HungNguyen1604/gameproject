using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GreenLight : FSM_State
{
    [NonSerialized]
    public TrafficLight parent;
    public float timer;
    public override void OnEnter()
    {
        Debug.LogError(" Enter Green");
        timer = 0;
        parent.icon_light.color = Color.green;
    }
    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= 15)
        {
            parent.GotoState(parent.yellowState);
        }
        parent.time_lb.text = ((int)timer).ToString();
    }
}
