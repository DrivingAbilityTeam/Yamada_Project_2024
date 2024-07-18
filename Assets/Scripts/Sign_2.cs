using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_2 : MonoBehaviour
{
    private Sign_Basic sign_basic;

    private Image image;

    private GameObject car;

    private GameObject obstacle;

    

    private float dis;

    private float disc;

    private int count =0;

    public float border1;

    public float border2;

    

    private float dis_now;



    // Start is called before the first frame update
    void Start()
    {
        sign_basic = GetComponent<Sign_Basic>();
        image = GetComponent<Image>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //image.color = new Color32(242, 108, 216, 255);

        if(car == null)
        {
            car = SignController.player_car;
        }

        if (car != null && obstacle != null)
        {
            dis_now = Vector3.Distance(car.transform.position, obstacle.transform.position);
            //Debug.Log("Distance is "+dis_now);
        }

        if(obstacle == null  && dis == 0 && car != null && count == 0)
        {
            count++;
            FindObstacle();
        }

        if(dis_now < border2 && dis_now != 0)
        {
            image.color = new Color32(255, 0, 0, 255);
        }
        else if (dis_now < border1 && dis_now != 0)
        {
            image.color = new Color32(255, 130, 0, 255);
        }
        else
        {
            image.color = new Color32(255, 255, 0, 255);
        }

        //Debug.Log("Distance is "+obstacle.transform.position);
    }






    private void FindObstacle()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Hazard");

        foreach (GameObject obs in obstacles)
        {
            dis = Vector3.Distance(car.transform.position, obs.transform.position);

            if (disc == 0)
            {
                disc = dis;
                obstacle = obs;
            }
            else if (dis < disc)
            {
                disc = dis;
                obstacle = obs;
            }

        }
    }
}
