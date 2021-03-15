using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    GameManagerScript GMS;
    GameObject player;
    Rigidbody2D rb;
    public GameObject kamikazePrefab;
    public GameObject shotPreFab;

    Vector2 distancePlayer;
    public float senseRange;
    public float playerMoveSpeedPercentage;
    float moveSpeed;
    Vector3 whereSpawn;

    [Tooltip("The angle at which the 3 bagles are fired")]
    public float bagleOffset;

    public bool isOpen = false;
    bool whiskSide;
    float whiskShootTime;
    float spawnTime;

    [Tooltip("This variable will also be used for the timing between the shotgun blasts when the boss is open")]
    public float timeBetweenSpawns;
    public float timeBetweenShots;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moveSpeed = player.GetComponent<PlayerController>().moveSpeed * (playerMoveSpeedPercentage / 100.0f);
        rb = this.GetComponent<Rigidbody2D>();
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        this.gameObject.transform.GetChild(2).transform.position = new Vector3(this.gameObject.transform.GetChild(3).transform.position.x - bagleOffset, this.gameObject.transform.GetChild(3).transform.position.y);
        this.gameObject.transform.GetChild(4).transform.position = new Vector3(this.gameObject.transform.GetChild(3).transform.position.x + bagleOffset, this.gameObject.transform.GetChild(3).transform.position.y);

        whiskShootTime = GMS.timeNow + timeBetweenShots;
        spawnTime = GMS.timeNow + timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMS.paused)
        {
            moveBoss();
            if (GMS.timeNow > whiskShootTime)
            {
                whiskShoot(whiskSide);
                whiskSide = !whiskSide;
                whiskShootTime = GMS.timeNow + timeBetweenShots;
            }
            if (GMS.timeNow > spawnTime)
            {
                if (!isOpen)
                {
                    whereSpawn = new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y, this.transform.position.z);
                    //adjust this, doesnt work when on left side
                    whereSpawn = customNormalize(whereSpawn);
                    Instantiate(kamikazePrefab, whereSpawn, Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 9001f));
                    isOpen = !isOpen;
                }
                else if (isOpen)
                {
                    shotgunShoot();
                    isOpen = !isOpen;
                }

                spawnTime = GMS.timeNow + timeBetweenSpawns;
            }
        }
    }


    void moveBoss()
    {
        distancePlayer = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
        if ((Mathf.Abs(distancePlayer.x) < senseRange) && (Mathf.Abs(distancePlayer.y) < senseRange))
        {
            Vector3 newVelocity = new Vector3(distancePlayer.x * moveSpeed, distancePlayer.y * moveSpeed, 1.0f);
            newVelocity.Normalize();
            rb.velocity = -newVelocity;

            ///use this to make the boss not go towards walls
            /*
            if (avoidMe && (Mathf.Sqrt(Mathf.Pow(distanceFromAvoid.x, 2) + Mathf.Pow(distanceFromAvoid.y, 2))) < avoidMiddleRange)
            {
                Vector3 newNewVelocity = new Vector3(distanceFromAvoid.x * (moveSpeed / 2), distanceFromAvoid.y * (moveSpeed / 2), 1.0f);
                newNewVelocity.Normalize();
                rb.velocity = new Vector2(rb.velocity.x + -newNewVelocity.x, rb.velocity.y + -newNewVelocity.y);
            }
            */
        }
        else
            rb.velocity = Vector2.zero;

        float angle = (Vector3.SignedAngle(Vector3.right, distancePlayer, Vector3.forward) + 360) % 360;
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 9001f);
    }

    void whiskShoot(bool whichSide)
    {
        if(whichSide)
        {
            this.gameObject.transform.GetChild(0).GetComponent<whiskScript>().shoot(player);
        }
        else
        {
            this.gameObject.transform.GetChild(1).GetComponent<whiskScript>().shoot(player);
        }
    }

    void shotgunShoot()
    {
        Vector3 diff;
        float angle;
        GameObject shot;

        //middle shot
        diff = GameObject.Find("bagleShot").transform.position - this.transform.position;
        angle = (Vector3.SignedAngle(Vector3.right, diff, Vector3.forward) + 360) % 360;
        shot = Instantiate(shotPreFab, this.transform.position, Quaternion.identity) as GameObject;
        shot.GetComponent<BasicProjectileScript>().setSpeed(diff.x, diff.y);
        shot.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 9001f);

        //left shot
        diff = GameObject.Find("bagleShotL").transform.position - this.transform.position;
        angle = (Vector3.SignedAngle(Vector3.right, diff, Vector3.forward) + 360) % 360;
        shot = Instantiate(shotPreFab, this.transform.position, Quaternion.identity) as GameObject;
        shot.GetComponent<BasicProjectileScript>().setSpeed(diff.x, diff.y);
        shot.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90 - 25), 9001f);

        //right shot
        diff = GameObject.Find("bagleShotR").transform.position - this.transform.position;
        angle = (Vector3.SignedAngle(Vector3.right, diff, Vector3.forward) + 360) % 360;
        shot = Instantiate(shotPreFab, this.transform.position, Quaternion.identity) as GameObject;
        shot.GetComponent<BasicProjectileScript>().setSpeed(diff.x, diff.y);
        shot.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90 + 25), 9001f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Vector2 diff = new Vector2(this.transform.position.x - col.transform.position.x, this.transform.position.y - col.transform.position.y);
            diff.x += (this.transform.position.x - player.transform.position.x);
            diff.y += (this.transform.position.y - player.transform.position.y);
            diff.Normalize();
            rb.velocity = new Vector2(diff.x * moveSpeed, diff.y * moveSpeed);
        }
    }

    Vector3 customNormalize(Vector3 vec)
    {
        Debug.Log(vec.x + " " + vec.y + " " + vec.z);
        float max = Mathf.Max(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
        Debug.Log("max" + max);
        vec.x /= max;
        vec.y /= max;
        vec.z /= max;
        Debug.Log(vec.x + " " + vec.y + " " + vec.z);

        return vec;
    }
}
