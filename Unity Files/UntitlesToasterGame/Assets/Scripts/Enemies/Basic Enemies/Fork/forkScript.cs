using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forkScript : MonoBehaviour
{
    GameManagerScript gms;
    public float speed;
    public int damage;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(gms.paused)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
        else
        {
            rb.velocity = new Vector2(0.0f, speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            speed *= -1;
            rb.velocity = new Vector2(0.0f, speed);
        }
    }

}
