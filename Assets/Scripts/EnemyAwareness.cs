using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 8f;
    public Material aggroMat;
    public bool isAggro;
    private Transform playersTransform;
    public float aggroDistance = 30f;

    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);
        //Debug.Log("dist to player: "+ dist);
        if (dist < awarenessRadius)
        {
            isAggro = true;
        }
        else if(dist > awarenessRadius + aggroDistance) 
        {
            isAggro = false;
        }

        if (isAggro)
        {
            //GetComponent<MeshRenderer>().material = aggroMat;
            // make sound
        }
    }

}
