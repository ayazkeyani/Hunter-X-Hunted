using UnityEngine;

public class AngleToPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    private float angle;
    public int lastIndex; // stores 0-7

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get target position and direction
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        // Get angle
        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        // Flip sprites if needed
        Vector3 tempScale = Vector3.one;
        if (angle < 0)
        {
            tempScale.x *= -1f;
        }

        spriteRenderer.transform.localScale = tempScale;

        // convert to index
        lastIndex = GetIndex(angle);
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
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
