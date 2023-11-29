using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Sign : MonoBehaviour
{
    public string[] messages = { "Holaaa", "adiooos" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Interaction (PlayerUI ui, PlayerMovement pm)
    {
        Debug.Log(messages.Length);
        pm.talking = true;
        int i = 0;
        ui.DialogText.text = messages[i];
        ui.Router("dialog");
        yield return new WaitForSeconds(1);

        while (i < messages.Length)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            {
                i++;
                Debug.Log(i);
                if (i < messages.Length)
                {
                    ui.DialogText.text = messages[i];
                }
            } else yield return null;
            Debug.Log(i);
        }

        yield return new WaitForSeconds(1);
        ui.Router("/");
        pm.talking = false;
    }
}
