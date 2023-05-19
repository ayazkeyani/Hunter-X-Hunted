using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("-Gun Settings-")]
    [Header("Range :")]
    public float range = 20f;
    public float verticalRange = 20f;
    public float gunShotRadius = 20f; // how far can enemies detect you when you fire
    private BoxCollider gunTrigger;
    [Header("Damage :")]
    public float bigDamage = 2f;
    public float smallDamage = 1f;
    [Header("Ammo :")]
    public int maxAmmo = 120;
    public int currentAmmo = 40;
    private int maxRounds = 3;
    [SerializeField]
    private int currentRounds;

    [Header("References")]
    public LayerMask enemyLayerMask; // enemy
    public LayerMask raycastLayerMask; // default, enemy
    public EnemyManager enemyManager;

    // animation
    [Header("-Animation Settings-")]
    public Animator gunAnim;
    int animLayer = 0;
    private float rifleFireTime; // stores clip length
    private float rifleReloadTime; // stores clip length
    private void Start()
    {
        // get the component and update the clip lengths
        gunAnim = GetComponentInChildren<Animator>();
        UpdateAnimClipTimes();

        enemyManager = FindObjectOfType<EnemyManager>();
        gunTrigger = GetComponent<BoxCollider>();
        // Set the size/range of the boxCollider
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPlaying(gunAnim, "RifleFire") && !isPlaying(gunAnim, "RifleReload")) // check for input / check for animations
        {
            Reload();
        }

        if (Input.GetMouseButtonDown(0) && !isPlaying(gunAnim, "RifleReload") && !isPlaying(gunAnim, "RifleFire")) // Check for input / check for animations
        {
            if (currentRounds > 0)
            {
                // can fire
                currentRounds -= 1;
                Fire();
                StartCoroutine(WaitForAnimation(gunAnim, "RifleFire", rifleFireTime));
                Debug.Log("Fire...");
            }
        }
    }

    // ANIMATION COROUTINE
    IEnumerator WaitForAnimation(Animator anim, string stateName, float waitTime)
    {
        anim.Play(stateName);
        yield return new WaitForSeconds(waitTime);
        anim.Play("RifleIdle");
    }

    // GET CLIP LENGTH
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = gunAnim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name) 
            {
                case "RifleFire":
                    rifleFireTime = clip.length;
                    break;
                case "RifleReload":
                    rifleReloadTime = clip.length;
                    break;
            }
        }
    }

    // RETURN IF STATENAME IS CURRENTSTATE
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    // SHOOTING CODE

    public void Reload()
    {
        if (currentAmmo > 0)
        {
            if (currentRounds < maxRounds)
            {
                currentAmmo -= 1;
                currentRounds += 1;
                StartCoroutine(WaitForAnimation(gunAnim, "RifleReload", rifleReloadTime));
                Debug.Log("Reloading...");
            }
        }
    }

    void Fire()
    {
        // simulate gun shot radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        // alert any enemy in earshot
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        // play test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        // damage enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            // raycast check if something is blocking the enemy
            // get direction to enemy
            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    // range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if (dist > range * 0.5)
                    {
                        // damage the enemy for half of the total damage
                        // damage enemy
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        // damage the enemy for the full amount
                        // damage enemy
                        enemy.TakeDamage(bigDamage);
                    }
                    // Check that the object is in sight and not behind cover
                    //Debug.DrawRay(transform.position, dir, Color.green);
                    //Debug.Break();
                }
            }
        }

        // reset timer
    }

    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            // add here
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // remove potential enemy when it dies
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            // remove here
            enemyManager.RemoveEnemy(enemy);
        }
    }
}
