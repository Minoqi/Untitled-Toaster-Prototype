using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamikazeScript : MonoBehaviour
{
    public int contactDamage;
    public int explosionDamage;
    public float startMoveSpeed;
    public float countdownMoveSpeed;
    public float detectPlayerRangeX;
    public float detectPlayerRangeY;
    public float countdownRangeX;
    public float countdownRangeY;
    bool detectPlayer;
    bool countingDown;
    GameObject player;
    Rigidbody2D rb;
    GameManagerScript GMS;

    float timeStart = -1;
    public float countdownLength;

    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        player = GameObject.Find("Player");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        detectPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMS.paused)
        {
            if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= detectPlayerRangeX && Mathf.Abs(this.transform.position.y - player.transform.position.y) <= detectPlayerRangeY)
                detectPlayer = true;
            else
                detectPlayer = false;

            if (detectPlayer)
                moveToPlayer();
            else
                rb.velocity = Vector2.zero;

            if (Mathf.Abs(this.transform.position.x - player.transform.position.x) <= countdownRangeX && Mathf.Abs(this.transform.position.y - player.transform.position.y) <= countdownRangeY)
                countingDown = true;

            if (countingDown)
            {
                if (timeStart == -1)
                    timeStart = GMS.timeNow;

                if (timeStart + countdownLength <= GMS.timeNow)
                    Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(contactDamage);
            countingDown = true;
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    void moveToPlayer()
    {
        float distanceX = player.transform.position.x - this.transform.position.x;
        float distanceY = player.transform.position.y - this.transform.position.y;
        if (!countingDown)
        {
            Vector3 newVelocity = new Vector3(distanceX, distanceY, 0.0f);
            newVelocity.Normalize();
            newVelocity.x *= startMoveSpeed;
            newVelocity.y *= startMoveSpeed;
            rb.velocity = newVelocity;
        }
        else
        {
            Vector3 newVelocity = new Vector3(distanceX, distanceY, 0.0f);
            newVelocity.Normalize();
            newVelocity.x *= countdownMoveSpeed;
            newVelocity.y *= countdownMoveSpeed;
            rb.velocity = newVelocity;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
        GameObject myExplosion = Instantiate(explosion);
        myExplosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        myExplosion.GetComponent<explosionScript>().damage = explosionDamage;
    }
}
