using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPalyerScript : MonoBehaviour
{
    public VideoPlayer video;
    public string escenaSiguiente = "level1";
    public GameObject SkipCanvas;
    List<CanvasRenderer> a = new List<CanvasRenderer>();
    // Start is called before the first frame update
    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        a.Add(SkipCanvas.GetComponent<CanvasRenderer>());
        //Debug.Log(SkipCanvas.transform.childCount);
        if (SkipCanvas.transform.childCount > 0)
        {
            for (int i = 0; i < SkipCanvas.transform.childCount; i++)
            {
                //Debug.Log(i);

                a.Add(SkipCanvas.transform.GetChild(i).gameObject.GetComponent<CanvasRenderer>());
            }
        }

        video.loopPointReached += nextScene;

    }
    // Update is called once per frame
    void Update()
    {
        if (SkipCanvas.activeSelf && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine("FadeOut");
            Initiate.Fade(escenaSiguiente, Color.black, 1.0f);
        }

        if (Input.anyKeyDown && !SkipCanvas.activeSelf) {
            SkipCanvas.SetActive(true);
            StartCoroutine("FadeIn");
            Invoke("ocultar", 4f);
        }

    }

    void ocultar() {
        SkipCanvas.SetActive(false);    
    }

    void nextScene(VideoPlayer vp) {
        Initiate.Fade(escenaSiguiente, Color.black, 1.0f);
    }
    IEnumerator FadeOut()
    {
        for (float f = a[0].GetAlpha(); f > -0.05f; f -= 0.05f)
        {
            foreach (CanvasRenderer cvr in a) {
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
