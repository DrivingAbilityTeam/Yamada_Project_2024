using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{

    LineRenderer linerenderer;

    Vector3[] positions;

    int positions_number;

    bool hazardon_past = false;

    public float border;

   
    bool curved = false;



    GameObject obstacle;

     [SerializeField] private AnimationCurve[] animation_curves;


     Vector3 start_position;

    // Start is called before the first frame update
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();

         positions_number = linerenderer.positionCount;

         positions = new Vector3[positions_number];

        int cnt = linerenderer.GetPositions(positions);

        start_position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
       if(obstacle != null)
       {
            //float obstacle_distance = Vector3.Distance(start_position, obstacle.transform.position);

            float obstacle_distance = Mathf.Abs(start_position.z -  obstacle.transform.position.z);

            

             if(obstacle_distance < border && !curved){

                curved = true;

                if(obstacle.transform.position.z < start_position.z)
                {
                    for(int i=0; i < positions_number; i++)
                    {
                    
                        positions[i].z = animation_curves[0].Evaluate( i /100f) * 10 + positions[i].z;


                    }
                }else
                {
                    for(int i=0; i < positions_number; i++)
                    {
                    

                        positions[i].z = (-1f * animation_curves[0].Evaluate( i /100f) * 10) + positions[i].z;


                    }
                }
                linerenderer.SetPositions(positions);
             }
       }


       if(hazardon_past == false && SignController.hazardon == true){

            FindObstacle();

        }

        hazardon_past = SignController.hazardon;
    }

    private void FindObstacle()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Hazard");

        float dis;
        float disc = 0;

        foreach (GameObject obs in obstacles)
        {
            dis = Vector3.Distance(this.transform.position, obs.transform.position);

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
