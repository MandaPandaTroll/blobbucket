using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class Logger : MonoBehaviour
{
GameObject[] blobs;
GameObject[] blibs;
List <GameObject> blobList;
public  List <GameObject> blibList;
private List<string[]> rowData = new List<string[]>();

    
    float time;
    float totalTime;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        

        time += Time.deltaTime;
        
            if (time >= 5.0f){
                blobs = GameObject.FindGameObjectsWithTag("Predator");
                blibs  = GameObject.FindGameObjectsWithTag("Prey");
                

                 blobList = new List <GameObject>(blobs);
                 blibList = new List <GameObject>(blibs);
                
             
            totalTime = Time.time;


            Save();
            


            }
        


    }
        void Save(){
            
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[2];
            rowDataTemp[0] = blibList.Count.ToString();
            rowDataTemp[1] = blobList.Count.ToString();
            
            rowData.Add(rowDataTemp);
    

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
        
        blobList.Clear();
        blibList.Clear();
        Array.Clear(blobs,0,blobs.Length);
        Array.Clear(blibs,0,blibs.Length);
        time = 0f;

    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Saved_data.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_STANDALONE_OSX
        return Application.dataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}



