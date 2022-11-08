using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colMaster : MapCheckpoint
{

    public List<int> id_Colected_Col = new List<int>();
    public List<GameObject> colList = new List<GameObject>();
    public List<int> tentative_colected = new List<int>();
    public int id_tile_save;

  
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadCol;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadCol;
    }


    private void LoadCol(Scene scene, LoadSceneMode mode)
    {
        colList.Clear();
        colList.AddRange(GameObject.FindGameObjectsWithTag("PowerUp"));
        colList.AddRange(GameObject.FindGameObjectsWithTag("NucleoTile"));

        tentative_colected.Clear();


        foreach (GameObject a in colList) {
            foreach (int b in id_Colected_Col) {

                if (a.GetComponent<Collectable>().col_Id == b) {
                    if (a.tag == "NucleoTile") {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Collectables>().Tiles_Obtenidos.Add(a.GetComponent<NucleoTile>().get_MyTimmy());
                    }
                    a.SetActive(false);  
                    break;
                } 
            }
        }

    }

    public void CollectedItem(int id) {

        tentative_colected.Add(id);

    }
    public void SaveColected() {
        if (tentative_colected.Count > 0) {
            foreach(int a in tentative_colected)
            {
                id_Colected_Col.Add(a);
            }
            tentative_colected.Clear() ;
        }
    }

}
