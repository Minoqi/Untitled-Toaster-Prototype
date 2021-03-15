using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthUIScript : MonoBehaviour
{
    public GameObject litCoilPreFab;
    public GameObject offCoilPreFab;
    
    int playerHP;
    int playerStartHP;
    GameObject[] healthUI;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerController>().health;
        playerStartHP = playerHP;
        healthUI = new GameObject[playerStartHP+1];
        for (int i = 1; i < playerStartHP + 1; ++i) 
        {
            healthUI[i] = Instantiate(litCoilPreFab);
            healthUI[i].transform.position = new Vector3(-7f + ((i - 1) * 0.5f), 4f, -9f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateHPUI()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerController>().health;
        for (int i = 1; i < playerStartHP + 1; ++i) 
        {
            Destroy(healthUI[i]);
            if (i > playerHP)
            {
                healthUI[i] = Instantiate(offCoilPreFab);
                healthUI[i].transform.position = new Vector3(-7f + ((i - 1) * 0.5f), 4f, -9f);
            }
            else
            {
                healthUI[i] = Instantiate(litCoilPreFab);
                healthUI[i].transform.position = new Vector3(-7f + ((i - 1) * 0.5f), 4f, -9f);
            }
        }
    }
}
