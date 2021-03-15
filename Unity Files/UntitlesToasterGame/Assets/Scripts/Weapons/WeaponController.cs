using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    GameManagerScript gms;

    //Variables
    bool canShoot;
    public float shootDelay = 0.0f;
    float timeElapsed = 0.0f;
    //Bullets
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource bulletSFX;

    // Start is called before the first frame update
    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        canShoot = true;
        timeElapsed = shootDelay;
        bulletSFX = GetComponent<AudioSource>();
    }

    //Controls
    void Controls()
    {
        if (!gms.paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                bulletSFX.Play();
            }
        }
    }

    //Shoot
    void Shoot()
    {
        if (canShoot)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeElapsed = shootDelay;
            canShoot = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeElapsed);
        timeElapsed -= Time.deltaTime;
        if (timeElapsed <= 0)
        {
            canShoot = true;
            Controls();
        }
    }
}
