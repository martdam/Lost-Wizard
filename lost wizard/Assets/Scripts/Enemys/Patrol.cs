using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{

    public Transform[] patroPoints;
    public float speed;
    int currentPointIndex;

    float WaitTime;
    public float startWaitTime;


    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        WaitTime = startWaitTime;
        transform.position = patroPoints[0].position;
        transform.rotation = patroPoints[0].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patroPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (transform.position == patroPoints[currentPointIndex].position)
        {
             anim.SetBool("isMoving", false);
            if (WaitTime <= 0) {

                transform.rotation = patroPoints[currentPointIndex].rotation;
         
                if (currentPointIndex + 1 < patroPoints.Length) {
                    currentPointIndex++;
                } else {
                    currentPointIndex = 0;
                }
                WaitTime = startWaitTime;
            } else {
                WaitTime -= Time.deltaTime;
            }

        } else {
            anim.SetBool("isMoving", true);
        }
    }

    
}
