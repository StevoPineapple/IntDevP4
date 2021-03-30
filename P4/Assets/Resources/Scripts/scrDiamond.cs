using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDiamond : MonoBehaviour
{
    Vector3 initPos;
    public bool following = false;
    public bool returning = false;
    GameObject followObj;
    float smoothRate = 0.1f;
    public GameObject holderCreate;

    float scrollSpeed = 0.0005f;
    float originalSize = 1.5f;
    float halfsize; // This controls scrollspeed
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
        halfsize = originalSize/2f;
        initPos = gameObject.transform.position;
        GameObject newHolder = Instantiate(holderCreate,new Vector3(initPos.x+0.75f,initPos.y,initPos.z),Quaternion.Euler(0,0,45));
    }

    void FixedUpdate()
    {
        if(following)
        {
            if(returning){
                if(Mathf.Abs(gameObject.transform.position.x - initPos.x)<=0.001)
                {
                    returning = false;
                    gameObject.tag = "Untagged";
                    following = false;
                }
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,initPos,smoothRate+0.1f);
            }
            else{
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
            new Vector3(followObj.gameObject.transform.position.x,
            followObj.gameObject.transform.position.y-2,
            followObj.gameObject.transform.position.z-1),
            smoothRate);
            }
        }
    }
    void Update()
    {
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        rend.material.SetVector("_Offset", new Vector4(
            (gameObject.transform.position.x-Camera.main.transform.position.x) % halfsize / halfsize * 0.5f,
            (gameObject.transform.position.y-Camera.main.transform.position.y) % halfsize / halfsize * 0.5f,
            0,0));
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        rend.material.SetFloat("_Speed",rend.material.GetFloat("_Speed")+scrollSpeed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!following)
        {
            if(other.name =="objPlayer")
            {
                following = true;
                followObj = other.gameObject;
                followObj.GetComponent<scrPlayer>().numOfDiamond+=1;
                gameObject.tag = "following";
            }
        }
    }
}
