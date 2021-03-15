using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public float timeNow;
    [HideInInspector]
    public bool paused = false;
    static int whichScene;
    public string[] sceneNames;
    
    // Start is called before the first frame update
    void Start()
    {
        timeNow = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timeNow += Time.deltaTime;
        }

        if(!(GameObject.FindGameObjectsWithTag("Enemy").Length > 0))
        {
            whichScene++;
            if (whichScene >= sceneNames.Length)
                whichScene = 0;

            SceneManager.LoadScene(sceneNames[whichScene]);
        }
    }
}
