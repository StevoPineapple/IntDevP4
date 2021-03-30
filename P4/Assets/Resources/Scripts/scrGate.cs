using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGate : MonoBehaviour
{
    public int dNumber;

    float scrollSpeed = 0.001f;
    float originalSize = 4f;
    float halfsize; // This controls scrollspeed
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer> ();
        halfsize = originalSize/2f;
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
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");
        if(other.gameObject.name == "objPlayer")
        {
            if(other.gameObject.GetComponent<scrPlayer>().numOfDiamond == dNumber)
            {
                foreach(GameObject obj in GameObject.FindGameObjectsWithTag("following"))
                    GameObject.Destroy(obj);
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
