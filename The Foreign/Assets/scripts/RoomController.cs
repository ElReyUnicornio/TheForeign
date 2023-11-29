using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    GameObject[] halls = new GameObject[4];
    [SerializeField]
    GameObject[] doors = new GameObject[4];

    [Header("routes")]
    public bool route_up;
    public bool route_right;
    public bool route_down;
    public bool route_left;

    // Start is called before the first frame update
    void Start()
    {
        if (route_up)
        {
            halls[0].SetActive(true);
            doors[0].SetActive(false);
        }
        if (route_right)
        {
            halls[1].SetActive(true);
            doors[1].SetActive(false);
        }
        if (route_down)
        {
            halls[2].SetActive(true);
            doors[2].SetActive(false);
        }
        if (route_left)
        {
            halls[3].SetActive(true);
            doors[3].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
