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
    private Vector3 prevMousePos;
    private Camera mainCam;
    private float moveSpd = 9.0f; //Add soft speed 
    public float deltaAngle;
    public int numOfDiamond;
    public bool lockMovement = false;
    float moveSmoothRate = 0.3f;
    float turnSmoothRate = 0.7f;
    float scrollSpeed = 0.35f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
        selfBody = gameObject.GetComponent<Rigidbody2D>();
        selfPos = gameObject.transform.position;
        sprRenderer = gameObject.GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
        currVelocity = selfBody.velocity;
    }

    void keyMove()
    {
        //float adjAngle;
        selfPos = gameObject.transform.position;
        prevMousePos = mousePos;
        mousePos.x = Mathf.Lerp(mousePos.x,mainCam.ScreenToWorldPoint(Input.mousePosition).x,turnSmoothRate); //Glith
        mousePos.y = Mathf.Lerp(mousePos.y,mainCam.ScreenToWorldPoint(Input.mousePosition).y,turnSmoothRate);
        /*adjAngle = getAngle(selfPos.y-mousePos.y,selfPos.x-mousePos.x);
        Debug.Log(adjAngle);
        if(adjAngle<0){
            adjAngle=-adjAngle;
            adjAngle=180-adjAngle;
            adjAngle=adjAngle+180;
        }*/
        selfAngle = getAngle(selfPos.y-mousePos.y,selfPos.x-mousePos.x);//Mathf.Lerp(selfAngle,adjAngle,turnSmoothRate);
        transform.rotation = Quaternion.Euler(0,0,selfAngle);  
        if(!lockMovement)
        {
            if(Input.GetKey(KeyCode.W))
                currVelocity.y = 1.0f*moveSpd;
            else if(Input.GetKey(KeyCode.S))
                currVelocity.y = -1.0f*moveSpd;
            if(Input.GetKey(KeyCode.A))
                currVelocity.x = -1.0f*moveSpd;
            else if(Input.GetKey(KeyCode.D))
                currVelocity.x = 1.0f*moveSpd;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.eulerAngles);
    }

    public float getAngle(float oppLen, float adjLen)
    {
        return(Mathf.Rad2Deg*Mathf.Atan2(oppLen,adjLen));
    }

    void HandleMovement()
    {
        selfBody.velocity = Vector3.Lerp(selfBody.velocity,currVelocity,moveSmoothRate);
        currVelocity = new Vector3(0,0,0);
    }
    void FixedUpdate()
    {
        keyMove();
        //Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.max.x);
        //Debug.Log();
        //rend.material.SetVector("_Tiling", new Vector4(
        //    gameObject.transform.localScale.x/originalSize,
        //    gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            (gameObject.transform.position.x-gameObject.GetComponent<CircleCollider2D>().radius % 1.5f) / 1.5f * 0.5f,
            (gameObject.transform.position.y-gameObject.GetComponent<CircleCollider2D>().radius % 1.5f) / 1.5f * 0.5f,
            0,0));
        rend.material.SetFloat("_Rotate", -selfAngle);
        float offset = Time.time * scrollSpeed;
        rend.material.SetFloat("_Speed", offset);
        
        HandleMovement();
    }
}
