using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiskScript : MonoBehaviour
{
    public GameObject projectilePreFab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(GameObject player)
    {
        Vector3 difference = player.transform.position - this.transform.position;
        float angle = (Vector3.SignedAngle(Vector3.right, difference, Vector3.forward) + 360) % 360;
        GameObject bullet = Instantiate(projectilePreFab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
        bullet.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 9001f);
        bullet.GetComponent<BasicProjectileScript>().xSpeed = difference.x / 1.5f;
        bullet.GetComponent<BasicProjectileScript>().ySpeed = difference.y / 1.5f;
    }
}
