using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public UIDocument playerUI;
    VisualElement root;

    public VisualElement Dialog;
    public VisualElement gameover;
    public VisualElement pause;

    public Label DialogText;
    Button btnPause0;
    Button btnPause1;
    Button btnPause2;
    Button btnPause3;
    Button btnGO1;
    Button btnGO2;
    Button btnGO3;

    public PlayerStats playerStats;

    public bool paused = false;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        root = playerUI.rootVisualElement;
        DialogText = root.Q<Label>("Dialog-text");
        Dialog = root.Q("Dialog");
        gameover = root.Q("GameOver");
        pause = root.Q("pause");

        btnPause0 = root.Q<Button>("btnPause0");
        btnPause1 = root.Q<Button>("btnPause1");
        btnPause2 = root.Q<Button>("btnPause2");
        btnPause3 = root.Q<Button>("btnPause3");
        btnGO1 = root.Q<Button>("btnGameover1");
        btnGO2 = root.Q<Button>("btnGameover2");
        btnGO3 = root.Q<Button>("btnGameover3");

        btnPause0.clicked += btnResume;
        btnPause1.clicked += btnRestart;
        btnPause2.clicked += btnMenu;
        btnPause3.clicked += btnExit;

        btnGO1.clicked += btnRestart;
        btnGO2.clicked += btnMenu;
        btnGO3.clicked += btnExit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();

        if (playerStats.HP < 1 && !dead)
        {
            paused = false;
            Pause();
            dead = true;
            Router("gameover");
            
        }
    }

    public void Router(string url)
    {
        switch (url)
        {
            case "dialog":
                Dialog.AddToClassList("active");
                gameover.RemoveFromClassList("active");
                pause.RemoveFromClassList("active");
                Debug.Log("Dialog opened");
                break;
            case "gameover":
                gameover.AddToClassList("active");
                Dialog.RemoveFromClassList("active");
                pause.RemoveFromClassList("active");
                Debug.Log("Dialog opened");
                break;
            case "pause":
                pause.AddToClassList("active");
                gameover.RemoveFromClassList("active");
                Dialog.RemoveFromClassList("active");
                Debug.Log("Dialog opened");
                break;
            default:
                Dialog.RemoveFromClassList("active");
                gameover.RemoveFromClassList("active");
                pause.RemoveFromClassList("active");
                Debug.Log("all UI have been closed");
                break;
        }
    }

    public void Pause ()
    {
        if (!paused && !dead)
        {
            Router("pause");
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
            }
        }
        else if (paused && !dead)
        {
            Router("/");
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
            }
        }
        
    }

    //buttons
    void btnResume ()
    {
        Pause();
        Debug.Log("resumed");
    }

    void btnRestart()
    {
        Pause();
        SceneManager.LoadScene(1);
        Debug.Log("restarted");
    }

    void btnMenu()
    {
        Pause();
        SceneManager.LoadScene(0);
        Debug.Log("back to menu");
    }

    void btnExit()
    {
        Application.Quit();
        Debug.Log("exit");
    }
}
