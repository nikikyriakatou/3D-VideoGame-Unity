using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] FOV FovBox;
    private int health;

    public void ReactToHit()
    {
        health -= 1;
        StartCoroutine(GetHurt());
        if (health < 1)
        {
            WanderingAI behavior = GetComponent<WanderingAI>();
            if (behavior != null)
            {
                behavior.SetAlive(false);
            }
            StartCoroutine(Die());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        FovBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private IEnumerator GetHurt()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        objectRenderer.material.color = Color.white;
    }
}
