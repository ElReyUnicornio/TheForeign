using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public UIDocument playerUI;
    VisualElement root;

    public VisualElement Dialog;
    public Label DialogText;
    // Start is called before the first frame update
    void Start()
    {
        root = playerUI.rootVisualElement;
        DialogText = root.Q<Label>("Dialog-text");
        Dialog = root.Q("Dialog");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Router(string url)
    {
        switch (url)
        {
            case "dialog":
                Dialog.AddToClassList("active");
                Debug.Log("Class toggled");
                break;
            default:
                Dialog.RemoveFromClassList("active");
                Debug.Log("Class toggled");
                break;
        }
    }
}
