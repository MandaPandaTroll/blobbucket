//prints genetic data to .csv file
//original code by smkplus
//modified by tabacwoman november 2021


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class BlubGenetics : MonoBehaviour
{

GameObject[] Blubs;

private List<string[]> rowData = new List<string[]>();
    int itCount;
    
    float time;
    float totalTime;

   public int sampleSize;
    public float sampleRate;
    
   

    int sampler;
    public List <int> generation;
    public List <float> intron1;
    public List <float> intron2;
    public List <float> intron3;
    public List <float> intron4;

    public List <float> moveAllele1;
    public List <float> moveAllele2;
    public List <float> redAllele1;
    public List <float> redAllele2;
    public List <float> greenAllele1;
    public List <float> greenAllele2;
    public List <float> blueAllele1;
    public List <float> blueAllele2;
    public List <float> LifeSpan;
    public List <float> lookDistance;
    public List <float> turnTorque;
     public List <int> turnDice;
    public List <float> energyToReproduce;
 
    
    
 
    BlubControls sampledBlub;

    // Start is called before the first frame update
    void Start()
    {
        itCount = 0;
        sampleSize = 100;
        sampleRate = 100f;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        time += Time.deltaTime;
        
            if (time >= sampleRate){
               
                Blubs  = GameObject.FindGameObjectsWithTag("ApexPred");



                if(Blubs.Length >= 1){
                for (int i = 0; i < sampleSize; i++)
                {   
                    
                    sampler = UnityEngine.Random.Range(0,Blubs.Length);
                    sampledBlub = Blubs[sampler].GetComponent<BlubControls>();


                    intron1.Add(sampledBlub.intron1);
                    intron2.Add(sampledBlub.intron2);
                    intron3.Add(sampledBlub.intron3);
                    intron4.Add(sampledBlub.intron4);

                    moveAllele1.Add(sampledBlub.moveAllele1);
                    moveAllele2.Add(sampledBlub.moveAllele2); 

                    redAllele1.Add(sampledBlub.redAllele1*10f);
                    redAllele2.Add(sampledBlub.redAllele2*10f);

                    greenAllele1.Add(sampledBlub.greenAllele1*10f);
                    greenAllele2.Add(sampledBlub.greenAllele2*10f);

                    blueAllele1.Add(sampledBlub.blueAllele1*10f);
                    blueAllele2.Add(sampledBlub.blueAllele2*10f);
                    LifeSpan.Add(sampledBlub.lifeLength);

                    lookDistance.Add(sampledBlub.lookDistance);
                    turnTorque.Add(sampledBlub.turnTorque);
                    turnDice.Add(sampledBlub.turnDice);
                    energyToReproduce.Add(sampledBlub.energyToReproduce);
                    generation.Add(sampledBlub.generation);
                }           
                
            
             


            



        
            
            
            Save();
            }
            


            }
        


    }
        void Save(){
            itCount += 1;
            string[] rowDataTemp;
        if (itCount == 1){
            rowDataTemp = new string[18];
            rowDataTemp[0] ="Generation";
            rowDataTemp[1] = "intron1";
            rowDataTemp[2] = "intron2";
            rowDataTemp[3] = "intron3";
            rowDataTemp[4] = "intron4";
            rowDataTemp[5] = "moveAllele1";
            rowDataTemp[6] = "moveAllele2";
            rowDataTemp[7] = "redAllele1";
            rowDataTemp[8] = "redAllele2";
            rowDataTemp[9] = "greenAllele1";
            rowDataTemp[10] = "greenAllele2";
            rowDataTemp[11] = "blueAllele1";
            rowDataTemp[12] = "blueAllele2";
            rowDataTemp[13] = "MaxlifeLength";
            rowDataTemp[15] = "turnTorque";
            rowDataTemp[16] = "turnDice";
            rowDataTemp[17] = "energyToReproduce";
            rowData.Add(rowDataTemp);
        }
        // Creating First row of titles manually..
       rowDataTemp = new string[1];
                    totalTime = Mathf.Round(Time.time);
            rowDataTemp[0] = " ";

            
            


            
            rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for(int i = 0; i < sampleSize; i++){
            rowDataTemp = new string[18];
            rowDataTemp[0] = generation[i].ToString();
            rowDataTemp[1] = intron1[i].ToString();
            rowDataTemp[2] = intron2[i].ToString();
            rowDataTemp[3] = intron3[i].ToString();
            rowDataTemp[4] = intron4[i].ToString();
            rowDataTemp[5] = moveAllele1[i].ToString();
            rowDataTemp[6] = moveAllele2[i].ToString();
            rowDataTemp[7] = redAllele1[i].ToString();
            rowDataTemp[8] = redAllele2[i].ToString();
            rowDataTemp[9] = greenAllele1[i].ToString();
            rowDataTemp[10] = greenAllele2[i].ToString();
            rowDataTemp[11] = blueAllele1[i].ToString();
            rowDataTemp[12] = blueAllele2[i].ToString();
            rowDataTemp[13] = LifeSpan[i].ToString();
            rowDataTemp[15] = turnTorque[i].ToString();
            rowDataTemp[16] = turnDice[i].ToString();
            rowDataTemp[17] = energyToReproduce[i].ToString();

            

            

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
        

        intron1.Clear();
        intron2.Clear();
        intron3.Clear();
        intron4.Clear();

        moveAllele1.Clear();
        moveAllele2.Clear();
        redAllele1.Clear();
        redAllele2.Clear();
        greenAllele1.Clear();
        greenAllele2.Clear();
        blueAllele1.Clear();
        blueAllele2.Clear();
        LifeSpan.Clear();
        lookDistance.Clear();
        turnTorque.Clear();
        turnDice.Clear();
        energyToReproduce.Clear();  
        generation.Clear();
        Array.Clear(Blubs,0,Blubs.Length);
        time = 0f;



    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(){
        #if UNITY_EDITOR
        return Application.dataPath +"/CSV/"+"Blub_genetics.csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Blub_genetics.csv";
        #elif UNITY_STANDALONE_OSX
        return Application.dataPath+"/"+"Blub_genetics.csv";
        #else
        return Application.dataPath +"/"+"Blub_genetics.csv";
        #endif
    }
}


