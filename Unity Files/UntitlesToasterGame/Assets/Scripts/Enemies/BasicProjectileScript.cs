using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileScript : MonoBehaviour
{
    //Variables
    GameManagerScript gms;
    public float time;
    float timeBeforeDestroy = 10.0f;
    public float timeToDestroy;
    public int damage;
    public float xSpeed;
    public float ySpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        time = gms.timeNow;
        timeToDestroy = gms.timeNow + timeBeforeDestroy;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gms.paused)
        {
            time = gms.timeNow;
            if (time > timeToDestroy)
            {
                Destroy(this.gameObject);
            }
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else
            rb.velocity = new Vector2(0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //If hits player
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            //Player takes damage
            hitInfo.GetComponent<PlayerController>().TakeDamage(damage);

            //Destroy Itself
            Destroy(gameObject);
        }

        //If hits wall
        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            //Destroy Itself
            Destroy(gameObject);
        }

    }

    public void setSpeed(float newX, float newY)
    {
        xSpeed = newX;
        ySpeed = newY;
    }
}
