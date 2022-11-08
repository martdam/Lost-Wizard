using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapManagement : MonoBehaviour
{

    public AudioClip MoveRoom;
    public AudioClip CantMoveRoom;
    public Color tileColor;
    public Color tileColor2x1;
    public Color tileColor2x2;
    public GameObject[] TileList;
    private GameObject Tile_A_Mover;
    public GameObject ShakPart1;
    public GameObject ShakPart2;
    public GameObject ShakPart3;
    bool Chekeado = false;
    bool posible = false;
    bool Moving = false;
    public float mov_dist;
    float dist_x = 0;
    float dist_y = 0;
    void Start()
    {
        TileList = GameObject.FindGameObjectsWithTag("MiniMap Tale");
        foreach (GameObject g in TileList) {
            if (g.GetComponent<MniniMapTale>().visible == false)
            {
                g.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        ShakPart1.GetComponent<ParticleSystem>().Stop();
        ShakPart2.GetComponent<ParticleSystem>().Stop();
        ShakPart3.GetComponent<ParticleSystem>().Stop();
    }

    public void conectTiles(MniniMapTale t1, string dir_t1,MniniMapTale t2, string dir_t2) {

        
        t1.dirBloq.Add(dir_t1[0]);

        if (t1.checkOwnDoor(dir_t1) && t2.checkOwnDoor(dir_t2))
        {
            t1.ConectDoor(t2.getConect(), t2.get_id());
        }

    
    }


    public void moverTile(int id) {
        if (!Chekeado)
        {
            checkearTile(id);
        }
        else if(posible && !Moving)
        {

            


            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                chekearDireccion('D', true, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                chekearDireccion('I', true, -1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                chekearDireccion('S', false, -1);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                chekearDireccion('N', false, 1);
            }



        }
    }


    void checkearTile( int id) {
        for (int i = 0; i < TileList.Length; i++) {
            if (TileList[i].GetComponent<MniniMapTale>().get_id() == id) {
                Tile_A_Mover = TileList[i];
                posible = true;

            }
        }
        Chekeado = true;
    }

    void chekearDireccion(char dir, bool x, float p)
    {

        ShakPart1.GetComponent<ParticleSystem>().Play();
        ShakPart2.GetComponent<ParticleSystem>().Play();
        ShakPart3.GetComponent<ParticleSystem>().Play();
        bool canMove = true;

        if (Tile_A_Mover.GetComponent<MniniMapTale>().dirBloq.Contains(dir)) {
            canMove = false;
            GetComponent<AudioSource>().clip = CantMoveRoom;
            GetComponent<AudioSource>().Play();

        }

        if (canMove) {

            Moving = true;

            FindObjectOfType<CameraShake>().ShakeRoom();
            GetComponent<AudioSource>().clip = MoveRoom;
            GetComponent<AudioSource>().Play();


            if (x) {
                dist_x = mov_dist * p;
                dist_y = 0;
            } else {
                dist_y = mov_dist * p;
                dist_x = 0;
            }

            Invoke("changeMove", 0.75f);
            Tile_A_Mover.gameObject.transform.SetPositionAndRotation(new Vector3(Tile_A_Mover.transform.position.x + dist_x, Tile_A_Mover.transform.position.y + dist_y, Tile_A_Mover.transform.position.z), Tile_A_Mover.transform.rotation);
        }

    }

    void changeMove()
    {
        Moving = false;

    }

    public void endTileMovment() {
        posible = false;
        Chekeado = false;
    }

    public void ActivarRender(int Tale)
    {
        for (int i = 0; i < TileList.Length; i++)
        {
            if (TileList[i].GetComponent<MniniMapTale>().get_id() == Tale)
            {
                TileList[i].GetComponent<MniniMapTale>().visible = true;
                TileList[i].GetComponent<SpriteRenderer>().enabled = true;
                break;

            }
        }

    }
    public void AclararTile(int Tale)
    {
        for (int i = 0; i < TileList.Length; i++)
        {
            if (TileList[i].GetComponent<MniniMapTale>().get_id() == Tale)
            {
                TileList[i].GetComponent<SpriteRenderer>().color = Color.white;
                break;

            }
        }

    }

    public void OscurecerTile(int Tale)
    {
        for (int i = 0; i < TileList.Length; i++)
        {
            if (TileList[i].GetComponent<MniniMapTale>().get_id() == Tale)
            {
                if (TileList[i].transform.childCount == 4)
                {
                    TileList[i].GetComponent<SpriteRenderer>().color = tileColor;
                }else if (TileList[i].transform.childCount == 8)
                {
                    TileList[i].GetComponent<SpriteRenderer>().color = tileColor2x1;
                }else if(TileList[i].transform.childCount >= 12)
                {
                    TileList[i].GetComponent<SpriteRenderer>().color = tileColor2x2;
                }
                break;


            }
        }

    }

}
