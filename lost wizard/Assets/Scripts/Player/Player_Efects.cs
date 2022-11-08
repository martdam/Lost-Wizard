using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Efects : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip sound_step;
    public AudioClip sound_jump;
    public AudioClip sound_door;
    public AudioClip sound_hit;
    public AudioClip sound_die;
    public AudioClip sound_land;
    public AudioSource aud;

    public void play_die()
    {
        aud.pitch = 0.5f;
        aud.clip = sound_die;
        aud.Play();
    }
    public void play_door()
    {
        gameObject.transform.Find("Ground Check").GetComponent<AudioSource>().Play();
    }

    public void play_slide(bool state)
    {
        if (state) {
            if (!gameObject.transform.Find("Front check").GetComponent<AudioSource>().isPlaying)
            {
                gameObject.transform.Find("Front check").GetComponent<AudioSource>().Play();
            }
        } else {
            gameObject.transform.Find("Front check").GetComponent<AudioSource>().Play();
        }
    }
    public void play_step(bool state)
    {
        if (!aud.isPlaying &&state)
        {
            aud.volume = 0.06f;
            aud.pitch = 3;
            aud.loop = true;
            aud.clip = sound_step;
            aud.Play();
        } else if (!state) {
            aud.loop = false;
        }
    }
    public void play_land()
    {

        aud.volume = 0.7f;
        aud.pitch = 1;
        aud.clip = sound_land;
        aud.Play();
    }
    public void play_jump()
    {

        aud.volume = 0.7f;
        aud.pitch = 0.9f;
        aud.clip = sound_jump;
        aud.Play();
    }
    public void play_Wjump()
    {

        aud.volume = 0.7f;
        aud.clip = sound_jump;
        aud.Play();
        aud.pitch = 1f;
    }

    public void play_dash()
    {
        if (!gameObject.transform.Find("dashPoint").GetComponent<AudioSource>().isPlaying)
        {
            gameObject.transform.Find("dashPoint").GetComponent<AudioSource>().Play();
        }
    }
    public void play_hit()
    {

        aud.volume = 1f;
        aud.pitch = 1;
        aud.loop = false;
        aud.clip = sound_hit;
        aud.Play();
    }
}
