using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneConfig : BySingleton<SceneConfig>
{
    public Transform player_pos;
    [SerializeField]
    private List<Transform> point_Spawns;

    [SerializeField]
    private List<Transform> point_ogres;

    //public Transform GetPointMove()
    //{
    //    int index = UnityEngine.Random.Range(0, point_ogres.Count);

    //    return point_ogres[index];
    //}
    public Transform GetPointSpawn(out int index)
    {
        index = UnityEngine.Random.Range(0, point_Spawns.Count);
        return point_Spawns[index];

    }
    public Transform GetPointMove()
    {
        return point_ogres.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
