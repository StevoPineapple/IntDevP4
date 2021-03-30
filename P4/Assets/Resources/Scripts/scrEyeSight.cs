using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEyeSight : MonoBehaviour
{
    public float xx = -1;
    public float yy;
    public float zz;
    float angle;
    float sightDisPlus = 1.5f;
    RaycastHit2D[] eyeSights;
    int sightDensity = 5;
    float sightAngleDiff = 10.0f;
    public Vector3 dir;
    float mouseAngle;
    void Start()
    {
        eyeSights = new RaycastHit2D[sightDensity];
    }
    public float GetAngle(float oppLen, float adjLen)
    {
        return(Mathf.Rad2Deg*Mathf.Atan2(oppLen,adjLen));
    }
    void EyeContact()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseAngle = -GetAngle(transform.position.y-mousePos.y,transform.position.x-mousePos.x);
        angle = mouseAngle - Mathf.Floor(sightDensity/2)*sightAngleDiff;

        for(int i = 0;i<sightDensity;i++)
        {
            dir = Quaternion.AngleAxis(-angle, Vector3.forward) * new Vector3(xx,yy,zz);
            eyeSights[i] = Physics2D.Raycast(transform.position,
                dir,Camera.main.orthographicSize+sightDisPlus);
            Debug.DrawRay(transform.position,
                Vector3.Scale(dir,new Vector3(Camera.main.orthographicSize,Camera.main.orthographicSize+sightDisPlus,0)),
                Color.red);
            if(eyeSights[i].collider != null)
            {
                //Debug.Log(eyeSights[i].collider.name);
                if(eyeSights[i].collider.tag == "Eyes")
                {
                    eyeSights[i].collider.gameObject.GetComponent<scrEyeSwitch>().MakingEyeContact();
                    i=sightDensity;
                }
            }
            angle += sightAngleDiff;
        }
        angle = 0;
    }
    void Update()
    {
        EyeContact();
    }
}
