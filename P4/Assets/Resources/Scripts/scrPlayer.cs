using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayer : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    private Vector3 selfPos;
    private Rigidbody2D selfBody;
    private Vector3 currVelocity;
    private float selfAngle;
    private Vector3 mousePos;
    private Camera mainCam;
    private float moveSpd = 3.0f;
    public float deltaAngle;

    void Start()
    {
        selfBody = gameObject.GetComponent<Rigidbody2D>();
        selfPos = gameObject.transform.position;
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
        currVelocity = selfBody.velocity;
    }

    void keyMove()
    {
        selfPos = gameObject.transform.position;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        selfAngle = getAngle(selfPos.y-mousePos.y,selfPos.x-mousePos.x);
        transform.rotation = Quaternion.Euler(0,0,selfAngle);  

        if(Input.GetKey(KeyCode.W))
            currVelocity.y = 1.0f*moveSpd;
        else if(Input.GetKey(KeyCode.S))
            currVelocity.y = -1.0f*moveSpd;
        if(Input.GetKey(KeyCode.A))
            currVelocity.x = -1.0f*moveSpd;
        else if(Input.GetKey(KeyCode.D))
            currVelocity.x = 1.0f*moveSpd;
    }

    public float getAngle(float oppLen, float adjLen)
    {
        return(Mathf.Rad2Deg*Mathf.Atan2(oppLen,adjLen));
    }

    void HandleMovement()
    {
        selfBody.velocity = currVelocity;
        currVelocity = new Vector3(0,0,0);
    }
    void FixedUpdate()
    {
        keyMove();
        HandleMovement();
    }
}
