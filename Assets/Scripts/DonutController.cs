using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutController : MonoBehaviour
{

    public GameObject circle;

    public Transform circle_pos;

    public Transform canvas_trans;

    // Start is called before the first frame update
    void Start()
    {
        CreateCircle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCircle(){

        if(IntersectionMaster.hazard_type == "element1_bicycle"){
            //circle.GetComponent<Animator>().SetFloat("People1",2.0f);
            //Debug.Log("done     ");
        }else{
            //circle.GetComponent<Animator>().SetFloat("People1",1.0f);
            //Debug.Log("no       ");
        }

        Instantiate(circle, circle_pos.position, Quaternion.identity,canvas_trans);
    }
}
