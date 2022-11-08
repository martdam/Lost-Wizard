using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Sprite GFX;

    public int col_Id;

    [SerializeField]private float initPos;
    [SerializeField]private float distFloat = 0.2f;
    [SerializeField]private float Speed = 0.25f;
    [SerializeField]private float finalPos;

   public GameObject colPart;

    private void Awake()
    {
        
        initPos = transform.position.y;
        finalPos = initPos + distFloat;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, finalPos), Speed *Time.deltaTime);
       
        
        if(transform.position.y >= initPos + distFloat) {
            finalPos = initPos;
        } else if(transform.position.y <= initPos)    {
            finalPos = initPos + distFloat;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().CollectedItem(col_Id);
            collision.GetComponent<Player_Collectables>().collect(gameObject);        
        }
    }
}
