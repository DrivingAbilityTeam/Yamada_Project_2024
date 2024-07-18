using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public GameObject car;

    public static GameObject player_car;


    [System.NonSerialized]
    public static bool signavailable;

    private bool signon = false;

    [SerializeField] GameObject signpoint_L;

    [SerializeField] GameObject signpoint_R;

    [SerializeField] GameObject signpoint_center;

    [SerializeField] private SignManager signmanager;

    private GameObject sign;

    public static int signcode;

    Vector3 signposition;

    public static string rorl;

    public static float value;

    public static bool no_hazard;

    
    

    public static bool hazardon;

    public static float player_velocity;

    public GameObject attention;

    public bool no_attention;

    // Start is called before the first frame update
    void Start()
    {
        hazardon = false;

        if(attention.gameObject.activeSelf){
            attention.SetActive(false);
        }

        player_car = car;
    }

    // Update is called once per frame
    void Update()
    {
        if (!signon && signavailable && !no_hazard)
        {
            Sign();
            
        }

        if(!attention.gameObject.activeSelf && IndicationController.indicate && !no_attention){
            attention.SetActive(true);
        }

        if(attention.gameObject.activeSelf && !IndicationController.indicate){
            attention.SetActive(false);
        }

        if(signon && !signavailable)
        {
            SignEnd();
            //signon = false;
        }

        player_velocity = player_car.GetComponent<Rigidbody>().velocity.z * 3.6f;
    }

    private void Sign()
    {
        /*if (signmanager.GetSigncode(signcode).Script())
        {
            
            player_car = car;
        }*/

        if(!signmanager.GetSigncode(signcode).Prefab())
        {
            return;
        }


        
        if (signmanager.GetSigncode(signcode).GetKind() == "right_and_left")
        {
            if (rorl == "L")
            {
                signposition = signpoint_L.transform.position;
                sign = Instantiate(signmanager.GetSigncode(signcode).GetSign(), signposition, Quaternion.Euler(0f, 0f, 0f));


            }
            else
            {
                signposition = signpoint_R.transform.position;
                sign = Instantiate(signmanager.GetSigncode(signcode).GetSign(), signposition, Quaternion.Euler(0f, 180f, 0f));


            }
        }
        else
        {
            signposition = signpoint_center.transform.position;
            sign = Instantiate(signmanager.GetSigncode(signcode).GetSign(), signposition, Quaternion.Euler(0f, 0f, 0f));

        }


        





        sign.transform.SetParent(this.transform, false);
            sign.transform.position = signposition;

        

        signon = true;


    }

    private void SignEnd()
    {
        if (sign != null)
        {
            Destroy(sign);
        }
        signon = false;
    }


    
    
}
