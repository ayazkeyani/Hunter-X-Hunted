using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("-Player Stats Settings-")]
    [Header("Health :")]
    public int maxHealth = 100;
    [SerializeField]
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            DamagePlayer(30);
            Debug.Log("Player damaged");
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player Died");

            // On death restart the scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    // CALLED FROM PICKUP SCRIPT

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
