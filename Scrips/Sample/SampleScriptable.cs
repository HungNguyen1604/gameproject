using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleScriptable", menuName = "BY/ScriptableObject", order = 1)]
public class SampleScriptable : ScriptableObject
{
    [SerializeField]
    private bool isStudent;
    public List<string> names;
    public int age;
    public List<Student> students;
}


