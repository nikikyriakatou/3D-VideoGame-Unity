using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public AudioSource audioSource;
    public AudioClip jumpSound;

    private bool isInvulnerable = false; // Track invulnerability




    public void Startup()
    {
        Debug.Log("Player manager starting...");
        UpdateData(50, 100);
        status = ManagerStatus.Started;
    }


private Transform FindChildRecursive(Transform parent, string childName)
{
    foreach (Transform child in parent)
    {
        if (child.name == childName)
            return child;

        Transform found = FindChildRecursive(child, childName);
        if (found != null)
            return found;
    }
    return null;
}


    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    public void ChangeHealth(int value)
    {
        if (!isInvulnerable) // Only take damage if not invulnerable
        {
            health += value;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
            }

            if (health == 0)
            {
                Messenger.Broadcast(GameEvent.LEVEL_FAILED);
            }

            Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
        }
    }

    void Update()
    {
        // Debug message to check if Update is running
        Debug.Log("Update is running...");

        // Check if the player presses "Q" to activate invulnerability
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q key pressed! Attempting to activate invulnerability...");

            if (!isInvulnerable)
            {
                StartCoroutine(ActivateInvulnerability());
            }
        }
    }

    private IEnumerator ActivateInvulnerability()
    {
        isInvulnerable = true;
        Debug.Log("Invulnerability activated!");


        yield return new WaitForSeconds(5); // Wait for 5 seconds

        isInvulnerable = false; // Disable invulnerability
        Debug.Log("Invulnerability ended!");

    }

    public void Jump()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        Debug.Log("Player Jumped!");
    }

    public void Respawn()
    {
        UpdateData(50, 100);
    }
}
