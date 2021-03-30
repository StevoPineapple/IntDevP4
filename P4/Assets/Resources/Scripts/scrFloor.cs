﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFloor : MonoBehaviour
{
    float scrollSpeed = 0.02f;
    public float originalSize;
    public float halfsize; // This controls scrollspeed

    Renderer rend;
    void Start()
    {
        halfsize = originalSize/2f;
        //Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.max.x);
        //Debug.Log();
        rend = GetComponent<Renderer> ();
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            gameObject.transform.position.x % halfsize / halfsize,
            gameObject.transform.position.y % halfsize / halfsize,
            0,0));
            
            //gameObject.GetComponent<BoxCollider2D>().bounds.min.x % 1.5f / 1.5f * 0.5f,
            //gameObject.GetComponent<BoxCollider2D>().bounds.min.y % 1.5f / 1.5f * 0.5f,
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        //rend.material.mainTextureScale = new Vector2(gameObject.transform.localScale.x/originalSize,gameObject.transform.localScale.y/originalSize);
        //rend.material.SetTextureOffset(maintexnew Vector2(gameObject.GetComponent<BoxCollider2D>().bounds.max.x % 2 / 2 * -0.5f,
        //gameObject.GetComponent<BoxCollider2D>().bounds.max.y % 2 / 2 * -0.5f); Don't delete, code for mesh method
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position,0.2f);
    }
    void Update()
    {
//Transform[] selfTrans = gameObject.transform.GetChild();
        if(gameObject.tag != "debug")
        {
            //Debug.Log(-gameObject.transform.localEulerAngles.z);
            
        //Debug.Log("Z:"+gameObject.GetComponent<BoxCollider2D>().bounds.min.x);
        
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            (gameObject.transform.position.x-Camera.main.transform.position.x) % halfsize / halfsize * 0.5f,
            (gameObject.transform.position.y-Camera.main.transform.position.y) % halfsize / halfsize * 0.5f,
            0,0));
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        }
        else{
            float offset = Time.time * scrollSpeed;
            rend.material.SetFloat("_Speed", offset);}
    }
}
