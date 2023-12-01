using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    UIDocument menu;
    Button startButton;
    Button exitButton;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<UIDocument>();
        root = menu.rootVisualElement;
        startButton = root.Q<Button>();
        exitButton = root.Query<Button>().AtIndex(1);

        startButton.clicked += StartEvt;
        exitButton.clicked += ExitEvt;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartEvt()
    {
        Debug.Log("Escena cambiada");
        SceneManager.LoadScene(1);
    }

    void ExitEvt()
    {
        Debug.Log("Has salido de la aplicación");
        Application.Quit();
    }
}
