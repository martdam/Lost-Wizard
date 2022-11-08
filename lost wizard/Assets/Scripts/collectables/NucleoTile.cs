using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleoTile : Collectable
{
    // Start is called before the first frame update
    [SerializeField] private int MyTimmy=190;

    private void Start()
    {
        if (MyTimmy==190) {
            MyTimmy = transform.parent.transform.Find("Tile").GetComponent<MniniMapTale>().get_id();
        }
        //transform.parent.DetachChildren();
    }
    public int get_MyTimmy() { return MyTimmy; }

    public void Destroy()
    {
        
        Destroy(gameObject);
    }

    // Update is called once per frame

}
