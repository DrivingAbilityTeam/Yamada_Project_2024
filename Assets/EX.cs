using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX : MonoBehaviour
{

    public GameObject slope;

    float gap;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 position = transform.position; // ローカル変数に格納

        Vector3 s_position = slope.transform.position;

        gap = s_position.x - position.x;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKey (KeyCode.LeftArrow)) {
       Debug.Log("MOVE");
    }*/

    Vector3 position = transform.position; // ローカル変数に格納

    Vector3 s_position = slope.transform.position;

    s_position.x = position.x + gap;
    
    slope.transform.position = s_position; // ローカル変数を代入




    }
}
