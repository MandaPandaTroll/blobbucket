//Behaviour and genetics
//tabacwoman november 2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBlubControls : MonoBehaviour
{
public float geneticDistance;
public int rCount = 0;
public bool hasReproduced;
public bool alive;
public bool eaten;
public bool nom;
public float eCostCo;
// Colour data sent to the sprite renderer.
float colorR;
float colorG;
float colorB;
float colorA = 1f;
Color geneticColor;

//Script instance genetically related mate involved in conjugation
BrainBlubControls mate;

public float speciationDistance;

    Rigidbody2D rb;
  
   Vector3 newSize;

    
    public float energy = 4500f;
    public float pEnergy;
    public float eCost;

    public float conjAge;

 
System.Random rndA = new System.Random();

    public float age;
     public float statAge;
   // Selection parameters
     public float moveAllele1;
     public float moveAllele2;
     public float moveForce;
    public float turnTorque;
    public float turnTorqueAllele1;
    public float turnTorqueAllele2;

    public float lookDistance;
    public float  energyToReproduce;
    

    public float lifeLength;
    public float intron1;
    public float intron2;
    public float intron3;
    public float intron4;
    public float redGene, greenGene, blueGene;
    public float redAllele1, redAllele2,greenAllele1,greenAllele2,blueAllele1,blueAllele2;

    public float initDiversity;
    public int rDice;
    private int deathDice;
    // Size stuff
  
    private float sigmoid;
    public int generation;
    public int deadLayer = 9;

   SpriteRenderer m_SpriteRenderer;
    public float basalMet;

    // Start is called before the first frame update
    void Start()
    {  
        eaten = false;
        alive = true;
        hasReproduced = false;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        moveForce = (moveAllele1 + moveAllele2)/2.0f;
        geneticColor = m_SpriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();

      

        redGene = geneticColor.r;
        greenGene = geneticColor.g;
        blueGene = geneticColor.b;

        redAllele1 = redGene;
        redAllele2 = redGene;
        greenAllele1 = greenGene;
        greenAllele2 = greenGene;
        blueAllele1 = blueGene;
        blueAllele2 = blueGene;

        redGene = (redAllele1+redAllele2)/2.0f;
        greenGene = (greenAllele1+greenAllele2)/2.0f;
        blueGene = (blueAllele1+blueAllele2)/2.0f;


        
        Resizer();
    }



    // Update is called once per frame
    void LateUpdate()
    {   
        
        if(Time.time < 0.1f && initDiversity != 0.0f){InitDiversifier(); }
        
        pEnergy = energy;
        if(alive == false)
        {  this.gameObject.tag = "Carcass";
            this.gameObject.layer = deadLayer;
            


            Dead();
        }
    if(alive == true)
    {
        eCost = rb.mass/eCostCo;
        energy -= basalMet;
        int dC = (int) ( (lifeLength*Mathf.Pow((3f*lifeLength/age),2f)) - (9f*lifeLength) );
        deathDice = Random.Range(1,dC);
                // rCo = 10 + (L/a)^2
       int rCo = 10 + (int)Mathf.Pow((lifeLength/age),2f); 
        
        rDice = Random.Range(1, rCo);
        
        age += Time.deltaTime;
        statAge = age;
         
        


            if(energy >= energyToReproduce && rDice == 1){
                
                Reproduce();

            }



        
            if  ( energy <= 100f || age > lifeLength || deathDice == 1)
            {
                
                alive = false;
                   
            }
            



            if(nom == true){
                
                nom = false; 
               
                Resizer();
                
                
            }

        
        }

       

            
    }

            void Dead()
        {   
            energy -= 100f*Time.deltaTime;
            this.GetComponent<BrainBlub>().enabled = false;
            if(energy <= 0f)
            {
                GameObject.Destroy(gameObject);
            }

        }

            void OnCollisionEnter2D(Collision2D col)
            {   
                GameObject booper = col.gameObject;
             if(alive == false)
                {
                     if(booper.tag == ("ApexPred"))
                    {
                        energy -= energy/2f;
                    }
                    energy -= 100f + energy/10f;
                }

                if( alive == true){
                if(booper.tag == ("Carcass"))
                {               
                    if(booper.layer == 6)
                    {
                        energy += (booper.GetComponent<BrainBlobControls>().pEnergy);
                    }
                    if(booper.layer == 7)
                    {
                        energy += 100f + (booper.GetComponent<BlubControls>().pEnergy)/2f;
                    }
                    if(booper.layer == 8)
                    {
                        energy += (booper.GetComponent<BlybControls>().pEnergy);
                    }
                    
                    nom = true; 
                }
                if (booper.tag == ("Predator"))
                {
                    BrainBlobControls blob = booper.GetComponent<BrainBlobControls>();
                   energy += blob.pEnergy;
                  
                    nom = true;
                }

                if (booper.tag == ("Predator2"))
                {
                    BlybControls blyb = booper.GetComponent<BlybControls>();
                   energy += blyb.pEnergy;
                    nom = true;
                }



                if(booper.tag == "ApexPred")
                {
                    mate = booper.GetComponent<BrainBlubControls>();
                    float distColR1 = Mathf.Abs(((float)redAllele1 - (float)mate.redAllele1));
                    float distColR2 = Mathf.Abs(((float)redAllele2 - (float)mate.redAllele2));
                    float distColG1 = Mathf.Abs(((float)greenAllele1 - (float)mate.greenAllele1));
                    float distColG2 = Mathf.Abs(((float)greenAllele2 - (float)mate.greenAllele2));
                    float distColB1 = Mathf.Abs(((float)blueAllele1 - (float)mate.blueAllele1));
                    float distColB2 = Mathf.Abs(((float)blueAllele2 - (float)mate.blueAllele2));
                     geneticDistance = (distColR1 + distColR2 + distColG1 + distColG2 + distColB1 + distColB2)/6f;
                    if(geneticDistance < speciationDistance)
                    {
                    moveAllele1 = (moveAllele1 + mate.moveAllele1)/2.0f;
                    moveAllele2 = (moveAllele2 + mate.moveAllele2)/2.0f;

                    turnTorqueAllele1 = (turnTorqueAllele1 + mate.turnTorqueAllele1)/2;
                    turnTorqueAllele2 = (turnTorqueAllele2 + mate.turnTorqueAllele2)/2;
                    
                    lookDistance = (lookDistance + mate.lookDistance)/2f;      
 
                    energyToReproduce = (energyToReproduce + mate.energyToReproduce)/2.0f;
                    lifeLength = (lifeLength + mate.lifeLength)/2.0f;

                    intron1 = (intron1 + mate.intron1)/2;
                    intron2 = (intron2 + mate.intron2)/2;
                    intron3 = (intron3 + mate.intron3)/2;
                    intron4 = (intron4 + mate.intron4)/2;

                    redAllele1   = (redAllele1 + mate.redAllele1)/2.0f;
                    redAllele2   = (redAllele1 + mate.redAllele1)/2.0f;
                    greenAllele1 = (greenAllele1 + mate.greenAllele1)/2.0f;
                    greenAllele2 = (greenAllele1 + mate.greenAllele1)/2.0f;
                    blueAllele1  = (blueAllele1 + mate.blueAllele1)/2.0f;
                    blueAllele2  = (blueAllele1 + mate.blueAllele1)/2.0f;

                    redGene = (redAllele1+redAllele2)/2.0f;
                    greenGene = (greenAllele1+greenAllele2)/2.0f;
                    blueGene = (blueAllele1+blueAllele2)/2.0f;

                    conjAge = (conjAge + mate.conjAge)/2f;

        if(redAllele1 < 0.0f){redAllele1 = 0.0f;}
        if(redAllele2 < 0.0f){redAllele2 = 0.0f;}
        if(greenAllele1 < 0.0f){greenAllele1 = 0.0f;}
        if(greenAllele2 < 0.0f){greenAllele2 = 0.0f;}
        if(blueAllele1 < 0.0f){blueAllele1 = 0.0f;}
        if(blueAllele2 < 0.0f){blueAllele2 = 0.0f;}

                    if(redAllele1 > 1.0f){redAllele1 = 1.0f;}
                    if(redAllele2 > 1.0f){redAllele2 = 1.0f;}
                    if(greenAllele1 > 1.0f){greenAllele1 = 1.0f;}
                    if(greenAllele2 > 1.0f){greenAllele2 = 1.0f;}
                    if(blueAllele1 > 1.0f){blueAllele1 = 1.0f;}
                    if(blueAllele2 > 1.0f){blueAllele2 = 1.0f;}

                    if (redGene < 0.0f){
                        redGene = 0.0f;
                    }
                    if (redGene > 1.0f){
                        redGene = 1.0f;
                    }
                    if (greenGene < 0.0f){
                        greenGene = 0.0f;
                    }
                    if (greenGene > 1.0f){
                        greenGene = 1.0f;
                    }
                    if (blueGene < 0.0f){
                        blueGene = 0.0f;
                    }
                    if (blueGene > 1.0f){
                        blueGene = 1.0f;
                    }
                    
                    colorR = redGene;
                    colorG = greenGene;
                    colorB = blueGene;

                    geneticColor = new Color(colorR, colorG, colorB, colorA);
                    m_SpriteRenderer.color = geneticColor;
                    }

                    }
                
                
                    


                
            }
                
                
                
            }









            void Reproduce()
            {   
                if (alive == true)
                {
                    hasReproduced = true;
                        List <int> randNosA = new List<int>();
                         
                        
                        for(int i = 0; i < 17; i++){
                            randNosA.Add(rndA.Next(-1,2));
                        }

                    GameObject clone;

                    //Mutation
                    moveAllele1 += (float)randNosA[0]*rndA.Next(2);
                    moveAllele2 += (float)randNosA[1]*rndA.Next(2);
                    lifeLength += (float)randNosA[2]*rndA.Next(2);
                    moveForce = (moveAllele1 + moveAllele2)/2.0f;

                    turnTorqueAllele1 += (float)randNosA[3]*rndA.Next(2);
                    turnTorqueAllele2 += (float)randNosA[4]*rndA.Next(2);
                    turnTorque = (turnTorqueAllele1 + turnTorqueAllele2)/2.0f;
                    lookDistance += (float)randNosA[5]*rndA.Next(2);
                    energyToReproduce += (float)randNosA[5]*rndA.Next(2);
                    

                    intron1 += randNosA[6]*rndA.Next(2);
                    intron2 += randNosA[7]*rndA.Next(2);
                    intron3 += randNosA[8]*rndA.Next(2);
                    intron4 += randNosA[9]*rndA.Next(2);

                    redAllele1   += (float)randNosA[10]*rndA.Next(2)*0.01f;
                    redAllele2   += (float)randNosA[11]*rndA.Next(2)*0.01f;
                    greenAllele1 += (float)randNosA[12]*rndA.Next(2)*0.01f;
                    greenAllele2 += (float)randNosA[13]*rndA.Next(2)*0.01f;
                    blueAllele1  += (float)randNosA[14]*rndA.Next(2)*0.01f;
                    blueAllele2  += (float)randNosA[15]*rndA.Next(2)*0.01f;

                    conjAge += (float)randNosA[16]*rndA.Next(2);


                    if(redAllele1 < 0.0f){redAllele1 = 0.0f;}
                    if(redAllele2 < 0.0f){redAllele2 = 0.0f;}
                    if(greenAllele1 < 0.0f){greenAllele1 = 0.0f;}
                    if(greenAllele2 < 0.0f){greenAllele2 = 0.0f;}
                    if(blueAllele1 < 0.0f){blueAllele1 = 0.0f;} 
                    if(blueAllele2 < 0.0f){blueAllele2 = 0.0f;}

                    if(redAllele1 > 1.0f){redAllele1 = 1.0f;}
                    if(redAllele2 > 1.0f){redAllele2 = 1.0f;}
                    if(greenAllele1 > 1.0f){greenAllele1 = 1.0f;}
                    if(greenAllele2 > 1.0f){greenAllele2 = 1.0f;}
                    if(blueAllele1 > 1.0f){blueAllele1 = 1.0f;}
                    if(blueAllele2 > 1.0f){blueAllele2 = 1.0f;} 

                    redGene = (redAllele1+redAllele2)/2.0f;
                    greenGene = (greenAllele1+greenAllele2)/2.0f;
                    blueGene = (blueAllele1+blueAllele2)/2.0f;



                    if (redGene < 0.0f){
                        redGene = 0.0f;
                    }
                    if (redGene > 1.0f){
                        redGene = 1.0f;
                    }
                    if (greenGene < 0.0f){
                        greenGene = 0.0f;
                    }
                    if (greenGene > 1.0f){
                        greenGene = 1.0f;
                    }
                    if (blueGene < 0.0f){
                        blueGene = 0.0f;
                    }
                    if (blueGene > 1.0f){
                        blueGene = 1.0f;
                    }
                    
                    colorR = redGene;
                    colorG = greenGene;
                    colorB = blueGene;

                    geneticColor = new Color(colorR, colorG, colorB, colorA);
                    m_SpriteRenderer.color = geneticColor;
                    

                    //Reproduction
                    energy = (energy/2.0f);
                    
                
                float x = energy/10000f;
                float k = 0.7f;
                sigmoid = 4f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;
                    
                    if (generation == 100|| generation == 200 || generation == 300 || generation == 400 || generation == 500 || generation == 600 || generation == 800 || generation == 1000)
                    {
                        Debug.Log( 
                            "Blubgen "        +
                            generation        + "," + 
                            moveAllele1       + "," +
                            moveAllele2       + "," +
                            turnTorque        + "," +
                            energyToReproduce 
                                    );
                    }
                    hasReproduced = false;
                    clone = Instantiate(this.gameObject);
                    clone.GetComponent<BrainBlubControls>().generation +=1;
                    clone.GetComponent<BrainBlubControls>().age = 0f;
                    rCount += 1;
                        
                        
                        
                        Resizer();

                }    
                    }





            void Resizer()
            {
                float x = energy/10000;
                float k = 0.7f;
                sigmoid = 4f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;
                
            }

        void InitDiversifier()
        {
                       

                    //Mutation
                    moveAllele1 += (float)rndA.Next(-1,2)*initDiversity;
                    moveAllele2 += (float)rndA.Next(-1,2)*initDiversity;
                    lifeLength += (float)rndA.Next(-1,2)*initDiversity;
                    moveForce = (moveAllele1 + moveAllele2)/2.0f;

                    turnTorqueAllele1 += (float)(rndA.Next(-1,2)*initDiversity);
                    turnTorqueAllele2 += (float)(rndA.Next(-1,2)*initDiversity);
                    turnTorque = (turnTorqueAllele1 + turnTorqueAllele2)/2.0f;


                    energyToReproduce += (float)rndA.Next(-1,2)*initDiversity;


                    intron1 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron2 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron3 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron4 += (float)(rndA.Next(-1,2)*initDiversity);

                    redAllele1   += (float)(rndA.Next(-1,2)*0.01f*initDiversity);
                    redAllele2   += (float)(rndA.Next(-1,2)*0.01f*initDiversity);
                    greenAllele1 += (float)(rndA.Next(-1,2)*0.01f*initDiversity);
                    greenAllele2 += (float)(rndA.Next(-1,2)*0.01f*initDiversity);
                    blueAllele1  += (float)(rndA.Next(-1,2)*0.01f*initDiversity);
                    blueAllele2  += (float)(rndA.Next(-1,2)*0.01f*initDiversity);

                    if(redAllele1 < 0.0f){redAllele1 = 0.0f;}
                    if(redAllele2 < 0.0f){redAllele2 = 0.0f;}
                    if(greenAllele1 < 0.0f){greenAllele1 = 0.0f;}
                    if(greenAllele2 < 0.0f){greenAllele2 = 0.0f;}
                    if(blueAllele1 < 0.0f){blueAllele1 = 0.0f;}
                    if(blueAllele2 < 0.0f){blueAllele2 = 0.0f;}

                    if(redAllele1 > 1.0f){redAllele1 = 1.0f;}
                    if(redAllele2 > 1.0f){redAllele2 = 1.0f;}
                    if(greenAllele1 > 1.0f){greenAllele1 = 1.0f;}
                    if(greenAllele2 > 1.0f){greenAllele2 = 1.0f;}
                    if(blueAllele1 > 1.0f){blueAllele1 = 1.0f;}
                    if(blueAllele2 > 1.0f){blueAllele2 = 1.0f;}
                    
                    redGene = (redAllele1+redAllele2)/2.0f;
                    greenGene = (greenAllele1+greenAllele2)/2.0f;
                    blueGene = (blueAllele1+blueAllele2)/2.0f;



        } 


}
