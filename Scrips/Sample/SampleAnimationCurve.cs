using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimationCurve : MonoBehaviour
{
    public AnimationCurve animationCurve;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        transform.DOMoveX(5, 3).SetEase(animationCurve);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
