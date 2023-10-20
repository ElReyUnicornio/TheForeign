using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int hp = 2;
    public int damage = 1;

    private bool walking = false;
    public float speed;
    public float walkingTime;
    public float range;
    public Transform player;
    float x = 0, y = 0;

    [Header("Shoot settings")]
    public float shootCooldown = 2.0f;
    float shootTimer = 0;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Vector2.Distance(transform.position, player.position) <= 5 && shootTimer > shootCooldown)
        {
            StartCoroutine(shoot());
            Debug.Log("shoot");
        }

        transform.Translate(new Vector3(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0));
        if (hp <= 0) Destroy(gameObject);
        if (walking) return;
        StartCoroutine(setDirection());
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.tag == "overload") {
            PlayerStats parent = col.transform.GetComponentInParent<PlayerStats>();
            hp -= parent.overloadDamage * parent.damage;
        }
    }

    //states
    IEnumerator setDirection()
    {
        walking = true;
        //set direction
        x = Random.Range(-1.0f, 1.0f);
        y = Random.Range(-1.0f, 1.0f);

        yield return new WaitForSeconds(walkingTime);
        x = 0; y = 0;
        yield return new WaitForSeconds(1);
        walking = false;
    }

    IEnumerator shoot()
    {
        var dir = player.transform.position - transform.position;
        var angleRads = Mathf.Atan2(dir.x, dir.y);
        var angle = angleRads * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        Instantiate(bullet, gameObject.transform);
        shootTimer = 0;
        yield return null;
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }
}
