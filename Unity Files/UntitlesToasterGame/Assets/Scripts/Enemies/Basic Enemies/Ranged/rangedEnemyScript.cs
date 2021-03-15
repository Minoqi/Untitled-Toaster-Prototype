using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class rangedEnemyScript : MonoBehaviour
{
    GameManagerScript GMS;
    GameObject player;
    //[HideInInspector]
    public GameObject projectilePreFab;
    public float timeBetweenShots;
    float timeBeforeShoot;
    float angle;
    Vector3 difference;
    Rigidbody2D rb;
    public int contactDamage;

    public float senseRange;
    public float moveSpeed;
    public float avoidMiddleRange;

    Vector2 distancePlayer;
    Vector2 distanceFromAvoid;
    //IEnumerable<GameObject> avoidMe;
    //GameObject avoidMe = null; 
    // Start is called before the first frame update
    void Start()
    {
        //avoidMe = FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("rangedUnitAvoidArea"));
        
        //avoidMe = (GameObject.Find("rangedUnitAvoidArea"));
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        timeBeforeShoot = GMS.timeNow + timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GMS.paused)
        {
            if (timeBeforeShoot <= GMS.timeNow)
            {
                Shoot();
                timeBeforeShoot = GMS.timeNow + timeBetweenShots;
            }

            foreach (GameObject avoidMe in GameObject.FindGameObjectsWithTag("Avoid"))
            {
                distancePlayer = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                distanceFromAvoid = new Vector2(avoidMe.transform.position.x - this.transform.position.x, avoidMe.transform.position.y - this.transform.position.y);

                if ((Mathf.Abs(distancePlayer.x) < senseRange) && (Mathf.Abs(distancePlayer.y) < senseRange))
                {
                    Vector3 newVelocity = new Vector3(distancePlayer.x * moveSpeed, distancePlayer.y * moveSpeed, 1.0f);
                    newVelocity.Normalize();
                    rb.velocity = -newVelocity;
                    if (avoidMe && (Mathf.Sqrt(Mathf.Pow(distanceFromAvoid.x, 2) + Mathf.Pow(distanceFromAvoid.y, 2))) < avoidMiddleRange)
                    {
                        Vector3 newNewVelocity = new Vector3(distanceFromAvoid.x * moveSpeed, distanceFromAvoid.y * moveSpeed, 1.0f);
                        newNewVelocity.Normalize();
                        rb.velocity += new Vector2(rb.velocity.x + -newNewVelocity.x, rb.velocity.y + -newNewVelocity.y);
                    }
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(contactDamage);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 diff = new Vector2(this.transform.position.x - collision.transform.position.x, this.transform.position.y - collision.transform.position.y);
            diff.x += (this.transform.position.x - player.transform.position.x);
            diff.y += (this.transform.position.y - player.transform.position.y);
            diff.Normalize();
            rb.velocity = new Vector2(diff.x * moveSpeed, diff.y * moveSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("rangedUnit"))
        {
            rb.velocity = new Vector2(this.transform.position.x - collision.transform.position.x, this.transform.position.y - collision.transform.position.y);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    void Shoot()
    {
        difference = player.transform.position - this.transform.position;
        angle = (Vector3.SignedAngle(Vector3.right, difference, Vector3.forward) + 360) % 360;
        GameObject bullet = Instantiate(projectilePreFab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 9001f);
        bullet.GetComponent<BasicProjectileScript>().xSpeed = difference.x / 1.5f;
        bullet.GetComponent<BasicProjectileScript>().ySpeed = difference.y / 1.5f;
    }
}
