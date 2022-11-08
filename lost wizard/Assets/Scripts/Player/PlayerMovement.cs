using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player_Efects
{

    //----------------------variables----------------------
    //caminar y correr
    public float runSpeed;
    private float finalSpeed;
    private bool facingRight = true;
    private float moveInput;

    //-----------------------------------------------------
    //permitir movimiento
    private Vector3 SaveVel = Vector3.zero;
    public bool blocked = false;
    //-----------------------------------------------------
    //dash 
    public float dashSpeed;
    public bool EnableDash = true;
    public bool dashing = false;
    public float dashDuration = 1;
    public int dashDir = 1;
    private int MaxDash = 0;
    public int MaxDashValue = 0;
    //------------------------------------------------------   
    //saltos
    public bool IsGrounded = false;
    public float jumpForce;
    private int extraJumps;
    public int extraJumpValue;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatIsGround;

    //-----------------------------------------------------
    //wall slide
    public bool isTouchingFront;
    public Transform FrontCheck;
    bool wallSlding;
    public bool canSlide = false;
    public float WallSlidingSpeed;

    //WallJump
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    //-----------------------------------------------------
    //vida
    public int vida;
    public bool alive = true;

    /*/ataque
    public float timeBetweenAttacks;
    float nextAttackTime;
    public Transform attackPoints;
    public float attackRange;
    public LayerMask enemyLayer;
    public int damage;
    */
    //-----------------------------------------------------
    //Particulas

    public Transform dashPoint;
    public GameObject jumpPart;
    public GameObject dashPart;
    public GameObject WallSlPart;
    public GameObject runPart;



    //-----------------------------------------------------
    //componentes
    Rigidbody2D rb2D;
    Animator anim;
    public bool paused = false;

    //-----------------------------------------------------
    void Start()    {
        aud = gameObject.GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpValue;
        MaxDash = MaxDashValue;

        dashPart = Instantiate(dashPart, dashPoint);

        dashPart.GetComponent<ParticleSystem>().Stop();

        runPart = Instantiate(runPart, groundCheck.transform);
        runPart.transform.position = new Vector3(runPart.transform.position.x - 0.34f, runPart.transform.position.y + 0.4f, runPart.transform.position.z);

        WallSlPart = Instantiate(WallSlPart, FrontCheck.transform);
        WallSlPart.GetComponent<ParticleSystem>().Stop();

       WallSlPart.transform.position = new Vector3(WallSlPart.transform.position.x + 0.05f, WallSlPart.transform.position.y -    0.4f, WallSlPart.transform.position.z);

    }

    private void Update()
    {
        if (alive)
        {
            if (!paused)
            {
                IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsGround);
                isTouchingFront = Physics2D.OverlapCircle(FrontCheck.position, checkRadius, WhatIsGround);

                if (!dashing)
                {
                    walk();
                }

                dash();
                salto();
                if (canSlide)
                {
                    wallSlide();
                }
                face();
                animations();
            }
        }   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(attackPoints.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(FrontCheck.position, checkRadius);
    }


   /*  public void AttackEvnt() {

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoints.position, attackRange, enemyLayer);

        foreach (Collider2D col in enemiesToDamage)
        {
            col.GetComponent<Enemy>().takeDamage(damage);
        }
    }/*/

    void animations()   {

       /* if (Time.time > nextAttackTime && !wallSlding && !dashing)
        {
            if (Input.GetKey(KeyCode.X))
            {
                anim.SetTrigger("Attack");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
        */

        if (IsGrounded || wallSlding) {
            if (IsGrounded && anim.GetBool("isFalling")) {
                FindObjectOfType<CameraShake>().ShakeLand();
                play_land();
            }
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", false);
        } else if (rb2D.velocity.y > 0.1f){
            anim.SetBool("isJumping", true);
        }else if (rb2D.velocity.y < 0.1f && !wallSlding) {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }

        if (moveInput != 0 && IsGrounded && !dashing && !blocked) {
            anim.SetBool("IsWalking", true);
            play_step(true);
            if (!runPart.GetComponent<ParticleSystem>().isEmitting) {
                
                runPart.GetComponent<ParticleSystem>().Play();
            }
        } else {
            anim.SetBool("IsWalking", false);
           
            if (runPart.GetComponent<ParticleSystem>().isEmitting)
            { 
                play_step(false);
                runPart.GetComponent<ParticleSystem>().Stop();
            }
        }
        
        if (dashing) {
            anim.SetBool("isDashing", true);
        } else {
            anim.SetBool("isDashing", false);
        }

        if (wallSlding) {
            anim.SetBool("isSliding", true);
        } else {
            anim.SetBool("isSliding", false);
        }

    }

    public void bloqMov(bool mov){
        if (!mov && SaveVel == Vector3.zero) {
            blocked = true;
            SaveVel = rb2D.velocity;
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }else if (mov) {
            blocked = false;
            rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            rb2D.velocity = SaveVel;
            SaveVel = Vector3.zero;
        }
    }

    void salto(){
        if (IsGrounded ||wallSlding){
            extraJumps = extraJumpValue;
        }
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps >= 0 && IsGrounded)
            {
                endDash();
                rb2D.AddForce(transform.up * jumpForce);
                Instantiate(jumpPart, groundCheck.transform);

                play_Wjump();
            }else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !wallSlding && !IsGrounded)
            {
                endDash();
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                rb2D.AddForce(transform.up * jumpForce);
                extraJumps--;
                Instantiate(jumpPart,  groundCheck.transform);
                play_jump();
                
            }
        }
    }

    private void dash() {
        
        if ((!EnableDash && IsGrounded || !EnableDash && wallSlding) && MaxDashValue > 0) {
            EnableDash = true;
            MaxDash = MaxDashValue;
        }

        if ((Input.GetKeyDown(KeyCode.X)|| Input.GetKeyDown(KeyCode.CapsLock))  && !dashing && EnableDash) {
            MaxDash--;
            dashing = true;

            dashPart.GetComponent<ParticleSystem>().Play();
            Invoke("endDash", dashDuration);
            if (MaxDash <= 0)   {
                EnableDash = false;
            } 
        }

        if (dashing) {
            play_dash();
            rb2D.velocity = new Vector2(dashSpeed* dashDir * 2.5f, 0);
        }
    }
        
    private void endDash() {
        if (dashing){
            rb2D.velocity = new Vector2(0, 0);
            dashing = false;
            checkDashDir();
            if (dashPart.GetComponent<ParticleSystem>().isEmitting) {
                
                dashPart.GetComponent<ParticleSystem>().Stop();
            }

        }
    }

    private void walk() {
        moveInput= Input.GetAxisRaw("Horizontal");

        if(!facingRight && moveInput < 0 || facingRight && moveInput > 0) {
            checkDashDir();
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Z)) {
            finalSpeed = runSpeed * 1.3f;
        }else {
            finalSpeed = runSpeed;
        }

        rb2D.velocity = new Vector2(moveInput * finalSpeed, rb2D.velocity.y);     
    }

    private void wallSlide() {
        
        if (isTouchingFront && !IsGrounded && moveInput != 0) {
            wallSlding = true;
            dashDir = int.Parse((-moveInput).ToString());
            play_slide(true);
            if (!WallSlPart.GetComponent<ParticleSystem>().isEmitting) {

                WallSlPart.GetComponent<ParticleSystem>().Play();
            }
        } else {
            wallSlding = false;
            play_slide(false);
            if (WallSlPart.GetComponent<ParticleSystem>().isEmitting)
            {

                WallSlPart.GetComponent<ParticleSystem>().Stop();
            }
        }

        if (wallSlding) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }

        wallJump();
    }
   
    private void wallJump() {
        if (Input.GetKeyDown(KeyCode.Space) && wallSlding) {
            wallJumping = true;
            Instantiate(jumpPart, groundCheck.transform);
            Invoke("setWallJumpingFalse", wallJumpTime);
        }
        if (wallJumping) {
            play_Wjump();
            rb2D.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    private void setWallJumpingFalse() {
        wallJumping = false;
    }

    void face() {
        if (!blocked)
        {
            if (!facingRight && rb2D.velocity.x > 0 || facingRight && rb2D.velocity.x < 0)
            {
                flip();
            }
        }
    }

    private void flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void checkDashDir() {
        if (facingRight){
            dashDir = 1;
        } else{
            dashDir = -1;
        }
    }


    public void powerUp(bool wallSlide, bool jumpMax, bool dashMax) {

        if (wallSlide) {
            canSlide = true;
        }
        if (jumpMax) {
            extraJumpValue += 1;
        } 
        if (dashMax) {
            MaxDashValue += 1;
        }
    
    }

    public void loadStats(int jumps, int dashes, bool slide) {
        canSlide = slide;
        MaxDashValue = dashes;
        extraJumpValue = jumps;
    
    }

    private void OnCollisionEnter2D(Collision2D collision)    {
        if (dashing) {
            endDash();  
        }
    }

    public void pause(bool p) {
        paused = p;
        if (p)
        {

            gameObject.GetComponent<AudioSource>().Pause();

        }
        else {

            gameObject.GetComponent<AudioSource>().UnPause();
        }
    
    }
}
