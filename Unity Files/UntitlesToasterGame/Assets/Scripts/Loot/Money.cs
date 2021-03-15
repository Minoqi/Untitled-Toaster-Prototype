using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    //Variables
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Add moeny to player
    void OnTriggerEnter2D(Collider2D player)
    {
        if(player.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().Cash(value);
            Debug.Log("Cash: $" + player.GetComponent<PlayerController>().cash);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
