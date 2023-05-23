using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 10f;
    public int damageAmount;
    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            // damage
            other.GetComponent<PlayerHealth>().DamagePlayer(damageAmount);
            Destroy(gameObject);
        }
        else if(!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
