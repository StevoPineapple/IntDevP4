using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEyeSwitch : MonoBehaviour
{
    float scrollSpeed = 0.0005f;
    float minScrollSpeed = 0.0005f;
    float maxScrollSpeed = 0.03f;
    float timeToMaxScroll = 150;
    float onToOffRate = 4; //takes 1 on, 3 off
    bool looking;
    public GameObject eyeWall;
    float inverse = 0;
    float originalSize = 2f;
    float halfsize;

    Renderer rend;
    void Start()
    {
        halfsize = originalSize/2;
        //Debug.Log(gameObject.GetComponent<BoxCollider2D>().bounds.max.x);
        //Debug.Log();
        rend = GetComponent<Renderer> ();
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            gameObject.transform.position.x % halfsize / halfsize * 0.5f,
            gameObject.transform.position.y % halfsize / halfsize * 0.5f,
            0,0));
            
            //gameObject.GetComponent<BoxCollider2D>().bounds.min.x % 1.5f / 1.5f * 0.5f,
            //gameObject.GetComponent<BoxCollider2D>().bounds.min.y % 1.5f / 1.5f * 0.5f,
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        //rend.material.mainTextureScale = new Vector2(gameObject.transform.localScale.x/originalSize,gameObject.transform.localScale.y/originalSize);
        //rend.material.SetTextureOffset(maintexnew Vector2(gameObject.GetComponent<BoxCollider2D>().bounds.max.x % 2 / 2 * -0.5f,
        //gameObject.GetComponent<BoxCollider2D>().bounds.max.y % 2 / 2 * -0.5f); Don't delete, code for mesh method
    }

    public void MakingEyeContact()
    {
        looking = true;
        if(scrollSpeed<maxScrollSpeed)
            scrollSpeed+=(maxScrollSpeed-minScrollSpeed)/timeToMaxScroll;
        else if(scrollSpeed>maxScrollSpeed)
            scrollSpeed = maxScrollSpeed;
        inverse = scrollSpeed/maxScrollSpeed;
    }
    void Update()
    {
        if(!looking)
        {
            if(scrollSpeed>minScrollSpeed)
            {
                scrollSpeed-=(maxScrollSpeed-minScrollSpeed)/timeToMaxScroll/onToOffRate;
                inverse = scrollSpeed/maxScrollSpeed;
            }
            else
            {
                scrollSpeed=minScrollSpeed;
                inverse = 0;
            }
        }
        if(inverse > 0)
            eyeWall.GetComponent<BoxCollider2D>().isTrigger = true;
        else   
            eyeWall.GetComponent<BoxCollider2D>().isTrigger = false;
        eyeWall.GetComponent<scrEyeWall>().setInvScrSpd(inverse,scrollSpeed);
        looking = false;

        //Transform[] selfTrans = gameObject.transform.GetChild();
        if(gameObject.tag != "debug")
        {
            //Debug.Log(-gameObject.transform.localEulerAngles.z);
            
        //Debug.Log("Z:"+gameObject.GetComponent<BoxCollider2D>().bounds.min.x);
        
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            gameObject.transform.position.x-gameObject.GetComponent<CircleCollider2D>().radius-Camera.main.transform.position.x % halfsize / halfsize * 0.5f,
            gameObject.transform.position.y-gameObject.GetComponent<CircleCollider2D>().radius-Camera.main.transform.position.y % halfsize / halfsize * 0.5f,
            0,0));
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        rend.material.SetFloat("_Inverse", inverse);
        }
        rend.material.SetFloat("_Speed",rend.material.GetFloat("_Speed")+scrollSpeed);
    }
}
