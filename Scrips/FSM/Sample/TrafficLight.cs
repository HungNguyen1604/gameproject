using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLight : FSM_System
{
    public Image icon_light;
    public Text time_lb;
    public YellowLight yellowState;
    public GreenLight greenState;
    public RedLight redState;

    // Start is called before the first frame update
    private void Start()
    {
        yellowState.parent = this;
        greenState.parent = this;
        redState.parent = this;
        GotoState(greenState);
    }

}
