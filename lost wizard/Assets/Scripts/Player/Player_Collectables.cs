using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collectables : MonoBehaviour
{

    public int actualTileId = -1;
    private bool IsInTale=false;


    public AudioClip usecore;

    Animator anim;

    MapManagement mapManager;

    public SpriteRenderer collectPoint;
    public Sprite coll_Sprite;


    public Sprite coll_SprDefault;

    public List<int> Tiles_Obtenidos = new List<int>();

    public GameObject useCorePart;




    // Start is called before the first frame update
    void Start() {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>();
        anim = GetComponent<Animator>();
        useCorePart = Instantiate(useCorePart, collectPoint.transform);
        useCorePart.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectPoint.sprite != coll_Sprite) { 
        collectPoint.sprite = coll_Sprite;
        }

        if (IsInTale && gameObject.GetComponent<PlayerMovement>().IsGrounded) {


            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                collectPoint.sprite = coll_SprDefault;
                anim.SetTrigger("UsingCore");
                anim.SetBool("RoomMove", true);

                gameObject.GetComponent<AudioSource>().clip = usecore;
                gameObject.GetComponent<AudioSource>().pitch = 1;
                gameObject.GetComponent<AudioSource>().volume = 1;
                gameObject.GetComponent<AudioSource>().loop = true;
                gameObject.GetComponent<AudioSource>().Play();


            }
            else if (Input.GetKey(KeyCode.LeftControl) && anim.GetBool("RoomMove")) {

                if (!useCorePart.GetComponent<ParticleSystem>().isEmitting) {

                    useCorePart.GetComponent<ParticleSystem>().Play();
                }
                gameObject.GetComponent<PlayerMovement>().bloqMov(false);
                mapManager.moverTile(actualTileId);
            
            }else if (Input.GetKeyUp(KeyCode.LeftControl))
            {

                useCorePart.GetComponent<ParticleSystem>().Stop();
                anim.SetBool("RoomMove", false);
                gameObject.GetComponent<PlayerMovement>().bloqMov(true);
                mapManager.endTileMovment();

                gameObject.GetComponent<AudioSource>().Stop();
                gameObject.GetComponent<AudioSource>().loop = false;
            }   
        }
        
    }

    public void set_actualTile(int id) {
        actualTileId = id;
        gameObject.GetComponent<PlayerMovement>().play_door();
       //Debug.Log(true);
        CheckTileList();
        
    }

    public void CheckTileList() {
        if (Tiles_Obtenidos.Count > 0) {
            for (int i = 0; i < Tiles_Obtenidos.Count; i++) {
                if (Tiles_Obtenidos[i] == actualTileId) {
                    IsInTale = true;
                    break;
                } else{
                    IsInTale = false;
                }
            }
        } 
    
    }

    public void collect(GameObject collObj) {

        anim.SetBool("isJumping", false);
        anim.SetBool("isDashing", false);
        anim.SetTrigger("grabObj");

        coll_Sprite = collObj.GetComponent<Collectable>().GFX;

        Instantiate(collObj.GetComponent<Collectable>().colPart, collectPoint.transform);

        if (collObj.tag == "NucleoTile")
        {
            Tiles_Obtenidos.Add(collObj.GetComponent<NucleoTile>().get_MyTimmy()) ;
            CheckTileList();
        }else if (collObj.tag == "PowerUp")
        {
            GetComponent<PlayerMovement>().powerUp(collObj.GetComponent<PowerUp>().wallup, collObj.GetComponent<PowerUp>().jumpUp, collObj.GetComponent<PowerUp>().dashUp);
        }
        collectPoint.gameObject.GetComponent<AudioSource>().Play();

        Destroy(collObj);

    }
    
}
