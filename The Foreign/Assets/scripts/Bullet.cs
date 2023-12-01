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
    float distance;
    int damage;
    BoxCollider2D colliderP;
    private string parentT;
    private Vector3 parentTr;
    // Start is called before the first frame update
    void Start()
    {
        colliderP = GetComponent<BoxCollider2D>();
        parentT = transform.parent.transform.tag + "";
        parentTr = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
        if (parentT == "enemy")
        {
            EnemyMovement parent = GetComponentInParent<EnemyMovement>();
            colliderP.isTrigger = true;
            damage = parent.damage;
        } else { 
            PlayerStats parent = GetComponentInParent<PlayerStats>();
            damage = parent.bulletDamage * parent.bulletDamage; 
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
        
        if (Vector3.Distance(transform.position, parentTr) > 1.2f) colliderP.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy" && parentT != "enemy")
        {
            EnemyMovement target = collision.transform.GetComponent<EnemyMovement>();
            target.hp -= damage;
        }
        else if (collision.transform.tag == "Player" && parentT != "Player")
        {
            PlayerStats target = collision.transform.GetComponent<PlayerStats>();
            PlayerMovement targetState = collision.transform.GetComponent<PlayerMovement>();
            if (!targetState.dashing)
            {
                target.HP -= damage;
            }
            
        }

        Destroy(gameObject);
    }
}
