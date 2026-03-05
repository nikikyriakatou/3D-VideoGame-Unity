using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 5;
    
    [SerializeField] private AudioClip fireballHitSound; // Assign this in Unity

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        if (player != null)
        {
            Managers.Player.ChangeHealth(-damage);

            // Play fireball hit sound  at the hit position
            if (fireballHitSound != null)
            {
                AudioSource.PlayClipAtPoint(fireballHitSound, transform.position);
            }
        }

        Destroy(this.gameObject); // Destroy fireball after hit
    }
}
