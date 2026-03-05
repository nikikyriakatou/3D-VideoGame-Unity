using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 1.0f;

    private bool isAlive;
    public int damage = 5;
    private bool isTooClose = false;



    public bool IsTooClose // Property to access isTooClose
    {
        get { return isTooClose; } // Only provide a getter 
    }

    public void SetTooClose(bool value)
    {
        isTooClose = value; // Sets the value of isTooClose
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && (!isTooClose))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {            
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PointClickMovement>())
            {

                if (hitObject != null)
                {
                    Managers.Player.ChangeHealth(-damage);
                }
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
