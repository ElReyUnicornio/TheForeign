using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool walking = false;
    public float speed;
    public float walkingTime;
    float x = 0, y = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0));
        if (walking) return;
        StartCoroutine(setDirection());
    }

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
}
