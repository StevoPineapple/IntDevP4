using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWallManager : MonoBehaviour
{
    Renderer Rend;
    int countWalls = 0;
    GameObject[] walls;
    GameObject[] wallSs;
    GameObject[] wallRSs;
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Walls");
        wallSs = GameObject.FindGameObjectsWithTag("WallsS");
        wallRSs = GameObject.FindGameObjectsWithTag("WallsRS");
    }
    void Update() //Optimz for slanted and maybe rec not working
    {
       
        //Debug.Log(GameObject.FindGameObjectsWithTag("Walls").Length);
        foreach(GameObject obj in walls)
        {
            //obj.GetComponent<scrWall>().enabled = false;
            Rend = obj.GetComponent<Renderer>();
            if(!Rend.isVisible && obj.GetComponent<scrWall>().enabled)
            {
                obj.GetComponent<scrWall>().enabled = false;
            }
            else// if(Rend.isVisible && !obj.GetComponent<scrWall>().enabled)
            {
                //Debug.Log(obj.name + "   :WALL");
                obj.GetComponent<scrWall>().enabled = true;
            }
        }
        foreach(GameObject obj in wallSs)
        {
            //obj.GetComponent<scrWall>().enabled = false;

            Rend = obj.GetComponent<Renderer>();
            if(!Rend.isVisible && obj.GetComponent<scrWallSlanted>().enabled)
            {
                
                obj.GetComponent<scrWallSlanted>().enabled = false;
            }
            else if(Rend.isVisible && !obj.GetComponent<scrWallSlanted>().enabled)
            {
                countWalls+=1;
                obj.GetComponent<scrWallSlanted>().enabled = true;
            }
        }
        /*foreach(GameObject obj in wallRSs) No Rendere on parent
        {
            //obj.GetComponent<scrWall>().enabled = false;
            Rend = obj.GetComponent<Renderer>();
            if(!Rend.isVisible && obj.GetComponent<scrWallRecSlanted>().enabled)
            {
                
                obj.GetComponent<scrWallRecSlanted>().enabled = false;
            }
            else if(Rend.isVisible && !obj.GetComponent<scrWallRecSlanted>().enabled)
            {
                countWalls+=1;
                obj.GetComponent<scrWallRecSlanted>().enabled = true;
            }
        }*/
    }
}
