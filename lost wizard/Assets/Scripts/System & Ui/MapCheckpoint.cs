using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCheckpoint : MonoBehaviour
{

    public GameObject[] TileList;
    public List<bool> sprite = new List<bool>();
    public List<Vector2> pos_t = new List<Vector2>();
    public List<int> id_t = new List<int>();

    // Start is called before the first frame update
    public void checkMapStat()
    {
        TileList = null;
        sprite.Clear();
        pos_t.Clear();

        TileList = GameObject.FindGameObjectsWithTag("MiniMap Tale");

        foreach (GameObject a in TileList) {
            sprite.Add(a.GetComponent<MniniMapTale>().visible);
            pos_t.Add(a.GetComponent<Transform>().position);
            id_t.Add(a.GetComponent<MniniMapTale>().get_id());

        }

    }

    public void setMapStats(Scene scene, LoadSceneMode mode)
    {

        TileList = null;

        TileList = GameObject.FindGameObjectsWithTag("MiniMap Tale");

        foreach (GameObject a in TileList) {
            for (int i = 0; i < sprite.Count; i++) {
                if (a.GetComponent<MniniMapTale>().get_id() == id_t[i]) {
                    a.GetComponent<Transform>().position = pos_t[i];
                   // Debug.Log(sprite[i]);
                    a.GetComponent<MniniMapTale>().visible = sprite[i];
                }
            } 
        }
    
    }

    public void clearData() {
        TileList = null;
        sprite.Clear();
        id_t.Clear();
        pos_t.Clear();
    }
}
