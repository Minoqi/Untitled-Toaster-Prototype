using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameManagerScript gms;
    //Variables
    public float speed;
    private Vector3 moveDirection;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    //Collision Events
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //If hits enemy
        if(hitInfo.gameObject.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<EnemyHealth>().TakeDamage(damage);

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

    // Update is called once per frame
    void Update()
    {
        if (!gms.paused)
        {
            transform.position = transform.position + moveDirection * speed * Time.deltaTime;
        }
    }
}
