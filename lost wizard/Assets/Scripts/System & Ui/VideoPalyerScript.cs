using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPalyerScript : MonoBehaviour
{
    public VideoPlayer video;
    public string escenaSiguiente = "level1";
    // Start is called before the first frame update
    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += nextScene;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void nextScene(VideoPlayer vp) {
        Initiate.Fade(escenaSiguiente, Color.black, 1.0f);
    }
}
