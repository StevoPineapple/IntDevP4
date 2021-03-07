using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrChangeCam : MonoBehaviour
{
    public bool toZoom;
    public bool toFull;
    public GameObject toWorld;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "objPlayer")
        {
            CameraFollow mainCam = Camera.main.GetComponent<CameraFollow>();
            if(toZoom)
                mainCam.startChanging("Zoom",toWorld.name);
            else
                mainCam.startChanging("Full",toWorld.name);
            GameObject.Destroy(gameObject);
        }
    }
}
