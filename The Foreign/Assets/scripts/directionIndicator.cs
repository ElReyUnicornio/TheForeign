using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class directionIndicator : MonoBehaviour
{
    Vector3 worldPosition;
    public Transform pivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = worldPosition - pivot.position;
        var angleRads = Mathf.Atan2(dir.x, dir.y);
        var angle = angleRads * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        gameObject.transform.localPosition = new Vector3(-MathF.Cos((angle + 90) * Mathf.Deg2Rad) * 1.5f, MathF.Sin((angle + 90) * Mathf.Deg2Rad) * 1.5f, -0.1f);
    }
}
