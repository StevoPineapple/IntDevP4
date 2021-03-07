using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrKey : MonoBehaviour
{
public GameObject door;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "objPlayer")
        {
            Debug.Log(".");
            GameObject.Destroy(door);
            GameObject.Destroy(gameObject);
        }
    }
}
