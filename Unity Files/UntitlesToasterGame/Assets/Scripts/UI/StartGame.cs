using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    //Variables
    public string levelName;
    public Button startButton, exitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(BeginGame);
        exitButton.onClick.AddListener(EndGame);
    }

    //Switch Scenes
    void BeginGame()
    {
        SceneManager.LoadScene(levelName);
    }

    void EndGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
