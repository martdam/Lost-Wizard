using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage;
    public int health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Pj_LifeManager>().takeDamage(transform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Pj_LifeManager>().takeDamage(transform.localPosition);
        }
    }

    public void takeDamage(int dam) {

        health -= dam;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

}
