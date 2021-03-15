using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyIFrames : MonoBehaviour
{
    SpriteRenderer sr;
    GameManagerScript GMS;
    public bool canTakeDamage = true;
    public bool tookDamage;

    float invincibilityLength;
    public float timeCurrent;
    public float timeEnd;

    Color newCol;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        timeCurrent = GMS.timeNow;

        invincibilityLength = 0.75f;

        canTakeDamage = true;
        tookDamage = false;
        newCol = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMS.paused)
        {
            timeCurrent = GMS.timeNow;
            if (canTakeDamage)
            {
                timeEnd = timeCurrent + invincibilityLength;
            }

            if (tookDamage && (timeCurrent <= timeEnd))
            {
                newCol.a = tookDamageFunc(timeCurrent);
            }
            else
            {
                newCol.a = 255;
                tookDamage = false;
                canTakeDamage = true;
            }

            sr.color = newCol;
        }
    }

    float tookDamageFunc(float timeCurrent)
    {
        return Mathf.Abs(Mathf.Sin(timeCurrent * 15.0f));
    }
}