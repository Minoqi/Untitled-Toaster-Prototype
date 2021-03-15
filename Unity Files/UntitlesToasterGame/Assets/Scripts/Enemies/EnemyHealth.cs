using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Variables
    public int health = 10;
    enemyIFrames iFrame;
    public GameObject money;
    public GameObject deathAnimation;

    // Start is called before the first frame update
    void Start()
    {
        iFrame = this.gameObject.GetComponent<enemyIFrames>();
    }

    //Take Damage
    public void TakeDamage(int damage)
    {
        //Subtract from Health

        if (iFrame.canTakeDamage)
        {
            iFrame.tookDamage = true;
            iFrame.canTakeDamage = false;
            health -= damage;

        }
        //Destroy Itself
        if(health <= 0)
        {
            Die();
        }
    }

    //Die
    void Die()
    {
        Instantiate(deathAnimation, transform.position, transform.rotation);
        //Instantiate(money, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
