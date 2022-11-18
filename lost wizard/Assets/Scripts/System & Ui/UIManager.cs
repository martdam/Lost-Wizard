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

    List<CanvasRenderer> a = new List<CanvasRenderer>();

    public Color loadToColor = Color.white;

    private void Start()
    {
        if(ContinueButton != null)
        {
            ContinueButton.interactable = SaveSystem.checkData();   

        }

        if (MenuPanel.activeSelf)
        {
            a.Add(MenuPanel.GetComponent<CanvasRenderer>());
            // Debug.Log(SkipCanvas.transform.childCount);
            if (MenuPanel.transform.childCount > 0)
            {
                for (int i = 0; i < MenuPanel.transform.childCount; i++)
                {
                    //Debug.Log(i);

                    a.Add(MenuPanel.transform.GetChild(i).gameObject.GetComponent<CanvasRenderer>());
                }
            }
            StartCoroutine("FadeIn");
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
        StartCoroutine("FadeOut");
        GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>().newGame();
    }

    public void LoadGame() {
        StartCoroutine("FadeOut");
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

    IEnumerator FadeOut()
    {
        for (float f = a[0].GetAlpha(); f > -0.05f; f -= 0.05f)
        {
            foreach (CanvasRenderer cvr in a)
            {
                cvr.SetAlpha(f);
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f < 1; f += 0.05f)
        {
            foreach (CanvasRenderer cvr in a)
            {
                cvr.SetAlpha(f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

}
