using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowCreater : MonoBehaviour
{

    public GameObject flow;

    public int f_number;

    // Start is called before the first frame update
    void Start()
    {
        float deltaTheta = (-1f * Mathf.PI) / (f_number-1);
        float theta = 0f;

        

        for (int i = 0; i < f_number ; i++)
        {
            flow.gameObject.GetComponent<FlowController>().theta = theta;

            Instantiate(flow, this.transform.position, Quaternion.identity,this.transform);
            
            theta += deltaTheta;
            
            
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
