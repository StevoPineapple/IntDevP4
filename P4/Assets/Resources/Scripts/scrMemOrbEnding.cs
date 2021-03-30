using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scrMemOrbEnding : MonoBehaviour
{
    float scrollSpeed = 0.0005f;
    //float minScrollSpeed = 0.0005f;
    //float maxScrollSpeed = 0.03f;
    //float timeToMaxScroll = 150;
    //float onToOffRate = 4; //takes 1 on, 3 off
    float maxSize = 50;
    float originalRaidus;
    float growthRate = 1.03f;
    float shrinkRate = 0.985f;
    //bool looking;
    public GameObject objPlayer;
    //public GameObject memWall;
    //public GameObject teleObj;
    public TextMeshProUGUI textObj;
    //private TextMeshProUGUI textObj;
    public string[] textArr = new string[3];
    //public float[] textTimeArr = new float[2];
    bool playerGet;
    bool endStart;
    float originalSize = 2f;
    float halfsize;
    int count = 0;
    float textAlpha = 0;
    Renderer rend;
    void Awake()
    {
        textObj.text = textArr[0];
    }
    void Start()
    {
        //textObj = textGObj.GetComponent<TextMeshProUGUI>();
        textObj.color = new Color(0,0,0,0);
        textObj.text = textArr[0];
        originalRaidus = GetComponent<CircleCollider2D>().radius;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name =="objPlayer")
        {
            objPlayer.GetComponent<scrPlayer>().lockMovement = true;
            if(!playerGet){
                playerGet = true;
            }
        }
    }
    void FixedUpdate()
    {          
        if(playerGet)
        {
            if(transform.localScale.x<maxSize)
            {
                transform.localScale*=growthRate;
                    //transform.position= new Vector3(transform.position.x-GetComponent<CircleCollider2D>().radius/2,transform.position.y-GetComponent<CircleCollider2D>().radius/2,transform.position.z);
                    //originalSize *= shrinkRate;
            }
                else
                {
                    if(count > 200)
                    {
                        if(textAlpha<1)
                        {
                            textAlpha+=0.03f;
                            textObj.color = new Color(0,0,0,textAlpha);
                        }
                    }
                    count+=1;
                    
                    //objPlayer.transform.position = teleObj.transform.position;
                    //    transform.position = teleObj.transform.position;
                    //    Camera.main.transform.position = 
                    //        new Vector3(teleObj.transform.position.x,teleObj.transform.position.y,Camera.main.transform.position.z);
                    if(count == 500)
                    {
                        textObj.text = textArr[1];
                    }
                }
            }
    }
    void Update(){
            rend.material.SetVector("_Offset", new Vector4(
                gameObject.transform.position.x-gameObject.GetComponent<CircleCollider2D>().radius-Camera.main.transform.position.x % halfsize / halfsize * 0.5f,
                gameObject.transform.position.y-gameObject.GetComponent<CircleCollider2D>().radius-Camera.main.transform.position.y % halfsize / halfsize * 0.5f,
                0,0));
        rend.material.SetVector("_Tiling", new Vector4(
            gameObject.transform.localScale.x/originalSize,
            gameObject.transform.localScale.y/originalSize,0,0));
        
        rend.material.SetFloat("_Rotate", -gameObject.transform.localEulerAngles.z);
        rend.material.SetFloat("_Speed",rend.material.GetFloat("_Speed")+scrollSpeed);
    }
}
