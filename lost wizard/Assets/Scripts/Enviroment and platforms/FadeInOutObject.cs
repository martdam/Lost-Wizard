using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeInOutObject : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer sprRend = null;
    Material matRend;


    public bool fading = false;
    public bool unFading = false;
    void Start()
    {
        if(sprRend != null) {
            matRend = sprRend.material;
            Color c = matRend.color;
            c.a = 0;
            matRend.color = c;
        }
        else if( gameObject.GetComponent<TilemapRenderer>())
        {
            matRend = gameObject.GetComponent<TilemapRenderer>().material;
        }
        else if(gameObject.GetComponent<SpriteRenderer>()) {

            matRend = gameObject.GetComponent<SpriteRenderer>().material;

        }
       
      
        
    }

    // Update is called once per frame

    IEnumerator FadeIn() {

        fading = true;
        for (float f = 0.05f; f <= 1; f += 0.05f) {
            Color c = matRend.color;
            c.a = f;
            matRend.color = c;
            
            yield return new WaitForSeconds(0.01f);
        }
        fading = false;
    }

    IEnumerator FadeOut()
    {
        unFading = true;
        for (float f = 1f; f > -0.05f; f -= 0.05f)
        {
            Color c = matRend.color;
            c.a = f;
            matRend.color = c;
            
            yield return new WaitForSeconds(0.01f);
        }
        unFading = false;
    }

    public void startFadingIn() {
        if (!fading)
        {
            StartCoroutine("FadeIn");
        }
    }
    public void startFadingOut()
    {
        if (!unFading)
        {
            StartCoroutine("FadeOut");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (sprRend != null)
            {
                startFadingIn();
            }
            else {
                startFadingOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (sprRend != null)
            {
                startFadingOut();
            }
            else
            {
                startFadingIn();
            }
        }

    }

}
