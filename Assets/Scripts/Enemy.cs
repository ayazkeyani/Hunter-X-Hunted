using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    private Animator spriteAnim;
    private AngleToPlayer angleToPlayer;

    private float enemyHealth = 2f;

    public GameObject gunHitEffect;
    public GameObject bodyPickup;
    public ParticleSystem bloodParticles;
    //public VisualEffect blood;
    // Start is called before the first frame update
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>(); // we can do this bc theres only (1) in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // beginning of update set the animations rotational index
        spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);

        if (enemyHealth <= 0)
        {
            // dead
            // remove from list
            enemyManager.RemoveEnemy(this);
            // instantiate the pickup
            //Vector3 bodyPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            // Store the VFX on the pickup to keep blood trails
            Instantiate(bodyPickup, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        // any animations we call will have the correct index
    }

    public void TakeDamage(float damage)
    {
        if (gunHitEffect != null)
        {
            Instantiate(gunHitEffect, transform.position, Quaternion.identity);
            bloodParticles.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("There is no 'gunHitEffect' reference");
        }

        enemyHealth -= damage;
    }
}
