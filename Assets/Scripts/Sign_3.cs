using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_3 : MonoBehaviour
{

    public enum EaseType2
    {
        wave,
        perlin_noise,


    }
    public EaseType2 sign_type;

    private Sign_Basic sign_basic;

    private Image image;

    public GameObject car;

    public GameObject obstacle;

    public GameObject shadow_wave;

    public GameObject light_wave;

    

    private float dis;

    private float disc;

    private int count =0;

    public float border1;

    //public float border2;

    

    public static float dis_now;



    


    // Start is called before the first frame update
    void Start()
    {
        sign_basic = GetComponent<Sign_Basic>();
        
        if(shadow_wave.gameObject.activeInHierarchy){
            shadow_wave.gameObject.SetActive(false);
        }

        if(shadow_wave.gameObject.activeInHierarchy){
            shadow_wave.gameObject.SetActive(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(car == null)
        {
            car = SignController.player_car;
        }

        if (car != null && obstacle != null)
        {
            dis_now = Vector3.Distance(car.transform.position, obstacle.transform.position);
            //Debug.Log("Distance is "+dis_now);
        }

        

        //Debug.Log("dis_now  " + dis_now);

        if(dis_now < border1 && dis_now != 0)
        {
            if(sign_type.ToString() == "wave"){

            if(SignController.signcode == 3){
                if(!shadow_wave.gameObject.activeInHierarchy)
                {
                    shadow_wave.gameObject.SetActive(true);
                }
            }else if(SignController.signcode == 4){
                if(!shadow_wave.gameObject.activeInHierarchy)
                {
                    light_wave.gameObject.SetActive(true);
                }
            }
            
            }
        }
        
    }






    
}
