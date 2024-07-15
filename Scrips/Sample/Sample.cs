using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    // Start is called before the first frame update
    public int age = 10;
    private float weight;
    public int dayOfYear;
    public bool isShow;
    public GameObject[] array;
    public string[] names = { "a", "d" };
    private void Awake()
    {
        Debug.LogError(" Awake");
        Info<string> info_1 = new Info<string>();
        info_1.a = "Hello";
        info_1.b = "World";
        info_1.ShowLog();

        Info<int> info_2 = new Info<int>();
        info_2.a = 1;
        info_2.b = 2;
        info_2.ShowLog();

        Info<float> info_3 = new Info<float> { a = 2.1f, b = 3.6f };
        info_3.ShowLog();

        LogInfo<Car> log = new LogInfo<Car>();
        LogInfo<Moto> log_2 = new LogInfo<Moto>();

    }

    private void OnEnable()
    {
        Debug.LogError(" OnEnable");

    }
    void Start()
    {
        Debug.LogError(" Start");

        int i = 5;
        int j = 5;
        int a = i + j;
        Debug.Log(a);
        //call function
        ShowInfo();
        if (a >= 20)
        {
            ShowLog("Hello k17");
        }
        else if (a > 10)
        {

        }
        else if (a >= 0)
        {

        }
        else
        {

        }

        for (i = 0; i < 10; i++)
        {
            Debug.LogError(" i :" + i);
        }
    }

    private void Reset()
    {
        Debug.LogError(" reset");
    }
    private void ShowInfo()
    {
        Debug.LogError(" Age: " + age);
    }
    private void ShowLog(string mess)
    {
        Debug.LogError(mess);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.LogError(" Update");

    }
    private void LateUpdate()
    {
        Debug.LogError(" LateUpdate");

    }
    private void FixedUpdate()
    {
        Debug.LogError(" FixedUpdate");

    }
    private void OnDisable()
    {
        Debug.LogError(" OnDisable");

    }
    private void OnDestroy()
    {
        Debug.LogError(" OnDestroy");

    }

    private void OnApplicationQuit()
    {
        Debug.LogError(" OnApplicationQuit");
    }
}
/*
 * static void Main(string[] args)
{
    int i = 5,j = 5;
    Console.WriteLine(i++); // Su dung gia tri i roi moi tang i
    Console.WriteLine(j++); // Tang j len roi moi in gia tri j ra man hinh

    Console.ReadKey();

}**/
public class Info<T>//Type
{
    public T a;
    public T b;
    public void ShowLog()
    {
        string res = a.ToString() + "-" + b.ToString();
        Debug.Log("result :" + res);
    }
}

public class LogInfo<T> where T : Vehicle, new()
{

}

public class Vehicle
{

}

public class Car : Vehicle
{
    public Car()
    {

    }
}
public class Moto : Vehicle
{

}