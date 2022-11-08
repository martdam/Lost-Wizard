using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Collectable
{
    // Start is called before the first frame update
    public bool wallup;
    public bool jumpUp;
    public bool dashUp;
    [SerializeField]private float turnSpeed = 120;
    private void LateUpdate()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
    }
}
