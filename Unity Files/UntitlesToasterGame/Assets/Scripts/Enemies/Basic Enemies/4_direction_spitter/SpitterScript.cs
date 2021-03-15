using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterScript : MonoBehaviour
{
    public GameObject basicProj;
    GameManagerScript gms;
    float time;
    public float timeBetweenShots = 5.0f;
    float timeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        time = gms.timeNow;
        timeToShoot = time + timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gms.paused)
        {
            time = gms.timeNow;
            if (time >= timeToShoot)
            {
                shootDirection(2, 0);
                shootDirection(-2, 0);
                shootDirection(0, 2);
                shootDirection(0, -2);

                timeToShoot = time + timeBetweenShots;
            }
        }
    }

    void shootDirection(float X, float Y)
    {
        GameObject newProj = Instantiate(basicProj);
        newProj.transform.position = new Vector2(this.transform.position.x + X/2, this.transform.position.y + Y/2);
        newProj.GetComponent<BasicProjectileScript>().xSpeed = X;
        newProj.GetComponent<BasicProjectileScript>().ySpeed = Y;
    }
}
