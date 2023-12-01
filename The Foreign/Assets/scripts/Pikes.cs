using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikes : MonoBehaviour
{
    public int damage = 2;
    public int time = 2;
    private Collider2D pikeCollider;
    private SpriteRenderer srPike;
    private bool working = false;
    // Start is called before the first frame update
    void Start()
    {
        pikeCollider = GetComponent<Collider2D>();
        srPike = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (working) return;
        StartCoroutine(toggle());
    }

    IEnumerator toggle()
    {
        working = true;
        srPike.color = new Color(125.0f / 255.0f, 144.0f / 255.0f,144.0f / 255.0f, 1.0f);
        pikeCollider.enabled = !pikeCollider.enabled;
        yield return new WaitForSeconds(time);
        srPike.color = new Color(225f / 255.0f, 145.0f / 255.0f, 34.0f / 255.0f, 1.0f);
        pikeCollider.enabled = !pikeCollider.enabled;
        yield return new WaitForSeconds(time);
        working = false;
    }
}
