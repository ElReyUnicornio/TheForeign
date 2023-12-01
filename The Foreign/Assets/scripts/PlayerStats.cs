using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int HP = 5;
    public int damage = 1;
    public int bulletDamage = 1;
    public int overloadDamage = 5;

    [Header("UI settings")]
    public TextMeshProUGUI hpUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpUI.text = "HP: " + HP + "/5";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("pikes"))
        {
            HP -= collision.transform.GetComponent<Pikes>().damage;
        }
    }
}
