using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    GameManagerScript GMS;
    public float explosionDuration;
    float timeStart;
    float timeEnd;
    float timeCount;
    float normalizeTime;
    [HideInInspector]
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        timeStart = Time.deltaTime;
        timeEnd = timeStart + explosionDuration;
        timeCount = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMS.paused)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= timeEnd)
            {
                Destroy(this.gameObject);
            }
            else
            {
                normalizeTime = (timeCount * 1.5f) / timeEnd;
                this.transform.localScale = new Vector3(2.75f * normalizeTime, 2.75f * normalizeTime, 0.0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
