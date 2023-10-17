using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 4.0f;
    public float lifetime = 1.0f;
    float timer = 0;

    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.transform.tag == "enemy" || collision.transform.tag == "hookable")
        {
            player.hookTarget = collision.transform;
            player.hooked = true;
            Destroy(gameObject);
        }
    }
}
