using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 2;
    public float width = 0.1f;

    //[System.NonSerialized]
    public int color_value = 255;

    public float length;

    public LineRenderer lineRenderer;

    public float theta;

    Material line_mat;
 
    void Start()
    {
        
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        line_mat = lineRenderer.material;
        line_mat.color = Color.white;
        lineRenderer.positionCount = segments;
       
    }

    void Update()
    {
        lineRenderer.widthMultiplier = width;
        


        /*float x = radius * Mathf.Cos(theta);
            float y = length;
            Vector3 pos = this.transform.position + new Vector3(0, 0f, y);*/

            
            Vector3 pos = this.transform.parent.transform.position;
            lineRenderer.SetPosition((0), pos);



            float x = length * Mathf.Cos(theta);
            float y = length * Mathf.Sin(theta);
            pos = this.transform.position + new Vector3(x, 0.01f, y);
            lineRenderer.SetPosition((1), pos);
 
       
 
        
        Color32 color = line_mat.color;

        color.a = (byte)color_value;


        line_mat.color = color;
    }


    public void CircleDestroy(){
        Destroy(this.gameObject);
    }
}
