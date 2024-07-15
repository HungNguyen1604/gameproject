using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public Collider collider_;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        //  Debug.LogError("OnBecameInvisible");
        collider_.enabled = false;
    }

    private void OnBecameVisible()
    {
        // Debug.LogError("OnBecameVisible");
        collider_.enabled = true;
    }


}
