using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDiamond : MonoBehaviour
{
    Vector3 initPos;
    public bool following = false;
    public bool returning = false;
    GameObject followObj;
    float smoothRate = 0.03f;
    void Start()
    {
        initPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if(following)
        {
            if(returning){
                if(Mathf.Abs(gameObject.transform.position.x - initPos.x)<=0.01)
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
            followObj.gameObject.transform.position.y-1,
            followObj.gameObject.transform.position.z-1),
            smoothRate);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!following)
        {
            if(other.name =="objPlayer")
            {
                Debug.Log("a");
                following = true;
                followObj = other.gameObject;
                followObj.GetComponent<scrPlayer>().numOfDiamond+=1;
                gameObject.tag = "following";
            }
        }
    }
}
