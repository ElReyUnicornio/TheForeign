using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player settings")]
    Rigidbody2D rbPlayer;
    public GameObject directionIndicator;
    public float speed = 640.0f;
    public GameObject interaction_F;
    public PlayerUI ui;

    [Header("States")]
    public bool talking = false;

    string interactionType;
    Sign actualSign;

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
    public bool hooked = false;
    private float hookTimer;
    public GameObject hook;
    public Transform hookTarget;
    public float hookDuration;
    public float hookCooldown;
    
    [Header("UI Settings")]
    public Scrollbar overloadScroll;
    public Scrollbar dashScroll;
    public Scrollbar hookScroll;

    //controls
    public KeyCode dashKey;
    public KeyCode overloadKey;
    public KeyCode hookKey;
    public KeyCode interact;

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
        if (talking) return;
        if (dashing) return;
        if (overloading) return;

        dashTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        overloadTimer += Time.deltaTime;
        hookTimer += Time.deltaTime;

        updateScrollbar();

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) rbPlayer.MovePosition(transform.position + (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") , 0) * speed * Time.deltaTime));

        if (hookTimer > hookCooldown && Input.GetMouseButton(1))
        {
            Instantiate(hook, directionIndicator.transform);
            hookTimer = 0;
        }

        if (hooked && Input.GetKey(hookKey))
        {
            hooked = false;
            StartCoroutine(HookGoTo(hookTarget.position, hookDuration));
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
            currentOverload = transform.GetChild(transform.childCount - 1);
            StartCoroutine(Overload());
        }

        if (Input.GetKeyDown(interact))
        {
            Interactions();
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
        float scaleCount = Time.deltaTime;
        overloading = true;
        while (scaleCount < overloadDuration) {
            scaleCount += Time.deltaTime;
            currentOverload.localScale = new Vector3(overloadSpeed * (scaleCount / overloadDuration), overloadSpeed * (scaleCount / overloadDuration), 0);
            yield return null;
        }
        currentOverload.GetComponent<Overload>().DestroyThis();
        overloadTimer = 0;
        overloading = false;
    }

    IEnumerator HookGoTo(Vector3 target, float duration)
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

    //check Cooldown scrollbar's
    void updateScrollbar() {
        if (overloadTimer <= overloadCooldown) {
            overloadScroll.size = overloadTimer / overloadCooldown;
        }else {
            overloadScroll.size = 1;
        }

        if (dashTimer <= dashCooldown) {
            dashScroll.size = dashTimer / dashCooldown;
        }else {
            dashScroll.size = 1;
        }

        if (hookTimer <= hookCooldown) {
            hookScroll.size = hookTimer / hookCooldown;
        }else {
            hookScroll.size = 1;
        }
    }

    //Triggers and colliders

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("interactable"))
        {
            interaction_F.SetActive(true);
            if (collision.GetComponent<Sign>() != null)
            {
                actualSign = collision.GetComponent<Sign>();
                interactionType = "sign";
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("interactable"))
        {
            interaction_F.SetActive(false);
            interactionType = "";
        }
    }

    //interacciones

    void Interactions ()
    {
        switch (interactionType)
        {
            case "sign":
                StartCoroutine(actualSign.Interaction(ui, this));
                break;
        }
    }
}
