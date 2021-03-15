using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    //Variables
    public int damage;
    public Animator anim;
    public Transform attackPosition;
    public LayerMask enemies;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    //Attack
    //void Attack()
    //{
    //    if (Input.GetMouseButtonDown(1)) //Attack
    //    {
    //        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemies);
    //        for(int i = 0; i < enemiesToDamage.Length; i++)
    //        {
    //            Debug.Log("Loop");
    //            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
    //        }
    //        anim.SetTrigger("Active");
    //    }
    //}

    void Animation()
    {
        if(Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("Active");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    //Draw Hitter
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
    }
}
