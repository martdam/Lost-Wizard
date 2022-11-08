using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl_Save_Obj : MonoBehaviour
{
    public bool save;
    public bool nextLvl;
    public string siguienteScena ="Level_";
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (save)
            {
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().saveGame();
            }
            if (nextLvl)
            {
                GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().nextLevel(siguienteScena);
            }


        }
    }
}
