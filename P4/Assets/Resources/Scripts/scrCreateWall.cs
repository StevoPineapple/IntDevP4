using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCreateWall : MonoBehaviour
{
public GameObject wallPos;
public GameObject wallCreate;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "objPlayer")
        {
            GameObject newWall = Instantiate(wallCreate,wallPos.transform.position,wallPos.transform.rotation);
            newWall.transform.localScale = new Vector3(2,2,2);
            GameObject.Destroy(gameObject);
        }
    }
}
