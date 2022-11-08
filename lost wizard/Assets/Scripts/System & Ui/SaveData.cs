using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    public int jumps;
    public int Dashes;
    public bool slide;
    public string level;

    public SaveData(PlayerMovement player, string scene) {

        jumps = player.extraJumpValue;
        Dashes = player.MaxDashValue;
        slide = player.canSlide;
        level = scene;
        
    
    }
}
