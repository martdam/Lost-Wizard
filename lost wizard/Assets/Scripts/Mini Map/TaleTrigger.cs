using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaleTrigger : MonoBehaviour
{
    public string Direction = "N";
    public MniniMapTale father_script;
    private bool conected = false;
    private bool blocked = false;
        
    private void Start()
    {

        father_script = gameObject.GetComponentInParent<MniniMapTale>();
    }

 

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("TileTrigger"))
        {
            father_script.DeconectDoor(Direction);
            father_script.HabilitarDir(Direction[0]);
            conected = false;
        }
        if (collision.gameObject.CompareTag("MapWall")) {


            father_script.HabilitarDir(Direction[0]);
            blocked = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("TileTrigger")&& !conected)
        {
            //Debug.Log(true);
            conected = true;
            TaleTrigger secondTale = collision.gameObject.GetComponent<TaleTrigger>();
            GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManagement>().conectTiles(father_script, Direction, secondTale.father_script, secondTale.Direction);

        }
        if(collision.gameObject.CompareTag("MapWall") && !blocked)
        {
            blocked = true;
            father_script.dirBloq.Add(Direction[0]);
        }
    }
}
