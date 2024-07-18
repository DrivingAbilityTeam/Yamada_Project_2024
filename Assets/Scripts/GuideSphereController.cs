using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSphereController : MonoBehaviour
{

    public GameObject guide_sphere;

    public Transform GS_start_R, GS_goal_R;

    public Transform GS_start_L, GS_goal_L;

     GameObject GS_leader;

    [Range(0, 1)]
	public float GS_interval;

    public enum EaseType2
    {
        pole,
        height,




    }
    public EaseType2 GS_type;

    public static int GS_code;

    // Start is called before the first frame update
    void Start()
    {
        if(GS_type.ToString() == "pole"){

            GS_code = 0;

        }else if(GS_type.ToString() == "height"){
            GS_code = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Guide_Sphere_On()
    {
       Vector3 GS_route_R = GS_goal_R.position - GS_start_R.position;
       Vector3 GS_route_L = GS_goal_L.position - GS_start_L.position;

        GS_leader = new GameObject("GS_leader");
        
       

        for(int i= 0; i < 1000 / GS_interval; i++)
        {
            Vector3 GS_position_R = GS_start_R.position + ((GS_route_R / 1000)* GS_interval * i);

            Vector3 GS_position_L = GS_start_L.position + ((GS_route_L / 1000)* GS_interval * i);
            
            Instantiate(guide_sphere, GS_position_R, Quaternion.Euler(0f, 0f, 0f),GS_leader.transform);

            Instantiate(guide_sphere, GS_position_L, Quaternion.Euler(0f, 0f, 0f),GS_leader.transform);
        }

    }

    public void Guide_Sphere_Off()
    {
        DestroyImmediate(GS_leader);
    }
}
