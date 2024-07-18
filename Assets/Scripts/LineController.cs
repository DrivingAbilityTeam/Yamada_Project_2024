using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

    public GameObject GL;

    public Transform GL_start_R, GL_goal_R;

    public Transform GL_start_L, GL_goal_L;

     GameObject GL_leader;

    [Range(0.1f, 10f)]
	public float GL_interval_x,GL_interval_z;

    [Range(0f, 0.1f)]
	public float line_width;


    [Range(2, 300)]
    public int line_point;

    //public Material material;

    Vector3[] positions;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        
    }

    public void Line_On()
    {
       Vector3 GL_route_R = GL_goal_R.position - GL_start_R.position;
       Vector3 center_pos =  new Vector3((GL_start_R.position.x +  GL_start_L.position.x) /2f,0f,(GL_start_R.position.z +  GL_goal_L.position.z) /2f);

        GL_leader = new GameObject("GL_leader");

       
        
       

        for(int i= 0; i < 1000 / GL_interval_z; i++)
        {
            //Instantiate (null, Vector3.zero, Quaternion.identity,GL_leader.transform);

            GameObject gl = Instantiate (GL, Vector3.zero, Quaternion.identity);//new GameObject("GL");

            

            Vector3 GL_position_R = new Vector3(GL_start_R.position.x,0f,(GL_start_R.position.z + ((GL_route_R.z / 1000)* GL_interval_z * i))) ;//+ ((GS_route_R / 1000)* GL_interval_z * i);

            Vector3 GL_position_L = new Vector3(GL_start_L.position.x,0f,(GL_start_L.position.z + ((GL_route_R.z / 1000)* GL_interval_z * i))) ;//+ ((GS_route_R / 1000)* GL_interval_z * i);

            
            var lineRenderer =  gl.gameObject.GetComponent<LineRenderer>();

            

            Vector3[] positions = new Vector3[line_point];


            for(int k= 0; k < line_point; k++)
            {
                positions[k] = GL_position_R + ((( GL_position_L - GL_position_R) /line_point) * k);
            }

            lineRenderer.positionCount = line_point;

            // 線を引く場所を指定する
            lineRenderer.SetPositions(positions);

            lineRenderer.startWidth = line_width;                   // 開始点の太さを0.1にする
            lineRenderer.endWidth = line_width;                     // 終了点の太さを0.1にする

           

           

            gl.gameObject.transform.parent = GL_leader.gameObject.transform;

            
        }

        Vector3 GL_route_start = GL_start_L.position - GL_start_R.position;

        for(int i= 0; i < 10 / GL_interval_x; i++)
        {
            //Instantiate (null, Vector3.zero, Quaternion.identity,GL_leader.transform);

            GameObject gl = Instantiate (GL, Vector3.zero, Quaternion.identity);//new GameObject("GL");

            

            Vector3 GL_position_R = new Vector3((GL_start_R.position.x + ((GL_route_start.x / 10)* GL_interval_x * i)),0f,GL_start_R.position.z) ;

            Vector3 GL_position_L = new Vector3((GL_goal_R.position.x + ((GL_route_start.x / 10)* GL_interval_x * i)),0f,GL_goal_R.position.z) ;

            

            
            var lineRenderer =  gl.gameObject.GetComponent<LineRenderer>();

            positions = new Vector3[line_point];

            for(int k= 0; k < line_point; k++)
            {
                positions[k] = GL_position_R + ((( GL_position_L - GL_position_R) /line_point) * k);
            }

            // 線を引く場所を指定する

            lineRenderer.positionCount = line_point;

            lineRenderer.SetPositions(positions);

            lineRenderer.startWidth = line_width;                   // 開始点の太さを0.1にする
            lineRenderer.endWidth = line_width;                     // 終了点の太さを0.1にする

           //lineRenderer.material = material;

            

            gl.gameObject.transform.parent = GL_leader.gameObject.transform;

            
        }

        

    }

    public void Line_Off()
    {
        DestroyImmediate(GL_leader);
    }
}
