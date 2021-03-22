using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFog : MonoBehaviour
{
    float camPosX;
    float camPosY;
    float camScale;
    void Start()
    {
        camScale = Camera.main.orthographicSize;
        gameObject.GetComponent<SpriteRenderer>().color = 
        new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
        gameObject.GetComponent<SpriteRenderer>().color.g,
        gameObject.GetComponent<SpriteRenderer>().color.b,
        0.0f);
    }

    void FixedUpdate()
    {
        camPosX = Camera.main.transform.position.x;
        camPosY = Camera.main.transform.position.y;
        gameObject.transform.position = new Vector3(camPosX,camPosY,gameObject.transform.position.z);
        float scale = Camera.main.orthographicSize/camScale;
        gameObject.transform.localScale = new Vector3(scale,scale,1.0f);
    }
}
