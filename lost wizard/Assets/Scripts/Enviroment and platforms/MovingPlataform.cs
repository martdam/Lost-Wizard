using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patroPoints;
    public float speed;
    int currentPointIndex;

    float WaitTime;
    public float startWaitTime;

    
    void Start()
    {
        WaitTime = startWaitTime;
        transform.position = patroPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patroPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (transform.position == patroPoints[currentPointIndex].position)
        {
            if (WaitTime <= 0)
            {
                if (currentPointIndex + 1 < patroPoints.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }
                WaitTime = startWaitTime;
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().alive) {
            collision.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }

}
