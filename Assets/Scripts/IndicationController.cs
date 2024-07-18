using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicationController : MonoBehaviour
{

    public static bool indicate;

    public static int indicate_code;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// Needed to attach "Player" tag on box collider.
        {
            indicate = true;

            Debug.Log("Indication Start");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")// Needed to attach "Player" tag on box collider.
        {
            indicate = false;

            Debug.Log("Indication End");
        }
    }
}
