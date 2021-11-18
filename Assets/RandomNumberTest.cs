
//prints random numbers to .csv file
//original code by smkplus
//modified by tabacwoman november 2021


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class RandomNumberTest : MonoBehaviour
{

GameObject[] blibs;

private List<string[]> rowData = new List<string[]>();

    
    float time;
    float totalTime;

   public int sampleSize;
    public float sampleRate;
    
   

    int sampler;

    public List <int> rand1;
    public List <int> rand2;

    public List <int> rand3;
    public List <int> rand4;
    
 
    
    
 
    BlibControls sampledBlib;

    // Start is called before the first frame update
    void Start()
    {
    
        sampleSize = 10;
        sampleRate = 10f;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        time += Time.deltaTime;
        
            if (time >= sampleRate){
               System.Random rnd = new System.Random();
                



                
                    
                for (int i = 0; i < sampleSize; i++)
                {   
                rand1.Add(rnd.Next(-1,2));
                rand2.Add(rnd.Next(-1,2));
                rand3.Add(rnd.Next(-1,2));
                rand4.Add(rnd.Next(-1,2));
                        
                }
            
             


            



        
            
            
            Save();
            


            }
        


    }
        void Save(){
           
            string[] rowDataTemp;

        
        // Creating First row of titles manually..


            
            


            


        // You can add up the values in as many cells as you want.
        
            rowDataTemp = new string[4];
            for(int i = 0; i< sampleSize; i++){
            rowDataTemp[0] = rand1[i].ToString();
            rowDataTemp[1] = rand2[i].ToString();
            rowDataTemp[2] = rand3[i].ToString();
            rowDataTemp[3] = rand4[i].ToString();
            rowData.Add(rowDataTemp);
            }
            

            

            

            
        
    

        string[][] output = new string[rowData.Count][];

        for(int i = 0; i < output.Length; i++){
            output[i] = rowData[i];
        }

        int     length         = output.GetLength(0);
        string     delimiter     = ",";

        StringBuilder sb = new StringBuilder();
        
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));
        
        
        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
        

        rand1.Clear();
        rand2.Clear();
        rand3.Clear();
        rand4.Clear();

        time = 0f;



    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Random_numbers.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Random_numbers.csv";
        #elif UNITY_STANDALONE_OSX
        return Application.dataPath+"/"+"Random_numbers.csv";
        #else
        return Application.dataPath +"/"+"Random_numbers.csv";
        #endif
    }
}



