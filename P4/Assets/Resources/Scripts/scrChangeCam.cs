using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrChangeCam : MonoBehaviour
{
    public bool toZoom;
    public bool toFull;
    public GameObject toWorld;
    bool stepped = false;
    GameObject objPlayer;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(objPlayer.transform.position.x-gameObject.transform.position.x)>5.0f||
        Mathf.Abs(objPlayer.transform.position.y-gameObject.transform.position.y)>5.0f)
        {
            stepped = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!stepped)
        {
            if(other.gameObject.name == "objPlayer")
            {
                objPlayer = other.gameObject;
                CameraFollow mainCam = Camera.main.GetComponent<CameraFollow>();
                if(toZoom)
                    mainCam.startChanging("Zoom",toWorld.name);
                else
                    mainCam.startChanging("Full",toWorld.name);
                stepped = true;
            }
        }
    }
}
