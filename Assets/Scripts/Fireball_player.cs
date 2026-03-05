using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Player : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 5;
    private Vector3 direction;

    public void SetDirection(Vector3 targetDirection)
    {
        direction = targetDirection.normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the fireball hit a regular enemy
        ReactiveTarget enemy = other.GetComponent<ReactiveTarget>();
        if (enemy != null)
        {
            Debug.Log("Fireball hit an enemy!");
            enemy.ReactToHit(); // Apply damage
        }

        // Check if the fireball hit a skull enemy
        ReactiveTarget_skull skullEnemy = other.GetComponent<ReactiveTarget_skull>();
        if (skullEnemy != null)
        {
            Debug.Log("Fireball hit a skull enemy!");
            skullEnemy.ReactToHit_skull(); // Apply damage
        }

        // Destroy fireball upon impact
        Destroy(this.gameObject);
    }
}
