using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask mask_ground;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit, 100, mask_ground))
            {
                agent.destination = hit.point;
                //Debug.LogError("hit : " + hit.point);
            }
        }
    }
}
