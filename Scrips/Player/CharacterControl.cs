using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public Transform trans;
    public float speed_move;
    [NonSerialized]
    public CharacterDataBinding dataBinding;
    public bool isAim = false;
    public CharacterController characterControl_;
    public int HP = 100;
    private int max_hp;
    public UnityEvent<int, int> OnHPChange;
    private float range_detect;
    public Transform cur_tran_enemy;
    public LayerMask maske_enemy;

    private void Awake()
    {
        trans = transform;
        dataBinding = gameObject.GetComponent<CharacterDataBinding>();
    }
    // Start is called before the first frame update
    void Start()
    {

        InputManager.instance.OnFire.AddListener(OnFire);
        WeaponControl.instance.OnChangeGun.AddListener(OnWeaponChange);
        max_hp = HP;
        OnHPChange?.Invoke(HP, HP);
    }
    private void OnWeaponChange(WeaponBehavior wp)
    {
        range_detect = wp.range;
    }
    public void OnFire(bool isFire)
    {
        isAim = isFire;
    }
    public void OnDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            // 
            HP = 0;
            Debug.LogError(" mission fail");
        }

        OnHPChange?.Invoke(HP, max_hp);

    }
    private void FixedUpdate()
    {
        if (isAim)
        {
            SearchTarget();
        }
        else
        {
            cur_tran_enemy = null;
        }
    }
    private void SearchTarget()
    {

        Collider[] cols = Physics.OverlapSphere(trans.position, range_detect, maske_enemy);
        List<EnemyDetectData> ls = new List<EnemyDetectData>();
        foreach (Collider col in cols)
        {
            Vector3 pos = trans.position;
            pos.y = col.transform.position.y;
            EnemyControl enemyControl = col.GetComponentInParent<EnemyControl>();
            if (enemyControl.isAlive)
            {
                if (Physics.Linecast(pos, col.transform.position, maske_enemy))
                {
                    ls.Add(new EnemyDetectData { trans = col.transform });

                }
            }
        }
        if (ls.Count >= 1)
        {
            ConpareEnemy compareEnemy = new ConpareEnemy();
            compareEnemy.trans_player = trans;
            ls.Sort(compareEnemy);
        }


        if (ls.Count > 0)
            cur_tran_enemy = ls[0].trans;
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    isAim = true;
        //}
        //else
        //{
        //    isAim = false;
        //}

        float speed_mul_aim = speed_move > 2 ? 2 : 1;

        Vector3 delta_move = InputManager.move_Dir;
        if (!isAim)// isAim==false , isAim==true
        {
            if (delta_move.magnitude > 0)//>,>=,==,!=
            {
                trans.forward = delta_move;
                // trans.position = trans.position + delta_move * Time.deltaTime * speed_move;

            }


            float speed_anim = delta_move.magnitude;

            //if(speed_move>2)
            //{
            //    speed_mul_aim = 2;
            //}
            //else
            //{
            //    speed_mul_aim = 1;
            //}
            dataBinding.MoveDir = new Vector3(0, 0, speed_anim * speed_mul_aim);
        }
        else
        {

            // quay vector 
            //  Vector3 move_dir_anim = trans.rotation * delta_move;
            Vector3 dir = trans.forward;
            // move 8 dir
            if (cur_tran_enemy != null)
            {
                Vector3 pos = cur_tran_enemy.position;
                pos.y = trans.position.y;
                dir = pos - trans.position;
                trans.forward = dir.normalized;
            }
            Vector3 move_dir_anim = trans.InverseTransformDirection(delta_move);

            dataBinding.MoveDir = move_dir_anim * speed_mul_aim;



        }

        // trans.position = trans.position + delta_move * Time.deltaTime * speed_move;

        if (!characterControl_.isGrounded)
        {
            delta_move.y = -1;
        }
        characterControl_.Move(delta_move * Time.deltaTime * speed_move);

    }

}
public class EnemyDetectData
{
    public Transform trans;
}
public class ConpareEnemy : IComparer<EnemyDetectData>
{

    public Transform trans_player;



    public int Compare(EnemyDetectData x, EnemyDetectData y)
    {
        float dis_x = Vector3.Distance(x.trans.position, trans_player.position);
        float dis_y = Vector3.Distance(y.trans.position, trans_player.position);
        if (dis_x > dis_y)
        {
            return 1;
        }
        else if (dis_x < dis_y)
        {
            return -1;
        }
        else
        {
            Vector3 pos_x = x.trans.position;
            pos_x.y = trans_player.position.y;
            Vector3 dir_x = pos_x - trans_player.position;
            float dot_x = Vector3.Dot(dir_x.normalized, trans_player.forward);
            Vector3 pos_y = y.trans.position;
            pos_y.y = trans_player.position.y;
            Vector3 dir_y = pos_y - trans_player.position;
            float dot_y = Vector3.Dot(dir_y.normalized, trans_player.forward);

            if (dot_x < dot_y)
            {
                return 1;
            }
            else if (dot_x > dot_y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

    }
}