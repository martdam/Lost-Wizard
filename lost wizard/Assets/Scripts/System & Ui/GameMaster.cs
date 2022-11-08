using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : colMaster
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    public int dashes ;
    public int jumps ;
    public bool slide ;


    AudioSource aud1;

    private void Start()
    {
        aud1 = GetComponent<AudioSource>();
  //      aud1.Play();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }

        
    }


    public void newGame() {
        lastCheckPointPos = Vector2.zero;
        clearData();
        id_Colected_Col.Clear();
        id_tile_save = 0;

        Initiate.Fade("Video", Color.white, 1.0f);

    }

    public void nextLevel(string newLvl) {
        if (newLvl[0]== 'L')
        {
            SceneManager.sceneLoaded += playerStats;
        }

        if (GameObject.FindGameObjectWithTag("BkgSound"))
        {
           GameObject.FindGameObjectWithTag("BkgSound").GetComponent<Sound>().changeScene();
                
        }
        if(newLvl[0] != 'L')
        {
            aud1.Stop();
        }
        saveGame(newLvl);
        lastCheckPointPos = Vector2.zero;
        clearData();
        id_Colected_Col.Clear();
        id_tile_save = 0;
        
        Initiate.Fade(newLvl, Color.black, 1.0f);
        
    }

    public void diePoint() {

        checkMapStat();
        SceneManager.sceneLoaded += checkpointReset;
        SceneManager.sceneLoaded += playerStats;
        SceneManager.sceneLoaded += setMapStats;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    private void checkpointReset(Scene scene, LoadSceneMode mode) {

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Collectables>().set_actualTile(id_tile_save);
        }

    }


    private void playerStats(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().loadStats(jumps, dashes, slide);
        }
        
    }
  
    public void saveGame(string name = "default") {
        if (name == "default")
        {
            SaveSystem.SavePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(), SceneManager.GetActiveScene().name);
        }
        else
        {
            SaveSystem.SavePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(),name);
        }
    
    }

    
    public void LoadGame() {

        aud1.Play();
        SaveData data = SaveSystem.loadPlayer();

        Time.timeScale = 1;
        jumps = data.jumps;
        dashes = data.Dashes;
        slide = data.slide;

        Initiate.Fade(data.level, Color.black, 1.0f);

        SceneManager.sceneLoaded += playerStats;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= playerStats;
        SceneManager.sceneLoaded -= checkpointReset;
        SceneManager.sceneLoaded -= setMapStats;

    }
}
