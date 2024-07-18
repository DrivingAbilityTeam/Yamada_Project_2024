using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    private int[] inter_actionvariation;

    public int inter_action1;
    public int inter_action2;
    public int inter_action3;

    private int inter_actioncode;

    public int actioncode;

    private int number;

    public string rorl;

    public bool random;

    public GameObject sign_3;

    public GameObject sign_4;

    public bool onmove;

    public float move_speed = 0.01f;

    public AudioSource audio;

    float sound_volume;

    float sound_count;

    float sound_span;


    // Start is called before the first frame update
    void Start()
    {
        inter_actionvariation = new int[inter_action1+ inter_action2+ inter_action3];
        for (int i = 0; i < inter_action1 ; i++)
        {
            inter_actionvariation [i] = 1;
        }

        for (int i = inter_action1; i < inter_action1 + inter_action2; i++)
        {
            inter_actionvariation[i] = 2;
        }

        for (int i = inter_action1+ inter_action2; i < inter_action1 + inter_action2 + inter_action3; i++)
        {
            inter_actionvariation[i] = 3;
        }

       

       if(SignController.signcode == 3)
       {
            sign_3.gameObject.SetActive(true);
       }

       if(SignController.signcode == 4)
       {
            sign_4.gameObject.SetActive(true);
       }

       
        sound_volume = AudioController.volume;

        sound_span = AudioController.span;

         if (actioncode == -1)
            {
                /*if(onmove){
                onmove = false;



                }else{
                    GetComponent<Rigidbody>().isKinematic = true;



                }*/

                transform.Rotate(new Vector3(0, 180, 0));
            }

            //Debug.Log("actioncode  " + actioncode);

    }

    // Update is called once per frame
    void Update()
    {
        if(onmove)
       {
            
        Transform myTransform = this.transform;
 
       

       myTransform.Translate(0f,0f,move_speed / 3.6f * Time.deltaTime);

       }



        if(AudioController.clip != null){

            float span = (Mathf.Abs((transform.position.x  - 0.5f))) /20f;

            if(span < 0.1f){
                span = 0.1f;
            }

            span = span * sound_span;

           

            


            

       if(sound_count < span){
        sound_count +=  Time.deltaTime;
       }else{
        sound_count = 0;
        Sound();
       }


        }
    }


    private void Sound(){

        

        
        audio.volume = sound_volume;

        audio.PlayOneShot(AudioController.clip);
    }

    private int InterAction()
    {
        number = Random.Range(0, inter_action1 + inter_action2 + inter_action3);
        inter_actioncode = inter_actionvariation[number];
        return inter_actioncode;
    }


    void OnTriggerEnter(Collider other)
    {

       
    }


    void OnTriggerStay(Collider other)
    {

       
        
    }


    
}
