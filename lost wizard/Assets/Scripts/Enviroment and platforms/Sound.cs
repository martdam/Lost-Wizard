using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    private static Sound instance;

    AudioSource aud1;

    private void Start()
    {
        aud1 = GetComponent<AudioSource>();
        if(!GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>().isPlaying){
            GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>().Play();
        }
        aud1.Play();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void changeScene() {
        
            Destroy(gameObject);
        
    }
}
