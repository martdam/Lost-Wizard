using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
    }

    public void ShakeRoom() {
        anim.SetTrigger("IsMoving");
    }

    public void ShakeLand() {
        anim.SetTrigger("Land");
    }

}
