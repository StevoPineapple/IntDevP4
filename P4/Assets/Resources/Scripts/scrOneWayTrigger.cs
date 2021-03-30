using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOneWayTrigger : MonoBehaviour
{
    public GameObject wall;

    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "objPlayer")
            wall.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "objPlayer")
            wall.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    void Update()
    {

    }
}
