using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractClassSample : MonoBehaviour
{
    public abstract void ShowInfo();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public interface SampleInterface
{
    void ShowInfo_2();
}
