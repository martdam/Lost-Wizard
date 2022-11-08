using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pj_LifeManager : MonoBehaviour
{
    private GameMaster gm;
    private Rigidbody2D Rb2d;
    Animator anim;
    public GameObject deathPart;
    // Start is called before the first frame update
    void Start()
    {

        anim =gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (gm.lastCheckPointPos != Vector2.zero)
        {
            transform.position = gm.lastCheckPointPos;
        }
        Rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    public void takeDamage(Vector2 EnemyPos)
    {

        if (gameObject.GetComponent<PlayerMovement>().alive)
        {
            
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<PlayerMovement>().alive = false;
            gameObject.GetComponent<PlayerMovement>().play_hit();

            int d = gameObject.GetComponent<PlayerMovement>().dashDir;
            float valorimpulso = 500;
            Vector2 impulso;

            if (d > 0)
            {
                impulso.x = -valorimpulso;
            }
            else
            {
                impulso.x = valorimpulso;
            }

            if (Rb2d.velocity.y > 0.1)
            {
                impulso.y = -valorimpulso;
            }
            else
            {
                impulso.y = valorimpulso;
            }

            stopMov();

            anim.SetBool("isJumping", false);
            anim.SetBool("isDashing", false);
            anim.SetTrigger("Hit");
            Rb2d.AddForce(impulso);
            Invoke("stopMov", 0.05f);
        }
    }

    void stopMov() {
        Rb2d.gravityScale = 0f;
        Rb2d.velocity = Vector2.zero;
    }

    public void die() {
        Instantiate(deathPart, transform);
        gameObject.GetComponent<PlayerMovement>().play_die();

        Invoke("resetlvl", 0.6f);
    }

    private void resetlvl()
    {

        GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().diePoint();
    }
}
