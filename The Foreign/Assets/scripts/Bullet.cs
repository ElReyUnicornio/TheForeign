using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4.0f;
    public float lifetime = 1.0f;
    float timer = 0;
    int damage;
    Collider2D colliderP;
    private string parentTag;
    // Start is called before the first frame update
    void Start()
    {
        colliderP = gameObject.GetComponent<Collider2D>();
        colliderP.isTrigger = true;
        parentTag = transform.parent.transform.tag;
        if (parentTag == "enemy")
        {
            EnemyMovement parent = GetComponentInParent<EnemyMovement>();
            damage = parent.damage;
        } else { 
            PlayerStats parent = GetComponentInParent<PlayerStats>();
            damage = parent.bulletDamage; 
        }

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
        if (Vector2.Distance(gameObject.transform.position, transform.parent.position) > 1.2f) colliderP.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy" && parentTag != "enemy")
        {
            EnemyMovement target = collision.transform.GetComponent<EnemyMovement>();
            target.hp -= damage;
        }
        else
        {
            PlayerStats target = collision.transform.GetComponent<PlayerStats>();
            target.HP -= damage;
        }
        Destroy(gameObject);
    }
}
