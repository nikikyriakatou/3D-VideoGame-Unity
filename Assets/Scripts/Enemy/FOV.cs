using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public WanderingAI enemyAI;  // Reference to the WanderingAI script
    private Transform playerTransform;

    private float maxDistance = 5.0f;

    void Start()
    {
        // Find the player object in the scene by locating the PlayerCharacter component
        PointClickMovement player = FindObjectOfType<PointClickMovement>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PointClickMovement>() && playerTransform != null)
        {
            if (HasLineOfSight())
            {
                Debug.Log("Player entered FOV and is in line of sight");
                RotateTowardsPlayer();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PointClickMovement>() && playerTransform != null)
        {
            if (HasLineOfSight())
            {
                RotateTowardsPlayer();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PointClickMovement>())
        {
            Debug.Log("Player exited FOV");
            enemyAI.SetTooClose(false);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = playerTransform.position - enemyAI.transform.position;
        Ray ray = new Ray(enemyAI.transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit the player
            if (hit.collider.GetComponent<PointClickMovement>() != null)
            {
                if (hit.distance < maxDistance)
                {
                    enemyAI.SetTooClose(true);
                }
                else
                {
                    enemyAI.SetTooClose(false);
                }
                return true; // No obstacles between enemy and player
            }
        }

        return false; // Obstacle detected
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player and instantly rotate to face the player
        Vector3 direction = playerTransform.position - enemyAI.transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Set the enemy's rotation instantly to face the player
        enemyAI.transform.rotation = lookRotation;
    }
}