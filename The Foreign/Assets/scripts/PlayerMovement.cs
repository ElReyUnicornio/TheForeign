using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player settings")]
    Rigidbody2D rbPlayer;
    public GameObject directionIndicator;
    public float speed = 640.0f;

    [Header("Dash settings")]
    public float dashDistance = 2.0f;
    public float dashCooldown = 2.0f;
    float dashTimer = 0;
    bool dashing = false;

    [Header("Shoot settings")]
    public float shootCooldown = 2.0f;
    float shootTimer = 0;
    public GameObject bullet;

    [Header("Overload settings")]
    private bool overloading = false;
    private Transform currentOverload;
    private float overloadTimer;
    public float overloadDuration;
    public float overloadCooldown;
    public float overloadSpeed;
    public GameObject overload;

    [Header("Hook settings")]
    private float hookTimer;
    public Transform hookTarget;
    public float hookDuration;
    public float hookCooldown;
    
    //controls
    public KeyCode dashKey;
    public KeyCode overloadKey;
    public KeyCode HookKey;

    //graphics
    SpriteRenderer srPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        srPlayer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashing) return;
        if (overloading) return;

        dashTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        overloadTimer += Time.deltaTime;
        hookTimer += Time.deltaTime;

        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime));

        if (hookTimer > hookCooldown && Input.GetMouseButton(1))
        {
            //Instantiate(bullet, directionIndicator.transform);
            //shootTimer = 0;
            StartCoroutine(HookgoTo(hookTarget.position, hookDuration));
        }

        if (dashTimer > dashCooldown && Input.GetKeyDown(dashKey))
        {
            StartCoroutine(Dash());
        }

        if (shootTimer > shootCooldown && Input.GetMouseButton(0))
        {
            Instantiate(bullet, directionIndicator.transform);
            shootTimer = 0;
        }

        if (overloadTimer > overloadCooldown && Input.GetKey(overloadKey))
        {
            Instantiate(overload, transform);
            currentOverload = transform.GetChild(1);
            StartCoroutine(Overload());
        }
    }

    IEnumerator Dash()
    {
        dashing = true;
        float x = 0;
        float y = 0;

        if (Input.GetAxis("Horizontal") > 0) x = 1;
        if (Input.GetAxis("Horizontal") < 0) x = -1;
        if (Input.GetAxis("Vertical") > 0) y = 1;
        if (Input.GetAxis("Vertical") < 0) y = -1;
        if (x == 0 && y == 0) x = 1;
        if (x != 0 && y != 0)
        {
            x *= 0.707f;
            y *= 0.707f;
        }
        srPlayer.color = Color.red;
        rbPlayer.velocity = new Vector2((x * dashDistance), (y * dashDistance));
        yield return new WaitForSeconds(dashCooldown);
        rbPlayer.velocity = new Vector2(0, 0);
        srPlayer.color = new Color(130.0f / 255.0f, 134.0f / 255.0f, 253.0f / 255.0f, 1.0f);
        dashTimer = 0;
        dashing = false;
    }

    IEnumerator Overload()
    {
        float scaleCount = 0;
        overloading = true;
        for (float i = 0; i < overloadDuration; i+=0.01f) {
            scaleCount += overloadSpeed * Time.deltaTime;
            currentOverload.localScale = new Vector3(scaleCount, scaleCount, 0);
            yield return new WaitForSeconds(i);
        }
        currentOverload.GetComponent<Overload>().DestroyThis();
        overloadTimer = 0;
        overloading = false;
    }

    IEnumerator HookgoTo(Vector3 target, float duration)
    {
        dashing = true;
        float timeElapsed = 0;
        srPlayer.color = Color.red;
        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(transform.position, target, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        srPlayer.color = new Color(130.0f / 255.0f, 134.0f / 255.0f, 253.0f / 255.0f, 1.0f);
        dashing = false;
    }
}
