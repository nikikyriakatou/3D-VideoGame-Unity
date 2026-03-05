using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private AudioClip collectSound; // Assign this in Unity

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character")) // Ensure only the player can collect
        {
            Managers.Inventory.AddItem(itemName);
            
            // Play the sound globally on the Player or AudioManager
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject); // Destroy immediately
        }
    }
}
