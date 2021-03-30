using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrChangeCam : MonoBehaviour
{
    public bool toZoom;
    public bool toFull;
    public GameObject toWorld;
    //bool stepped = false;
    GameObject objPlayer;
    //float reactivateDis = 0.0f;
    void Start()
    {
        //objPlayer
    }
    void FixedUpdate()
    {
        
        //if(Mathf.Abs(objPlayer.transform.position.x-gameObject.transform.position.x)>reactivateDis||
        //Mathf.Abs(objPlayer.transform.position.y-gameObject.transform.position.y)>reactivateDis)
        //{
        //    stepped = false;
        //}
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //if(!stepped)
        //{
            if(other.gameObject.name == "objPlayer")
            {
                objPlayer = other.gameObject;
                CameraFollow mainCam = Camera.main.GetComponent<CameraFollow>();
                if(toZoom)
                    mainCam.startChanging("Zoom",toWorld.name);
                else
                    mainCam.startChanging("Full",toWorld.name);
                //stepped = true;
            }
        //}
    }
}
