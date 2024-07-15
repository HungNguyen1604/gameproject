using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SampleHandle();
public delegate void ShowLogHandle(string mess);
public delegate void ShowLogAndUpperHandle(string mess);
public delegate int AddHanle(int a, int b);
public class DelegateSample : MonoBehaviour
{
    public AddHanle addHanle;
    public Func<int, int, int> Add_Func;
    // public EnemyControl;
    // function
    // Start is called before the first frame update
    void Start()
    {
        string s = " Action and lamda expression";
        ShowInfo((s) =>
        {

        }, s);
        // ShowInfo((m) => Debug.LogError("ex :" + m), "hello lamda");
        // ShowInfoUpper(ShowLogUpperHandleSample, "Hello World");

        SampleLamda(SampleLamdaHandle);
        SampleLamda(() =>
        {
            Debug.LogError("Sample lamda 2");
            Debug.LogError("\nSample lamda 3");
        });
        //addHanle = AddHandleSample;
        addHanle = (x, y) =>
        {
            return x + y;
        };
        Add_Func = AddHandleSample;
        // Predicate<string> isUpper = delegate (string s) { return s.Equals(s.ToUpper()); };
        Predicate<string> isUpper = (s) =>
        {
            return s.Equals(s.ToUpper());
        };
        //
        Student A = new Student() { name_student = "asd", code = 123 };
        Student B = new Student() { name_student = "qwe", code = 12 };
        Debug.LogError(" Compare:" + A.CompareTo(B));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int res = addHanle(5, 2);
            Debug.LogError(" res :" + res);
        }
    }
    private void SampleLamda(SampleHandle sp)
    {
        sp();
    }
    private void SampleLamdaHandle()
    {
        Debug.LogError(" Sample lamda 1");
    }
    private void ShowInfo(Action<string> showLogHandle, string mess)
    {
        showLogHandle(mess);
    }
    private void ShowInfoUpper(ShowLogAndUpperHandle showLogHandle, string mess)
    {
        showLogHandle(mess);
    }
    private void ShowLogHandleSample(string mess)
    {
        Debug.LogError(" log :" + mess);
    }
    private void ShowLogUpperHandleSample(string mess)
    {
        string s = mess.ToUpper();
        Debug.LogError(" log :" + s);
    }
    private int AddHandleSample(int a, int b)
    {
        int res = a + b;
        return res;
    }
}
