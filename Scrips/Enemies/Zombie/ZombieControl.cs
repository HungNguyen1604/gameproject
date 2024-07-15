using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class ZombieControl : EnemyControl
{
    public float speed_move = 2;

    public ZombieDataBinding dataBinding;
    public Zombie_AttackState attackState;
    public Zombie_ChaseState chaseState;
    public Zombie_WanderState wanderState;
    public ZombieStartState startState;
    public ZombieDeadState deadState;
    public float dot_attack = 0.5f;
    // Start is called before the first frame update

    public override void Setup(ConfigEnemyRecord configEnemy)
    {
        base.Setup(configEnemy);


        chaseState.parent = this;
        attackState.parent = this;
        wanderState.parent = this;
        startState.parent = this;
        deadState.parent = this;

        agent_.updateRotation = false;
        GotoState(startState);
        StartCoroutine("LoopCheckAttack");
    }
    public void RotateAgent()
    {
        Vector3 dir = agent_.steeringTarget - trans.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 120);
        }

    }
    IEnumerator LoopCheckAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Vector3 target_character = characterControl.trans.position;

            float dis = Vector3.Distance(trans.position, target_character);
            Vector3 dir = target_character - trans.position;
            dir.Normalize();
            float dot = Vector3.Dot(trans.forward, dir);



            if (dis <= range_Detect && dot > dot_attack)
            {
                if (current_state == wanderState || current_state == startState)
                    GotoState(chaseState);
                // chase 
                //time_count_attack >= attack_speed&&


            }

        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }



    /*
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 dir = characterControl.trans.position - trans.position;
        dir.Normalize();
        float dot = Vector3.Dot(trans.forward, dir);

        Vector3 target_character = characterControl.trans.position;

        float dis = Vector3.Distance(trans.position, target_character);

        if (dis <= range_Detect && dot>0.5f)
        {
           
            // chase 
            //time_count_attack >= attack_speed&&
          

            if(dis<=range_Attack&& time_count_attack >= attack_speed)
            {
                Debug.LogError("Attack by zombie");
                time_count_attack = 0;
                dataBinding.Speed = 0;
            }
            else
            {
                Vector3 dir_move = target_character - trans.position;
                Quaternion q = Quaternion.LookRotation(dir_move.normalized, Vector3.up);
                trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 3);
                // trans.position = Vector3.Lerp(trans.position, targetMove, Time.deltaTime * speed_move);
                trans.Translate(Vector3.forward * Time.deltaTime * speed_move * 2);
                dataBinding.Speed = 2;
               // dataBinding.NewPos = trans.position;
            }
        }
        else
        {
            float dis_move = Vector3.Distance(trans.position, targetMove);
            if (dis_move <= 0.1f)
            {
                RandomTarget();
            }
            else
            {
                Vector3 dir_move = targetMove - trans.position;
                Quaternion q = Quaternion.LookRotation(dir_move.normalized, Vector3.up);
                trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 2);
                // trans.position = Vector3.Lerp(trans.position, targetMove, Time.deltaTime * speed_move);
                trans.Translate(Vector3.forward * Time.deltaTime * speed_move);
                dataBinding.Speed = 1;
                // dataBinding.NewPos = trans.position;
            }
        }


        // move 

     
    }
    */
    public override void OnDamage(Bulletdata bulletdata)
    {
        if (hp <= 0)
            return;
        this.bulletdata = bulletdata;
        hp = hp - bulletdata.damage;

        if (hp <= 0)
        {
            hp = 0;
            if (current_state != deadState)
            {
                GotoState(deadState);
                Invoke("ImpactPhysic", 0.1f);
            }
        }
        base.OnDamage(bulletdata);
    }
    private void ImpactPhysic()
    {
        bulletdata.rig_body.AddForceAtPosition(bulletdata.force, bulletdata.point_impact, ForceMode.Impulse);
    }
}
