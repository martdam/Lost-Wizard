using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   public GameMaster gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.lastCheckPointPos = transform.position;
            gm.dashes = collision.GetComponent<PlayerMovement>().MaxDashValue;
            gm.jumps = collision.GetComponent<PlayerMovement>().extraJumpValue;
            gm.slide = collision.GetComponent<PlayerMovement>().canSlide;
            if (collision.GetComponent<Player_Collectables>().actualTileId != -1)
            {
                gm.id_tile_save = collision.GetComponent<Player_Collectables>().actualTileId;
            }

            GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>().AclararTile(collision.GetComponent<Player_Collectables>().actualTileId);
    
            gm.SaveColected();
        }
    }
}
