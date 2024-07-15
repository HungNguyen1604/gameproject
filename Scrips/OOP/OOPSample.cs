using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOPSample : MonoBehaviour
{
    public EnemyControl[] enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                //enemies[i].OnDamage(5);
            }
        }
    }
}
public class CuopGiat
{
    public int tuoi;
    public bool gioiTinh;

    public void GiutDayChuyen()
    {
        Debug.LogError(" Giut day chuyen cua nguoi phu nu di duong");
    }
}
[Serializable]
public class Enemy
{
    public int hp;
    public int damage;

    public void ShowInfo()
    {
        Debug.LogError(" HP :" + hp);
    }
    public void Attack()
    {

    }

    public void Move()
    {

    }
}
public class Orge : Enemy
{

    public Orge() { }
    public Orge(int hp_, int damage_)
    {
        this.hp = hp_;
    }

}


public class Zombie : Enemy
{

    public Zombie() { }
    public Zombie(int hp_, int damage_)
    {
        this.hp = hp_;
    }

}
