using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Required for DOTween

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private bool requireKey;
    [SerializeField] private float openDistance = 3f; // Distance the door moves
    [SerializeField] private float duration = 1f; // Time for door to open
    [SerializeField] private AudioClip doorOpenSound; // Add this

    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private AudioSource audioSource; // Add this
    private bool isDoorOpen = false; // Track if the door is open

    void Start()
    {
        // Store the original positions of objects
        foreach (GameObject target in targets)
        {
            originalPositions[target] = target.transform.position;
        }

        audioSource = GetComponent<AudioSource>(); // Get AudioSource
    }

    void OnTriggerEnter(Collider other)
    {
        if (requireKey && Managers.Inventory.equippedItem != "key")
        {
            return;
        }

        // Only play the sound if the door is currently closed
        if (!isDoorOpen)
        {
            if (audioSource != null && doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }
            isDoorOpen = true; // Mark the door as open
        }

        foreach (GameObject target in targets)
        {
            Vector3 openPosition = target.transform.position + new Vector3(0, openDistance, 0); // Move up
            target.transform.DOMove(openPosition, duration).SetEase(Ease.OutQuad); // Smooth movement
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.transform.DOMove(originalPositions[target], duration).SetEase(Ease.InQuad); // Smooth closing
        }

        isDoorOpen = false; // Mark the door as closed
    }
}
