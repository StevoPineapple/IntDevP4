using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGate : MonoBehaviour
{
    public int dNumber;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        if(other.gameObject.name == "objPlayer")
        {
            if(other.gameObject.GetComponent<scrPlayer>().numOfDiamond == dNumber)
            {
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("following"))
                    obj.GetComponent<scrDiamond>().returning = true;
                other.gameObject.GetComponent<scrPlayer>().numOfDiamond = 0;
                GameObject.Destroy(gameObject);
            }
            else
            {
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("following"))
                {
                    obj.GetComponent<scrDiamond>().returning = true;
                }
                other.gameObject.GetComponent<scrPlayer>().numOfDiamond = 0;
            }
        }
    }
}
