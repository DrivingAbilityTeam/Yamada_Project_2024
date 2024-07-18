using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    //public AudioClip[] clips;

     [SerializeField]
    Intersection[] clips;



    
    [System.Serializable]
    class Intersection
    {
       public AudioClip clip_prefab;

        
        [Range(0.1f,10f)]
        public float clip_span = 1f;

        [Range(0.0f,1.0f)]
        public float  clip_volume = 0.5f;
    }

    public static int audio_code;

    public static AudioClip clip;

    public static float span;

    public static float volume;

    public AudioSource player_audio;

    [Range(0.0f,1.0f)]
    public float sound_volume;

    float sound_count;

    [Range(0.1f,10f)]
    public float sound_span;

    public bool allow;

    


    //public AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        clip = clips[audio_code].clip_prefab;

        span = clips[audio_code].clip_span;

        volume = clips[audio_code].clip_volume;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Return))
        {
            allow = true;
        }*/



        if(clip != null && IndicationController.indicate){
            

       if(sound_count < sound_span){
        sound_count +=  Time.deltaTime;
       }else{
        sound_count = 0;
        Sound();
       }

        }
    }

    private void Sound(){

        
        player_audio.volume = sound_volume;

        player_audio.PlayOneShot(clip);
    }
}
