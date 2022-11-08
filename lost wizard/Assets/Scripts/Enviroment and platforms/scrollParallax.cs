using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollParallax : MonoBehaviour
{

    [SerializeField] private Vector2 vel_Movimiento;

    [SerializeField] private Vector2 offset;
    [SerializeField] private float multiplier;
    [SerializeField] private float dist, distX;

    private Material material;

    private Rigidbody2D jugador_rb2d;
    private PlayerMovement jugador_mov;

    public Transform camTransform;

    public Transform limitIzquierdo;

    public Transform limitDerecho;

    private float InitPosY;

    public bool followTransform =true;

    // Start is called before the first frame update
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugador_rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        jugador_mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        InitPosY = transform.position.y;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        offset = (jugador_rb2d.velocity.x * multiplier) * vel_Movimiento * Time.deltaTime;
        if (jugador_rb2d.constraints != RigidbodyConstraints2D.FreezeAll && !jugador_mov.isTouchingFront ) {
            material.mainTextureOffset += offset;
        }
        if (followTransform)
        {
            if (camTransform.position.y+ dist*10 <= InitPosY  )
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, camTransform.position.y + dist*10, transform.position.z), transform.rotation);
            } else  {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, InitPosY + dist, transform.position.z), transform.rotation);
            }

            if (camTransform.position.x > limitIzquierdo.position.x && camTransform.position.x< limitDerecho.position.x) {
                transform.SetPositionAndRotation(new Vector3(camTransform.position.x + distX, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
}
