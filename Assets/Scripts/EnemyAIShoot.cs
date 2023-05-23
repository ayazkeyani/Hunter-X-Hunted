using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIShoot : MonoBehaviour
{
    public EnemyAwareness enemyAwareness;
    public Transform playersTransform;
    public GameObject bullet;
    public float shootTime = 5f;
    public bool canShoot;
    public GameObject bulletSpawn;
    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        
    }

    // if isAggro
    // then get player transform
    // instance a bullet
    // move the bullet to the player transform
    // bullet has a MoveDirection() method that sends the bullet in a direction

    private void Update()
    {
        //Shoot(playersTransform.position - transform.position);
        if (enemyAwareness.isAggro && canShoot)
        {
            // direction = destination - origin
            //Shoot(playersTransform.position - transform.position);
        }
    }

    private void Shoot(Vector3 dir)
    {
        // instance a bullet
        // move the bullet to the player transform
        // bullet has a MoveDirection() method that sends the bullet in a direction
        GameObject thisBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        thisBullet.GetComponent<EnemyBullet>().dir = dir;
    }
}
