using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAngleToPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    //temporary

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float angle;
    public int lastIndex;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        // Get target position and Direction
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        // Get Angle
        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        lastIndex = GetIndex(angle);

        spriteRenderer.sprite = sprites[lastIndex];
    }

    private int GetIndex(float angle)
    {
        //front
        if (angle > -22.5f && angle < 22.6f)
            return 0; // forward
        if (angle >= 22.5f && angle < 67.5f)
            return 7; // angled forward
        if (angle >= 67.5f && angle < 112.5f)
            return 6; // side
        if (angle >= 112.5f && angle < 157.5f)
            return 5; // angle backwards

        //back
        if (angle <= -157.5 || angle >= 157.5)
            return 4; // backwards
        if (angle >= -157.4f && angle < -112.5f)
            return 3;
        if (angle >= -112.5f && angle < -67.5f)
            return 2;
        if (angle >= -67.5f && angle <= -22.5f)
            return 1;

        return lastIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
