using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSphere : MonoBehaviour
{

    private GameObject obstacle;

    float obstacle_distance;

    public float player_distance;

    public float border;//40

    public float max_height;

    bool hazardon_past;

    public float player_GS_gap;// 7

    Vector3 position;

    Vector3 start_position;

    Vector3 start_scale;

    bool hazard_on_left;

    public float height_start_size;

    public Vector3 car_position;

    bool onleft;

    public float change;

    public float distance;

    public AnimationCurve animation_curve;

    public AnimationCurve animation_curve_near;

    float count;

    [Range(1,100)]
	public float velocity;

    public GameObject player;


    public bool system_on;

    public float system_start_position_z;

    [Range(1,1000)]
    public int shake_velocity;

    int shake_count;

    public int code;

   
    void Start()
    {
        
        if(GuideSphereController.GS_code == 0){

            

        }else if(GuideSphereController.GS_code == 1){

            Vector3 localScale = transform.localScale; 
            localScale.y = height_start_size; 
            transform.localScale = localScale;

            //Debug.Log("Kaiju");

        }

        hazardon_past = false;

        start_position = this.transform.position;

        start_scale = this.transform.localScale;

        if(car_position.x > this.transform.position.x){
            onleft = true;
        }

        player=  FindObstacle("Player");
        
    }

    
    void Update()
    {
       



        if(obstacle != null && SignController.hazardon == true && system_on)
        {
            
            Encount_Hazard();

            

        }else if(system_on  && IntersectionController.hazard_code > -1){
                 
                 Before_Hazard();
                 
                 

            }


        

        if(hazardon_past == false && SignController.hazardon == true){

            

            obstacle= FindObstacle("Hazard");

            /*if(obstacle != null){
            Debug.Log("obstacle_name" + obstacle.gameObject.name);
            }*/

        }

        hazardon_past = SignController.hazardon;


        if(!system_on && IndicationController.indicate){

            system_on = true;

        }

        
        

    }

    private void Encount_Hazard(){

        obstacle_distance = Vector3.Distance(start_position, obstacle.transform.position);

        //Debug.Log("obstacle_distance   " + obstacle_distance);

            Vector3 hazard_pos = obstacle.transform.position;



            

            if( obstacle_distance < border){//obstacle_distance < border   hazard_pos.x >  -3f

                
                if(GuideSphereController.GS_code == 1){

                 Vector3 localScale = transform.localScale; 
                 localScale.y = (0 + ((border/obstacle_distance)/3f)); 

                 if(max_height < localScale.y)
                 {
                    localScale.y = max_height;
                 }

                 transform.localScale = localScale;

                }






                 

                 position = transform.position; // ローカル変数に格納



                if(count < (500f / velocity)){
                    count++;
                }

               x = ((start_position.z - player.transform.position.z) / 10f) + player_GS_gap;

               float y_min = 1.0f, y_max = 1.5f;

               float y = (y_max - Mathf.Pow(((obstacle.transform.position.z - player.transform.position.z) / 50f), 2));

                if(y < y_min){
                    y = y_min;
                }

                if(y > y_max){
                    y = y_max;
                }

              // Debug.Log("y = " + y);

             // Debug.Log("x   " + x);


                float change_value = animation_curve_near.Evaluate(x) * (count / (500f / velocity)) * y;

                change = change_value;

                if(onleft){
                    change_value = change_value * (-1f);
                }

                position.x = start_position.x - change_value;

                transform.position = position;

            }

    }


    public float x;

    private void Before_Hazard(){

        if(true)//Input.GetKey(KeyCode.Return)
        {
             /*if(player == null)
            {
                player=  FindObstacle("Player");
            }*/

            player_distance = Vector3.Distance(start_position, player.transform.position);

            if(player_distance > border){
                return;
            }


            position = transform.position; // ローカル変数に格納



                if(count < (500f / velocity)){
                    count++;
                }

                x = ((start_position.z - player.transform.position.z) / 10f)  + player_GS_gap -0.5f;



                float change_value = animation_curve_near.Evaluate(x) * (count / (500f / velocity)) * 1.5f;



                

                /*if(shake_count < shake_span){
                    shake_count++;
                }else{
                    shake_count = 0;
                }

                

                float shake_value = (((float)shake_span - (float)shake_count) / (float)shake_span);*/



                float shake_value = 0.5f + (0.5f * Mathf.Sin(Time.time * shake_velocity)); 

                change_value = change_value * shake_value;


                if(code ==1){
                    Debug.Log("shake_count " + shake_count + "shake_value" + shake_value);
                }




                change = change_value;

                if(onleft){
                    change_value = change_value * (-1f);
                }

                position.x = start_position.x - change_value;

                transform.position = position;
        }

    }

    

    private GameObject FindObstacle(string tag_name)
    {
            
        GameObject out_object = null;

        if(GameObject.FindGameObjectsWithTag(tag_name) == null)
        {
            return null;
        }

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(tag_name);

        float dis;
        float disc = 0;

        foreach (GameObject obs in obstacles)
        {
            dis = Vector3.Distance(this.transform.position, obs.transform.position);

            if (disc == 0)
            {
                disc = dis;
                out_object = obs;
            }
            else if (dis < disc)
            {
                disc = dis;
                out_object = obs;
            }

        }

        if(out_object == null)
        {
            return null;
        }

        if(out_object.transform.position.x < this.transform.position.x  && out_object.gameObject.tag == "Hazard"){
            hazard_on_left = true;
        }else{
            hazard_on_left = false;
        }

        return out_object;



    }
}
