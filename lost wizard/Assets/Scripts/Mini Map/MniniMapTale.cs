using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MniniMapTale : MonoBehaviour
{
    
    private string[] puertas_id;
    private int door_owned_to_conect;
   
    [SerializeField]private int Tale_id;
    private int conect_id;
    
    public Door_script[] puertas;

    public bool visible = false;

    public List<char> dirBloq = new List<char>();



    // Start is called before the first frame update
    void Start()
    {
        if (puertas.Length > 0)
        {
            puertas_id = new string[puertas.Length];
            for (int i = 0; i < puertas.Length; i++)
            {
                puertas[i].valoTale = Tale_id;
                puertas_id[i] = puertas[i].id;
            }
        }
    }

    public int get_id() { return Tale_id; }

    public bool checkOwnDoor(string ownDir) {

        if (puertas.Length > 0)
        {
            for (int i = 0; i < puertas_id.Length; i++)
            {
                if (puertas_id[i] == ownDir)
                {
                    door_owned_to_conect = i;
                    conect_id = i;
                    return true;
                }
            }
        }
        return false;
    }

    public Vector2 getConect() {
        
        return puertas[conect_id].getMyExit();
        
    }

    /*public void add_door_to_conect(  Vector2 Exit, int tale_id) {
       
        info_to_conection info1 = new info_to_conection();
        info1.position_to_exit = Exit;
        info1.value_of_exit_tale = tale_id;
        info_to_conect.Add(info1);
    }*/

    public void ConectDoor(Vector2 Exit, int tale_id )
    {
        puertas[door_owned_to_conect].conectar(Exit, tale_id);
        door_owned_to_conect = -1;

        //Debug.Log("puertas conectadas");
       
    }

    public void DeconectDoor(string Door_id) {
        if (puertas.Length > 0)
        {
            for (int i = 0; i < puertas_id.Length; i++)
            {
                if (puertas_id[i] == Door_id)
                {
                    //Debug.Log("tale " + Tale_id.ToString() + ": puerta desconectada");
                    puertas[i].desconectar();
                    break;
                }
            }
        }
    }

    public void HabilitarDir(char dir)
    {
        if (dirBloq.Contains(dir))
        {
            dirBloq.Remove(dir);
            HabilitarDir(dir);
        }
       
    }
    
 
}
