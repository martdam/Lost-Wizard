using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_script : MonoBehaviour
{
    //posiciones salida/entrada
    public Vector2 TpExit;
    private Vector2 MyExit;

    //estado
    public bool open = false;
    public string id;
    public int valoTale;
    public bool unic = false;


    //componentes
    BoxCollider2D bx2D;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        bx2D = GetComponent<BoxCollider2D>();
        MyExit = transform.GetChild(0).transform.position;
        open = bx2D.isTrigger;
        if (unic) {
            conectar(MyExit, valoTale);
            anim.SetBool("Unique", true);
        }
    }

    public void conectar(Vector2 salida_n, int tale_n) {
        TpExit= salida_n;
        valoTale = tale_n;
        open = true;
        bx2D.isTrigger = open;
        anim.SetBool("isOpen", open);
    }

    public void desconectar() {
        TpExit = Vector2.zero;
        valoTale = 0;
        open = false;
        anim.SetBool("isOpen", false);
        bx2D.isTrigger = open;

    }

    public Vector2 getMyExit() {
        return MyExit;    
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            if (!collision.GetComponent<PlayerMovement>().blocked)
            {
                GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>().OscurecerTile(collision.GetComponent<Player_Collectables>().actualTileId);

                collision.transform.SetPositionAndRotation(new Vector3(TpExit.x, TpExit.y, collision.transform.position.z), collision.transform.rotation);
                collision.GetComponent<Player_Collectables>().set_actualTile(valoTale);
                GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>().ActivarRender(valoTale);
                GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>().AclararTile(collision.GetComponent<Player_Collectables>().actualTileId);
            }
        }
        
    }

    
}
