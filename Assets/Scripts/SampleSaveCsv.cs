using System.IO;
using System.Text;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

// csvに保存するためのコード
// SaveCsvへアタッチ
public class SampleSaveCsv : MonoBehaviour
{
    // System.IO
    private StreamWriter sw;

    public bool save;

    public bool save2;

    string file_name;

    public int record_number;

    

    // Start is called before the first frame update
    void Start()
    {

        DateTime dt = DateTime.Now;

        if(save){

           

            string title = "SaveData_" +SceneManager.GetActiveScene().name+"_"+ dt.Month.ToString() + "_" + dt.Day.ToString()+ "_" + dt.Hour.ToString()+ "_" + dt.Minute.ToString()+ "_" + record_number+".csv";

            string record = PlayerPrefs.GetString(title, null);

            

            
                sw = new StreamWriter(@title, false, Encoding.GetEncoding("Shift_JIS"));
            

        }else if(save2){

            //int number = PlayerPrefs.GetInt("Number", 0);

            //number++;

             //PlayerPrefs.SetInt("Number", number);

            //file_name = IntersectionMaster.hazard_type;

           
             string title = "SaveData_" +SceneManager.GetActiveScene().name+"_"+ dt.Month.ToString() + "_" + dt.Day.ToString()+ "_" + dt.Hour.ToString()+ "_" + dt.Minute.ToString()+ "_" + record_number+".csv";

            string record = PlayerPrefs.GetString(title, null);

            

            if(record == null){

                sw = new StreamWriter(@title, false, Encoding.GetEncoding("Shift_JIS"));

                PlayerPrefs.SetString(title, title);

                string[] s1 = { "car", "base", "hazard", "gap"};

                string s2 = string.Join(",", s1);

                sw.WriteLine(s2);

            }else{
                sw = new StreamWriter(@title, false, Encoding.GetEncoding("Shift_JIS"));
            }

            //PlayerPrefs.SetString(title, title);

            //Debug.Log("null or not" + record);


             
       
        

        
        

        //string s2 = string.Join(",",null);

       /* if(record == null){
        
        string s2 = string.Join(",", s1);

        sw.WriteLine(s2);

        }*/

       
        

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Enterキーが押されたらcsvへの書き込みを終了する
        if (Input.GetKeyDown(KeyCode.Return))
        {
            /**
             * @see https://docs.microsoft.com/ja-jp/dotnet/api/system.io.streamwriter.close?view=net-6.0#System_IO_StreamWriter_Close
             */
            //sw.Close();
        }
    }

    public void SaveData1(string txt1, string txt2, string txt3,string txt4,string txt5)
    {
        if(sw == null){
            return;
        }


        string[] s1 = { txt1, txt2, txt3 ,txt4,txt5};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        //Debug.Log("s2 " + s2);
    }

    public void SaveData(string txt1, string txt2, string txt3,string txt4,string txt5,string txt6)
    {
        if(sw == null){
            return;
        }


        string[] s1 = { txt1, txt2, txt3 ,txt4,txt5,txt6};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        //Debug.Log("s2 " + s2);
    }

    private void OnApplicationQuit()
    {
        if(sw == null){
            return;
        }


        //Debug.Log("OnApplicationQuit");

        sw.Close();
    }
}