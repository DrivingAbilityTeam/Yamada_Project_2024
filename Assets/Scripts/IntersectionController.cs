using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{

   

    public GameObject hazard_spawn_area_L;
    public GameObject hazard_spawn_area_R;
    public GameObject[] planes;
    public GameObject hole;
    public GameObject[] waves;
    public GameObject[] magnets;
    public GameObject[] waves2;

    /*[SerializeField]
    Wave2[] wave2;



    
    [System.Serializable]
    class Wave2
    {
        public GameObject[] waves2;

       
    }*/
    
    public GameObject donut;
    public GameObject donut1;
    public GameObject circle_pos;
    public GameObject circle_pos1;


    public GameObject[] indicate_waves;
    public GameObject[] indicate_bumps;





    [System.NonSerialized]
    public GameObject hazard1;

    [System.NonSerialized]
    public string rorl;

    [System.NonSerialized]
    public int actioncode;

    [System.NonSerialized]
    public float value;

    [System.NonSerialized]
    public bool random;

    [System.NonSerialized]
    public bool no_hazard;

    private  Animator _animator;

    GameObject hazard = null;

    public static GameObject s_hazard;

    int wave_count;


    int wave2_number;

    

    public GameObject[] base_point;

    bool car_detected;

    
    public static int hazard_code;

    // Start is called before the first frame update
    void Start()
    {
        if(hole.gameObject.activeSelf){
            hole.SetActive(false);
        }

        if(donut.gameObject.activeSelf){
            donut.SetActive(false);
        }

        if(donut1.gameObject.activeSelf){
            donut1.SetActive(false);
        }

        for(int i = 0; i < waves.Length; i++){

        if(waves[i].gameObject.activeSelf){
            waves[i].SetActive(false);
        }

        }

         for(int k = 0; k < magnets.Length; k++){

        if(magnets[k].gameObject.activeSelf){
            magnets[k].SetActive(false);
        }

        }

        

        for(int i = 0; i < waves2.Length; i++){

        if(waves2[i].gameObject.activeSelf){
            waves2[i].SetActive(false);
        }

        }


                planes[0].gameObject.SetActive(false);

                planes[1].gameObject.SetActive(false);

                planes[2].gameObject.SetActive(false);

         if(SignController.signcode < 6)
            {
                planes[1].gameObject.SetActive(true);

            }else if(SignController.signcode == 6){
            
                planes[2].gameObject.SetActive(true);
            
            }else{
                planes[0].gameObject.SetActive(true);

            }

        for(int i = 0; i < indicate_waves.Length; i++){

        if(indicate_waves[i].gameObject.activeSelf){
            indicate_waves[i].SetActive(false);
        }

        }

         for(int i = 0; i < indicate_bumps.Length; i++){

        if( indicate_bumps[i].gameObject.activeSelf){
             indicate_bumps[i].SetActive(false);
        }

        }


        wave2_number = waves2.Length;

         

        
    }

    // Update is called once per frame
    void Update()
    {
        if(IndicationController.indicate){

            //Debug.Log("indication" + IndicationController.indicate_code);

            if(IndicationController.indicate_code == 1){

                
            for(int i = 0; i < indicate_waves.Length; i++){

            if(!indicate_waves[i].gameObject.activeSelf){
                indicate_waves[i].SetActive(true);
            }

            }
            }else if(IndicationController.indicate_code == 2){

                for(int i = 0; i < indicate_bumps.Length; i++){

            if(!indicate_bumps[i].gameObject.activeSelf){
                indicate_bumps[i].SetActive(true);
            }

            }

            }

        }else{

            if(IndicationController.indicate_code == 1){

            for(int i = 0; i < indicate_waves.Length; i++){

            if(indicate_waves[i].gameObject.activeSelf){
                indicate_waves[i].SetActive(false);
            }

            }
            }else if(IndicationController.indicate_code == 2){

                for(int i = 0; i < indicate_bumps.Length; i++){

            if(indicate_bumps[i].gameObject.activeSelf){
                indicate_bumps[i].SetActive(false);
            }

            }

            }

        }

        if(Input.GetKeyDown(KeyCode.Return) && IntersectionMaster.sample2 && car_detected){

            if(SignController.hazardon){
                Destroy(hazard.gameObject);
            }

            SignController.no_hazard = false;

            CreateHazard();

        }

        if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.JoystickButton2) ){

            

           if(IntersectionMaster.sample2 && car_detected){

            int n = 0;

            if(rorl == "L"){
                n = 1;
            }

            

            Debug.Log("Car  " + Mathf.Abs(car.gameObject.transform.position.x) + "  Base   " + base_point[n].transform.position.x+ "  Hazard   " + hazard.transform.position.x + "  Gap  " + (-base_point[n].transform.position.x  +hazard.transform.position.x ));

            IntersectionMaster.point_list[0] =  Mathf.Abs(car.gameObject.transform.position.x);
             IntersectionMaster.point_list[1] =  base_point[n].transform.position.x;
             IntersectionMaster.point_list[2] =  hazard.transform.position.x;
             IntersectionMaster.point_list[3] =  (-base_point[n].transform.position.x + hazard.transform.position.x );
             IntersectionMaster.name_list[0] =  IntersectionMaster.hazard_type;
             IntersectionMaster.name_list[1] =  IntersectionMaster.sign_name;
           }
        }


        if(hazard != null)
        {

            

            if(SignController.signcode == 6)
            {
                //Debug.Log("Edge Worse Kyper Belt");


                if(!hole.gameObject.activeSelf)
                {
                    hole.SetActive(true);
                }

                hole.transform.position = hazard.transform.position;
            }

            if(SignController.signcode == 7)
            {

               

                
                    
                for(int i = 0; i < waves.Length; i++){

                if(!waves[i].gameObject.activeSelf)
                {
                    waves[i].SetActive(true);

                    //Debug.Log(waves[i].name+ "  activated");
                }

                }

                


                Vector3 pos = hazard.transform.position;

                Vector3 raw = pos;

                for(int i = 0; i < waves.Length; i++){

                    pos.y = pos.y + 0f;

                    waves[i].transform.position = pos;

                }

                

                
                
            }


            if(SignController.signcode == 8)
            {

                

                
                    
                for(int i = 0; i < magnets.Length; i++){

                if(!magnets[i].gameObject.activeSelf)
                {
                    magnets[i].SetActive(true);

                    //Debug.Log(waves[i].name+ "  activated");
                }

                }

                


                Vector3 pos = hazard.transform.position;

                Vector3 raw = pos;

                for(int i = 0; i < magnets.Length; i++){

                    pos.y = pos.y + 0f;

                    magnets[i].transform.position = pos;

                }

                

                //pos.z = pos.z +  -0.5f * Mathf.Pow(raw.x,2);

                
            }

            if(SignController.signcode == 9)
            {
                for(int i = 0; i < wave2_number; i++){

                if(!waves2[i].gameObject.activeSelf)
                {
                    waves2[i].SetActive(true);


                    if(i == 5){
                        waves2[i].gameObject.transform.localScale = new Vector3 (1f,0.3f,1f);
                    }

                    //Debug.Log(waves[i].name+ "  activated");
                }

                }

                


                Vector3 pos = hazard.transform.position;

                Vector3 raw = pos;

                for(int i = 0; i < wave2_number; i++){

                    pos.y = pos.y + 0f;

                    waves2[i].transform.position = pos;

                    

                }



                
            }


            if(SignController.signcode == 0)
            {


                string name_head = hazard.gameObject.name.Substring(1, 1);


                Debug.Log("active  or not   " +donut1.gameObject.activeSelf );


                
                
                if(name_head =="i" && !donut1.gameObject.activeSelf ){
                    donut1.SetActive(true);

                    Debug.Log("Yes  ");
                    
                }else if(!donut.gameObject.activeSelf && !donut1.gameObject.activeSelf){
                

               
                    donut.SetActive(true);

                     Debug.Log("No  ");
                }

                //donut.transform.position = hazard.transform.position;
                circle_pos.transform.position = hazard.transform.position;

                circle_pos1.transform.position = hazard.transform.position;

                
            }
        }
    }

    

    private void CreateHazard()
    {

        hazard1.GetComponent<HumanController>().actioncode = actioncode;

        if (random)
        {
            int rnd = Random.Range(0, 2);　// ※ 1～9の範囲でランダムな整数値が返る

            if(rnd == 0){
                rorl = "R";
            }else{
                rorl = "L";
            }

            SignController.rorl = rorl;
        }

        string name_head = hazard1.gameObject.name.Substring(1, 1);

        float fixed_dis = 0f;


        if(name_head == "i"){
            fixed_dis = 12f;
            hazard_code = 2;
        }else if(name_head == "o"){
            fixed_dis = -3f;
            hazard_code = 3;
        }else{
            hazard_code = 1;
        }

        if(actioncode == -1){
            fixed_dis += 20f;
        }

        //Debug.Log("fixed_dis   " + fixed_dis);

        if (rorl == "R")
        {

             Vector3 spawn_pos = new Vector3(hazard_spawn_area_R.transform.position.x + fixed_dis,hazard_spawn_area_R.transform.position.y,hazard_spawn_area_R.transform.position.z);

            hazard = Instantiate(hazard1, spawn_pos, Quaternion.Euler(0f, 270f, 0f));
            
            hazard1.GetComponent<HumanController>().rorl = "R";
            //Debug.Log(this.gameObject.name);
        }
        else
        {
             Vector3 spawn_pos = new Vector3(hazard_spawn_area_L.transform.position.x - fixed_dis,hazard_spawn_area_L.transform.position.y,hazard_spawn_area_L.transform.position.z);


            hazard = Instantiate(hazard1, spawn_pos, Quaternion.Euler(0f, 90f, 0f));
            hazard1.GetComponent<HumanController>().rorl = "L";
            //Debug.Log(this.gameObject.name);
        }

        //hazard1.GetComponent<HumanController>().actioncode = actioncode;

        s_hazard = hazard;

        

        SignController.value = value;
        _animator = hazard1.GetComponent<Animator>();
        //_animator.SetFloat("speed", value);//�A�j���[�^�[�̃p�����[�^�[�͓������͎̂g���Ȃ��I


        if(actioncode != -1){
        SignController.hazardon = true;
        
        }else{
            hazard_code = -1;
        }

        



    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// Needed to attach "Player" tag on box collider.
        {
            //Debug.Log("Car hit the Intersectionn!!!!!!!!");


            SignController.rorl = rorl;
            SignController.signavailable = true;
            SignController.no_hazard = no_hazard;

            if(!no_hazard){
                CreateHazard();
                IndicationController.indicate = false;
            }

            

        }
        else
        {
            //Debug.Log("Amesansan    " + other.gameObject.name);
        }
    }

    GameObject car;

     void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")// Needed to attach "Player" tag on box collider.
        {
            car = other.gameObject;

             SignController.rorl = rorl;
            SignController.signavailable = true;
            SignController.no_hazard = no_hazard;

            car_detected = true;

            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")// Needed to attach "Player" tag on box collider.
        {
            SignController.signavailable = false;
           
           SignController.hazardon = false;

           IndicationController.indicate = true;

           hazard_code = 0;
        }
    }

    /*private IEnumerator WaveCreater(){


        for(int i = 0; i < wave2.Length; i++){

                if(!wave2[i].waves2[wave_count].gameObject.activeSelf)
                {
                    wave2[i].waves2[wave_count].SetActive(true);

                    
                }

                }

                

                


                Vector3 pos = hazard.transform.position;

                Vector3 raw = pos;

                for(int i = 0; i < wave2.Length; i++){

                    pos.y = pos.y + 0f;

                    wave2[i].waves2[wave_count].transform.position = pos;

                }


                if(wave_count <= wave2[0].waves2.Length){

                    wave_count++;

                }else{

                    wave_count = 0;

                }

                

        yield return new WaitForSeconds(2);

        StartCoroutine(WaveCreater());



    }*/
}
