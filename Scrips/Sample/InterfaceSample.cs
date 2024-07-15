using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceSample
{
    void OnFire(float rof);
}
public class Student : IComparable
{
    public string name_student;
    public int code;
    public int CompareTo(object o_other)
    {
        if (o_other == null)
            return 1;
        Student student_b = o_other as Student;
        if (this.name_student.Length > student_b.name_student.Length)
        {
            return 1;
        }
        else if (this.name_student.Length < student_b.name_student.Length)
        {
            return -1;
        }
        else
        {
            if
                (this.code > student_b.code)
            {
                return 1;
            }
            else if (this.code < student_b.code)
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
public class Student_2
{
    public string name_student;
    public int code;
}
public class SortStudent : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        return x.CompareTo(y);
    }
}
public class SortStudent_2 : IComparer<Student_2>
{
    public int Compare(Student_2 A, Student_2 B)
    {
        if (A.name_student.Length > B.name_student.Length)
        {
            return 1;
        }
        else if (A.name_student.Length < B.name_student.Length)
        {
            return -1;
        }
        else
        {
            if
                (A.code > B.code)
            {
                return 1;
            }
            else if (A.code < B.code)
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
