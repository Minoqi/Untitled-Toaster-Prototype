using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spoonScript : MonoBehaviour
{
    GameManagerScript GMS;
    RaycastHit2D hit;
    public Vector2 left;
    public Vector2 right;
    int layerMask = 1 << 8;
    public float spoonMoveSpeed;
    public float arcHeight;
    public int damage;
    Vector3 newPos;
    bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        hit = Physics2D.Raycast(this.gameObject.transform.position, Vector3.left, Mathf.Infinity, layerMask);
        left = hit.point;

        hit = Physics2D.Raycast(this.gameObject.transform.position, Vector3.right, Mathf.Infinity, layerMask);
        right = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMS.paused)
        {
            if (moveRight)
            {
                float x0 = left.x;
                float x1 = right.x;
                float dist = x1 - x0;
                float nextX = Mathf.MoveTowards(transform.position.x, x1, spoonMoveSpeed * Time.deltaTime);
                float baseY = Mathf.Lerp(left.y, right.y, (nextX - x0) / dist);
                float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                newPos = new Vector3(nextX, baseY + arc, transform.position.z);
                transform.position = newPos;
            }
            else
            {
                float x0 = right.x;
                float x1 = left.x;
                float dist = x1 - x0;
                float nextX = Mathf.MoveTowards(transform.position.x, x1, spoonMoveSpeed * Time.deltaTime);
                float baseY = Mathf.Lerp(right.y, left.y, (nextX - x0) / dist);
                float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                newPos = new Vector3(nextX, baseY + arc, transform.position.z);
                transform.position = newPos;
            }

        }
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            moveRight = !moveRight;
        }
    }
}
