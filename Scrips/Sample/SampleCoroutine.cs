using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCoroutine : MonoBehaviour
{
    private int count = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine("Demo");
        // (Demo());
        // yield return new WaitUntil(() => count > 3);
        yield return new WaitUntil(Check);

        Debug.LogError("count :" + count);
    }
    private bool Check()
    {
        return count > 3;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopCoroutine("Demo");
        }
    }
    IEnumerator Demo()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        yield return wait;
        Debug.LogError(" wait 1");
        yield return new WaitForSeconds(5);
        Debug.LogError(" wait 3");
        while (true)
        {
            yield return wait;
            count++;
        }
    }
}
