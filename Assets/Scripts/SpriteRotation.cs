using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
