using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionMaster : MonoBehaviour
{
    

    [ContextMenu("Guide_Sphere_On")]
    void Guide_Sphere_On()
    {
        GS_controller.Guide_Sphere_On();
    }

     [ContextMenu("Guide_Sphere_Off")]
    void Guide_Sphere_Off()
    {
        GS_controller.Guide_Sphere_Off();
    }

    [ContextMenu("Line_On")]
    void Line_On()
    {
        linecontroller.Line_On();
    }

     [ContextMenu("Line_Off")]
    void Line_Off()
    {
        linecontroller.Line_Off();
    }

    
    public enum EaseType2
    {
        sign_1,
        sign_2,
        sign_3_shadow,
        sign_4_light,
        sign_5_null,
        sign_6_hole,
        sign_7_wave,
        sign_8_density,
        sign_9_wave2,
        sign_0_circle,
        sign_10,
        sign_11,
        sign_12,


    }
    public EaseType2 sign_code;

    /*public enum SignType
    {
        full,
        half,
        middle,

    }
    public SignType sign_type;

    public static int type_code;*/

    public enum EaseType3
    {
        audio0,
        audio1,
        audio2,
        audio3,
        audio4,
        audio5,
        audio6,



    }
    public EaseType3 audio_code;

    public enum EaseType4
    {
        indicate0_null,
        indicate1_waves,
        indicate2_bumps,




    }
    public EaseType4 indicate_code;


    [SerializeField]
    Intersection[] intersections;



    
    [System.Serializable]
    class Intersection
    {
        public IntersectionController intersectioncontroller;

        public enum EaseType2
        {
            element0_human,
            element1_bicycle,
            element2_child,
            element3,
            element4




        }
        public EaseType2 hazard;


        public enum EaseType
        {
            right,
            left

            
            
        }
        public  EaseType direction;



        public int actioncode;




        [Range(0, Mathf.Infinity)]
        public float value;


        public bool random;

        public bool no_hazard;
    }

    

    public List<GameObject> hazardlist;

    private int hazardtype;

    public SignController signcontroller;

    public GuideSphereController GS_controller;

    public LineController linecontroller;

    public GameObject car;

    Rigidbody car_rigid;

    public SampleSaveCsv excel;

    public bool sample;

    public static bool sample2;

    public static string sign_name;

    public static float[] point_list=new float[6];

    public static string[] name_list=new string[2];

    public static string hazard_type;

    LogitechGSDK.DIJOYSTATE2ENGINES rec;
    
    

    // Start is called before the first frame update
    void Start()
    {
        sample2 = sample;


        for (int i = 0; i < intersections.Length ; i++)
        {
            hazardtype = int.Parse(intersections[i].hazard.ToString().Substring(7, 1));

            if (hazardlist[hazardtype] != null)
            {
                intersections[i].intersectioncontroller.hazard1 = hazardlist[hazardtype];
            }
            else
            {
                Debug.Log("Error");
            }

            if(intersections[i].direction.ToString() == "right")
            {
                intersections[i].intersectioncontroller.rorl = "R";
            }
            else
            {
                intersections[i].intersectioncontroller.rorl = "L";
            }

            intersections[i].intersectioncontroller.actioncode  = intersections[i].actioncode;

            intersections[i].intersectioncontroller.value = intersections[i].value;

            if (intersections[i].random)
            {
                intersections[i].intersectioncontroller.random = true;
            }
            else
            {
                intersections[i].intersectioncontroller.random = false;
            }

            if (intersections[i].no_hazard)
            {
                intersections[i].intersectioncontroller.no_hazard = true;
            }
            else
            {
                intersections[i].intersectioncontroller.no_hazard = false;
            }
        }

        //SignController.signcode = int.Parse(sign_code.ToString().Substring(5, 1));

        

        switch (sign_code)
        {
            case EaseType2.sign_1:
            SignController.signcode = 1;
            break;
            case EaseType2.sign_2:
            SignController.signcode = 2;
            break;
            case EaseType2.sign_3_shadow:
            SignController.signcode = 3;
            break;
            case EaseType2.sign_4_light:
            SignController.signcode = 4;
            break;
            case EaseType2.sign_5_null:
            SignController.signcode = 5;
            break;
            case EaseType2.sign_6_hole:
            SignController.signcode = 6;
            break;
            case EaseType2.sign_7_wave:
            SignController.signcode = 7;
            break;
            case EaseType2.sign_8_density:
            SignController.signcode = 8;
            break;
            case EaseType2.sign_9_wave2:
            SignController.signcode = 9;
            break;
            case EaseType2.sign_0_circle:
            SignController.signcode = 0;
            break;
            case EaseType2.sign_10:
            SignController.signcode = 10;
            break;
            case EaseType2.sign_11:
            SignController.signcode = 11;
            break;
            case EaseType2.sign_12:
            SignController.signcode = 12;
            break;
            default:
            Debug.Log("知らない数字です。");
            break;
        }

        sign_name = sign_code.ToString();








         AudioController.audio_code = int.Parse(audio_code.ToString().Substring(5, 1));

          IndicationController.indicate_code = int.Parse(indicate_code.ToString().Substring(8, 1));

         

         car_rigid = car.gameObject.GetComponent<Rigidbody>();

         hazard_type= intersections[0].hazard.ToString();

         rec = LogitechGSDK.LogiGetStateUnity(0);
    }

    float time;

    

    

    // Update is called once per frame
    void Update()
    {
        time +=  Time.deltaTime;

        

        float accel = -1 *(rec.lY / 65536f) + 0.5f;           // アクセル
        float brake = -1 *(rec.lRz / 65536f) + 0.5f;          // ブレーキ

        //Debug.Log("brake   " +brake  +   "accel   " + accel );

        
        if(excel.save){

            float dis = 0f;

            if(IntersectionController.s_hazard != null && SignController.player_car != null && IntersectionController.hazard_code != 0){

                dis = Vector3.Distance(SignController.player_car.gameObject.transform.position,IntersectionController.s_hazard.gameObject.transform.position);

            }

            excel.SaveData(time.ToString(), brake.ToString(), accel.ToString(),SignController.player_velocity.ToString(), IntersectionController.hazard_code.ToString(),dis.ToString());
        }


        if(Input.GetKeyUp(KeyCode.RightShift) && excel.save2){

            excel.SaveData(point_list[0].ToString(), point_list[1].ToString(), point_list[2].ToString(),point_list[3].ToString(),name_list[0],name_list[1]);

        }

        if(Input.GetKeyUp(KeyCode.JoystickButton2) && excel.save2){

            excel.SaveData(point_list[0].ToString(), point_list[1].ToString(), point_list[2].ToString(),point_list[3].ToString(),name_list[0],name_list[1]);

        }

        
        
    }
}
