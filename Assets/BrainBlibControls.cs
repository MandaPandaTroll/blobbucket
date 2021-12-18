//Behaviour and genetics
//tabacwoman november 2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrainBlibControls : MonoBehaviour 
{

public bool hasReproduced;
bool touch1;
bool touch2;
bool touch3;
bool touch4;
Detector detector;
GameObject Alpha;
Collider2D AlphaCol;

int AlphaPop;

int BetaPop;

int GammaPop;

int DeltaPop;

Vector2 pos;

public float speciationDistance;
public float energyToReproduce;
private float colorR;
private float colorG;
private float colorB;
private float colorA = 1f;

private Color geneticColor;
public float densityCo;
    Rigidbody2D rb;
    GameObject box;
    public  bool bump;
    static GameObject[] blibs;
    private  int layer_mask; 
    private   int blib_mask;
    public  int rDice;
    public  float age;
    public float energy;
    public float statAge;
    System.Random rndA = new System.Random();
    
  
      

    // Selection parameters
   public float moveAllele1;
   public float moveAllele2;

   public float moveForce;
   public float turnTorque;
   public float turnTorqueAllele1;
   public float turnTorqueAllele2;
   public float intron1;
   public float intron2;
   public float intron3;
   public float intron4;

   public float redGene, greenGene, blueGene;
   public float redAllele1, redAllele2,greenAllele1,greenAllele2,blueAllele1,blueAllele2;
    public float initDiversity;
  public float lifeLength;
 int deathDice;

  public float lookDistance;
    public float geneticDistance;

  Transform boxTran;
  float boxLength;
  public int generation;

    SpriteRenderer m_SpriteRenderer;
  
    
float energyTick;

    public Transform lightSource;
    void Start()
    {

       hasReproduced = false;

       Alpha = GameObject.Find("Alpha");
        detector = Alpha.GetComponent<Detector>();


        
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        geneticColor = m_SpriteRenderer.color;
        moveForce = (moveAllele1 + moveAllele2)/2.0f;
        rb = GetComponent<Rigidbody2D>();
       
        layer_mask = LayerMask.GetMask("Predator","Predator2");
        blib_mask = LayerMask.GetMask("Prey");
        box = GameObject.Find("box");
        boxTran = box.GetComponent<Transform>();
        boxLength = boxTran.localScale.x;

        
        
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

        AlphaPop = detector.in1;
        BetaPop = detector.in2;
        GammaPop = detector.in3;
        DeltaPop = detector.in4;

        
        
    }


    // Update is called once per frame
    void LateUpdate()
    {   
        Vector2 brownian = new Vector2 (Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f));
        rb.AddForce(brownian);
         energyTick += Time.deltaTime;
        if(energyTick > 1.0f)
        {
            float distLightScaled = Vector2.Distance(this.transform.position, lightSource.position)/Mathf.Sqrt(2.0f*(Mathf.Pow(2.0f*boxLength,2.0f))); 
            energy += 100f*greenGene;
            energyTick = 0.0f;
        }
        if(Time.time < 0.1f && initDiversity != 0.0f)
        {
            age = 0f + Random.Range(0f, lifeLength/2.0f);
            energy = Random.Range(0f, energyToReproduce);
            InitDiversifier(); 
        }
        
        pos = new Vector2(transform.position.x,transform.position.y);
        if (pos.x < 0f && pos.y > 0f){touch1= true;}
        if (pos.x > 0f && pos.y > 0f){touch2= true;}
        if (pos.x < 0f && pos.y < 0f){touch3= true;}
        if (pos.x > 0f && pos.y < 0f){touch4= true;}

        AlphaPop = detector.in1;
        BetaPop = detector.in2;
        GammaPop = detector.in3;
        DeltaPop = detector.in4;
        int aCo = (int)Mathf.Pow(2,((AlphaPop*32f)/boxLength));
        int bCo = (int)Mathf.Pow(2,((AlphaPop*32f)/boxLength));
        int gCo = (int)Mathf.Pow(2,((AlphaPop*32f)/boxLength));
        int dCo = (int)Mathf.Pow(2,((AlphaPop*32f)/boxLength));
       

        
        int dC = (int) ( (lifeLength*Mathf.Pow((3f*lifeLength/age),2f)) - (9f*lifeLength) );
        deathDice = Random.Range(1,dC);
        
        age += Time.deltaTime;
                   

       
        // rAgeC = 10 + (L/a)^2 
            int rAgeC = 10 + (int)(Mathf.Pow((10f*lifeLength/age*greenGene),2f));
         int  rAgeDice = Random.Range(0,rAgeC);
        if(touch1 == true)
                        {
                          
                          rDice = Random.Range(1, (2 + aCo));

                        }                                                          
       if(touch2 == true)
                        {
                          
                          rDice = Random.Range(1, (2 + bCo));
                        }

       if(touch3 == true)
                        {
                          
                            rDice =Random.Range(1, (2 + gCo));
                        }

       if(touch4 == true)
                        {
                          
                         rDice =Random.Range(1, (2 + dCo));
                        }


         if ( touch1 == false && touch2 == false && touch3 == false && touch4 == false)
                        {
                          rDice =Random.Range(0, ((int)Mathf.Exp((float)blibs.Length*densityCo/4)));
                        }
                    
        statAge = age;
       
        if( rDice == 1 && rAgeDice == 1 && energy >= energyToReproduce){
            
            Reproduce();
            
        }





            if(deathDice == 1 && age > (lifeLength/2) || age >= lifeLength)
            {   
                this.GetComponent<BrainBlib>().enabled = false;
                Destroy(gameObject, 0.2f);
            
            }



            

    }



            void OnCollisionEnter2D(Collision2D col)

            {       
                bump = true;
                GameObject booper = col.gameObject;

                if(booper.tag == ("Predator") || booper.tag == "Predator2")
                {   
                    Destroy(gameObject, 0.2f);
                    
        
                }

                    if(booper.tag == "Prey")
                {
                    BlibControls mate;
                    mate = booper.GetComponent<BlibControls>();
                    float distIntron1 = Mathf.Abs(((float)intron1 - (float)mate.intron1));
                    float distIntron2 = Mathf.Abs(((float)intron2 - (float)mate.intron2));
                    float distIntron3 = Mathf.Abs(((float)intron3 - (float)mate.intron3));
                    float distIntron4 = Mathf.Abs(((float)intron4 - (float)mate.intron4));
                    geneticDistance = (distIntron1 + distIntron2 + distIntron3 + distIntron4)/4f;
                    if(geneticDistance > speciationDistance){Debug.Log("blibGenDist = " + geneticDistance);}
                    if(geneticDistance < speciationDistance)
                    {
                    moveAllele1 = (moveAllele1 + mate.moveAllele1)/2.0f;
                    moveAllele2 = (moveAllele2 + mate.moveAllele2)/2.0f;

                    turnTorqueAllele1 = (turnTorqueAllele1 + mate.turnTorqueAllele1)/2;
                    turnTorqueAllele2 = (turnTorqueAllele2 + mate.turnTorqueAllele2)/2;
                    
                    energyToReproduce = (energyToReproduce + mate.energyToReproduce)/2.0f;

                    lookDistance = (lookDistance + mate.lookDistance)/2.0f;

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
                

                ContactPoint2D contact = col.GetContact(0);
                Vector2 norm = contact.normal.normalized;
                
                
                Quaternion turney = new Quaternion();
                turney.Set(0.0f, 0.0f, norm.x, norm.y);
                transform.rotation = turney;
                
                
                
            }










            void Reproduce()
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
                    moveForce = (moveAllele1 + moveAllele2)/2.0f;

                    turnTorqueAllele1 += (float)randNosA[2]*rndA.Next(2);
                    turnTorqueAllele2 += (float)randNosA[3]*rndA.Next(2);
                    turnTorque = (turnTorqueAllele1 + turnTorqueAllele2)/2.0f;
                    intron1 += randNosA[4]*rndA.Next(2);
                    intron2 += randNosA[5]*rndA.Next(2);
                    intron3 += randNosA[6]*rndA.Next(2);
                    intron4 += randNosA[7]*rndA.Next(2);
                    lifeLength += (float)randNosA[8]*rndA.Next(2);

                    lookDistance += (float)randNosA[9]*rndA.Next(2);
                    

                    redAllele1   += (float)randNosA[10]*rndA.Next(2)*0.01f;
                    redAllele2   += (float)randNosA[11]*rndA.Next(2)*0.01f;
                    greenAllele1 += (float)randNosA[12]*rndA.Next(2)*0.01f;
                    greenAllele2 += (float)randNosA[13]*rndA.Next(2)*0.01f;
                    blueAllele1  += (float)randNosA[14]*rndA.Next(2)*0.01f;
                    blueAllele2  += (float)randNosA[15]*rndA.Next(2)*0.01f;

                    energyToReproduce += (float)randNosA[16]*rndA.Next(2);

                    redGene = (redAllele1+redAllele2)/2.0f;
                    greenGene = (greenAllele1+greenAllele2)/2.0f;
                    blueGene = (blueAllele1+blueAllele2)/2.0f;

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
                    

                                        

                    

                
                    energy = energy/2f;
                    age = 0f;
                    clone = Instantiate(this.gameObject);
                    
                    clone.GetComponent<BlibControls>().generation = generation + 1;
                    clone.GetComponent<BlibControls>().age = 0f;

                    

                    
                        
                        
                }
                
            

        void InitDiversifier()
        {
                        

                    //Mutation
                    moveAllele1 += (float)(rndA.Next(-1,2)*initDiversity);
                    moveAllele2 += (float)(rndA.Next(-1,2)*initDiversity);
                    moveForce = ((moveAllele1 + moveAllele2)/2.0f);

                    turnTorqueAllele1 += (float)(rndA.Next(-1,2)*initDiversity);
                    turnTorqueAllele2 += (float)(rndA.Next(-1,2)*initDiversity);
                    turnTorque = (turnTorqueAllele1 + turnTorqueAllele2)/2.0f;
                    
                    lookDistance += (float)(rndA.Next(-1,2)*initDiversity);

                    intron1 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron2 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron3 += (float)(rndA.Next(-1,2)*initDiversity);
                    intron4 += (float)(rndA.Next(-1,2)*initDiversity);
                    lifeLength += (float)(rndA.Next(-1,2)*initDiversity);
                    energyToReproduce += (float)(rndA.Next(-1,2)*initDiversity);

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
