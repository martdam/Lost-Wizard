using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public AudioClip Click;
    public AudioClip OpenMenu;
    public GameObject OptionPanel;
    public GameObject MapPanel;
    public GameObject MenuPanel;
    public Button ContinueButton = null;
    public bool buttons = true;

    public Color loadToColor = Color.white;

    private void Start()
    {
        if(ContinueButton != null)
        {
            ContinueButton.interactable = SaveSystem.checkData();   

        } 
    }


    public void deletData()
    {

        SaveSystem.borrarFile();
    }

    public void OptionsPanel()
    {

        gameObject.GetComponent<AudioSource>().clip = OpenMenu;
        gameObject.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        OptionPanel.SetActive(true);
    }
    public void Return()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().pause(false);
        gameObject.GetComponent<AudioSource>().clip = OpenMenu;
        gameObject.GetComponent<AudioSource>().Play();

        Time.timeScale = 1;
        MapPanel.SetActive(false);
        OptionPanel.SetActive(false);
    }

    public void GoMainMenu() {

        Time.timeScale = 1;
        OptionPanel.SetActive(false);
        if (GameObject.FindGameObjectWithTag("BkgSound"))
        {
            GameObject.FindGameObjectWithTag("BkgSound").GetComponent<Sound>().changeScene();
        }
        Initiate.Fade("MainMenu", loadToColor, 1.0f);
        //SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame() {

        Application.Quit();
    
    }

    public void NewGame()
    {
        MenuPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().newGame();
    }

    public void LoadGame() {
        MenuPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().LoadGame();
    }

    public void MyMapPanel() {

        gameObject.GetComponent<AudioSource>().clip = OpenMenu;
        gameObject.GetComponent<AudioSource>().Play();
        MapPanel.SetActive(!MapPanel.activeSelf);
    
    }
    private void Update()
    {
        if (buttons)
        {
            if (MapPanel.activeSelf && Input.GetKeyUp(KeyCode.LeftControl)|| (OptionPanel.activeSelf || MapPanel.activeSelf) && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.M)))
            {
                Return();
            }
            else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !MapPanel.activeSelf)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().pause(true);
                OptionsPanel();
            }
            else if (!OptionPanel.activeSelf && Input.GetKeyDown(KeyCode.M))
            {
                MyMapPanel();
            }
        }
    }
}
