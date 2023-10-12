using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4.0f;
    public float lifetime = 1.0f;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
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
}
