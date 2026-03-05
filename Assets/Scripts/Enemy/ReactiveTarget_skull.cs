using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ReactiveTarget_skull : MonoBehaviour
{

    private int health;

    public void ReactToHit_skull()
    {
        health -= 1;
        StartCoroutine(GetHurt_skull());
        if (health < 1)
        {
            skullAI behavior = GetComponent<skullAI>();
            if (behavior != null)
            {
                behavior.SetAlive(false);
            }
            StartCoroutine(Die_skull());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    private IEnumerator Die_skull()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private IEnumerator GetHurt_skull()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        objectRenderer.material.color = Color.white;
    }
}
