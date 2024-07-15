using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OrgeControl : EnemyControl
{
    public float armo = 1;
    public float speed_rotate = 5;
    public Orge_AttackState attackState;
    public Orge_ChaseState chaseState;
    public Orge_WanderState wanderState;

    public OrgeDataBinding dataBinding;

    // Start is called before the first frame update
    public override void Setup(ConfigEnemyRecord configEnemy)
    {
        base.Setup(configEnemy);
        Debug.LogError(" Ogre Init");

        //base.Setup();
        hp = hp * 2;
        attackState.parent = this;
        chaseState.parent = this;
        wanderState.parent = this;
        GotoState(wanderState);
        agent_.Warp(trans.position);
        agent_.updateRotation = false;
        //agent_.updatePosition = false;
    }


    /*
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 dir = characterControl.trans.position - trans.position;
       // dir.Normalize();
        //1.
        // trans.forward = Vector3.Lerp(trans.forward,dir,Time.deltaTime* speed_rotate);
        // trans.forward = Vector3.MoveTowards(trans.forward, dir, Time.deltaTime * speed_rotate);

        Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);

        trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * speed_rotate);
        
        float dis = Vector3.Distance(trans.position, characterControl.trans.position);

        if(dis<=range_Detect&&time_count_attack>=attack_speed)
        {
            Debug.LogError("Attack by orge");
            time_count_attack = 0;
        }
    }
    */

}
