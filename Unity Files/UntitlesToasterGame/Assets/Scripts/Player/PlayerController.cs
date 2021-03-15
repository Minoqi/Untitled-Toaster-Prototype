using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    GameManagerScript GMS;

    //Variables
    //Movement
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    enemyIFrames iFrames;
    Vector2 movement;
    //Stats
    public int health;
    public int cash;

    // Start is called before the first frame update
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        iFrames = this.gameObject.GetComponent<enemyIFrames>();
        cash = 0;
    }

    //Hit by object
    public void TakeDamage(int damage)
    {
        //Takle Damage
        if (iFrames.canTakeDamage)
        {
            health -= damage;
            iFrames.tookDamage = true;
            iFrames.canTakeDamage = false;
        }
        //Update UI
        GameObject.Find("Canvas").GetComponent<healthUIScript>().updateHPUI();

        //Die
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Lose");
        }
    }

    //Grab cash
    public void Cash(int value)
    {
        //Increase cash
        cash += value;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(!GMS.paused)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GMS.paused = !GMS.paused;        
        }
    }

    //Fixed Update
    void FixedUpdate()
    {
        if (!GMS.paused)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnDestroy()
    {
        GMS.paused = true;
    }
}
