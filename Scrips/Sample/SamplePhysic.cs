using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SamplePhysic : MonoBehaviour
{
    private Transform trans;
    private float time_cout = 0;
    public LayerMask mask_hit;
    private void Awake()
    {
        trans = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        int d;
        float dev = 0;
        int sum = Add(1, 2, out d, ref dev);
    }

    // Update is called once per frame
    void Update()
    {
        time_cout += Time.deltaTime;
        if (time_cout > 0.1f)
        {
            time_cout = 0;
            trans.Rotate(Vector3.up, 5);
        }
        RaycastHit hitInfo;
        // Ray r = new Ray { origin = trans.position, direction = trans.forward };
        Ray r = new Ray();
        r.origin = trans.position;
        r.direction = trans.forward;
        if (Physics.Raycast(r, out hitInfo, 10, 1 << 7))
        {
            Debug.LogError(" hit");
            Debug.DrawLine(trans.position, hitInfo.point);
        }
        RaycastHit hitInfo2_;
        if (Physics.Raycast(trans.position, trans.forward, out hitInfo2_, 10, mask_hit))
        {
            //Debug.LogError(" hit 2");
        }
    }
    private int Add(int a, int b, out int dob, ref float dev)
    {
        dob = (a + b) * 2;
        //dev = (a + b) / 2;
        return a + b;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.LogError(" detect :" + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.LogError(" OnCollisionExit :" + collision.gameObject.name);

    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.LogError(" OnCollisionStay :" + collision.gameObject.name);
    }
    public void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(" OnTriggerEnter :" + other.gameObject.name);
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.LogError(" OnTriggerExit :" + other.gameObject.name);

    }
}
