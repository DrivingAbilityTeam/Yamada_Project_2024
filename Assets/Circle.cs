using UnityEngine;

public class Circle : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 20;
    public float width = 0.1f;

    //[System.NonSerialized]
    public int color_value = 255;

    public LineRenderer lineRenderer;

    Material line_mat;
 
    void Start()
    {
        
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        line_mat = lineRenderer.material;
        //line_mat.color = Color.white;

        //Debug.Log("presets   " + lineRenderer.startColor.r);
       
    }

    void Update()
    {
        lineRenderer.widthMultiplier = width;
        lineRenderer.positionCount = segments + 1;

        float deltaTheta = 0f;

        
          deltaTheta = (2f * Mathf.PI) / segments;
        
        //float deltaTheta = (-1f * Mathf.PI) / segments;
        float theta = 0f;
 
        for (int i = 0; i < segments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            Vector3 pos = this.transform.position + new Vector3(x, 0.01f, y);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }

        Color32 color = line_mat.color;

        color.a = (byte)color_value;


        line_mat.color = color;
    }


    public void CircleDestroy(){
        Destroy(this.gameObject);
    }
}