using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region vars
    public Vector3 mouseClampPos;
    public Transform followTransform;
    BoxCollider2D worldBounds;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float camX;
    float camY;
    float camXFull;
    float camYFull;
    float camWidthZoom;
    float camWidthFull; //this is half of the entire width
    float camSizeFull;
    float camSizeZoom = 8.5f;
    float camChangeSize = 9999.0f;
    float camChangeSpeed = 40.0f;
    Vector3 smoothPos;
    float smoothRate = 0.6f;
    Camera mainCam;

    public string worldName;

    public string camMode;
    public bool isChanging;

    Vector3 mousePos;
    float mouseOffsetRadius = 0.75f;
    float mouseOffsetRate = 4.5f;
    SpriteRenderer fogRend;
    # endregion
    void Start()
    {
        fogRend = GameObject.Find("objFog").GetComponent<SpriteRenderer>(); //not universal
        isChanging = false;
        camXFull = gameObject.transform.position.x;
        camYFull = gameObject.transform.position.y;
        followTransform = GameObject.Find("objPlayer").transform;
        worldName = "objWorldOutside";
        worldBounds = GameObject.Find(worldName).GetComponent<BoxCollider2D>();
        camMode = "Full";
        xMin = worldBounds.bounds.min.x;
        yMin = worldBounds.bounds.min.y;
        xMax = worldBounds.bounds.max.x;
        yMax = worldBounds.bounds.max.y;
        mainCam = gameObject.GetComponent<Camera>();
        //isChanging = true;
        camSizeFull = mainCam.orthographicSize;
        camWidthZoom = mainCam.aspect*camSizeZoom;
        camWidthFull = mainCam.aspect*camSizeFull;
    }

    public void startChanging(string mode, string newWorld)
    {
        //Debug.Log("camSizeZoom");
        //Debug.Log(camSizeZoom);
        isChanging = true;
        camMode = mode;
        worldBounds = GameObject.Find(newWorld).GetComponent<BoxCollider2D>();
        worldName = newWorld;
                xMin = worldBounds.bounds.min.x;
        yMin = worldBounds.bounds.min.y;
        xMax = worldBounds.bounds.max.x;
        yMax = worldBounds.bounds.max.y;
    }
    
    void OnDrawGizmosSelected()
    {
        //Vector3 initialPos;
        // Draw a yellow sphere at the transform's position
        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //var allowedPos = mousePos - initialPos;
        //mousePos = Vector3.ClampMagnitude(mousePos, mouseOffsetRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(-Vector3.ClampMagnitude(followTransform.position-mainCam.ScreenToWorldPoint(Input.mousePosition),mouseOffsetRadius),0.3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(mainCam.ScreenToWorldPoint(Input.mousePosition),0.3f);

    }

    void FixedUpdate()
    {
        //Debug.Log(camSizeZoom);
        //Debug.Log("camSizeZoom");
        //Debug.Log(camSizeZoom);
        if(camMode == "Zoom")
        { 
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = -Vector3.ClampMagnitude(followTransform.position-mousePos,mouseOffsetRadius);
            camY = Mathf.Clamp(followTransform.position.y,yMin+camSizeZoom,yMax-camSizeZoom);
            camX = Mathf.Clamp(followTransform.position.x,xMin+camWidthZoom,xMax-camWidthZoom);
            mouseClampPos = new Vector3(camX+mousePos.x*mouseOffsetRate,
                    camY+mousePos.y*mouseOffsetRate,
                    gameObject.transform.position.z);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,mouseClampPos,smoothRate);
        }
        else
        {
            camY = Mathf.Clamp(followTransform.position.y,yMin+camSizeFull,yMax-camSizeFull);
            camX = Mathf.Clamp(followTransform.position.x,xMin+camWidthFull,xMax-camWidthFull);
        }
        if(isChanging)
        {
            //camSizeZoom = followTransform.position.y;
            //mainCam.orthographicSize = camSizeZoom;
            //camRatio = (xMax+camSizeFull)/800000000.0f;
            switch(camMode){
                case "Zoom": //TO ZOOM
                { 
                    if(camChangeSize == 9999.0f)//calc size
                    {
                        camChangeSize = (mainCam.orthographicSize-camSizeZoom)/camChangeSpeed;
                    }
                    if(mainCam.orthographicSize>camSizeZoom)//Changing
                    {
                        mainCam.orthographicSize-=camChangeSize;
                        fogRend.color = new Color(fogRend.color.r,fogRend.color.g,fogRend.color.b,
                        fogRend.color.a+1.0f/camChangeSpeed);
                    }
                    else //Finished
                    {
                        isChanging = false;
                        camChangeSize = 9999.0f;
                        mainCam.orthographicSize = camSizeZoom;
                        fogRend.color = new Color(fogRend.color.r,fogRend.color.g,fogRend.color.b,1.0f);
                    }
                    break;
                }
                case "Full":
                {
                    if(camChangeSize == 9999.0f)//ZOOM TO FULL this is not efficient FIRST TIME
                    {
                        camSizeFull = Mathf.Abs((yMax-yMin)/2);
                        camWidthFull = mainCam.aspect*camSizeFull;
                        camChangeSize = (camSizeFull-mainCam.orthographicSize)/camChangeSpeed;
                        camXFull = worldBounds.gameObject.transform.position.x;
                        camYFull = worldBounds.gameObject.transform.position.y;
                    }
                    if(mainCam.orthographicSize<camSizeFull)
                    {
                        mainCam.orthographicSize+=camChangeSize;
                        camWidthFull = mainCam.aspect*mainCam.orthographicSize;
                        camY = Mathf.Clamp(followTransform.position.y,yMin+mainCam.orthographicSize,yMax-mainCam.orthographicSize);
                        camX = Mathf.Clamp(followTransform.position.x,xMin+camWidthFull,xMax-camWidthFull);
                        smoothPos = Vector3.Lerp(gameObject.transform.position,new Vector3(camX,camY,gameObject.transform.position.z),smoothRate);
                        gameObject.transform.position = smoothPos;
                        fogRend.color = new Color(fogRend.color.r,fogRend.color.g,fogRend.color.b,
                        fogRend.color.a-1.0f/camChangeSpeed);
                    }
                    else
                    {
                        isChanging = false;
                        //gameObject.transform.position = new Vector3(camXFull,camYFull,gameObject.transform.position.z);
                        camChangeSize = 9999.0f;
                        mainCam.orthographicSize = camSizeFull;
                        fogRend.color = new Color(fogRend.color.r,fogRend.color.g,fogRend.color.b,0.0f);
                    }
                }
                break;
            }
        }
    }
}
