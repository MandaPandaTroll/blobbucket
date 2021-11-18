//Behaviour and genetics
//tabacwoman november 2021


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubControls : MonoBehaviour
{



// Colour data sent to the sprite renderer.
public float colorR;
public float colorG;
public float colorB;
public float colorA = 1f;
Color geneticColor;

//Script instance genetically related mate involved in conjugation
BlybControls mate;

public float speciationDistance;

    Rigidbody2D rb;
  
   Vector3 newSize;
    private bool bump;
    
    private float energy = 10000f;
    public float pEnergy;
   private float eCost;
    private int layer_mask; 
    private int flee_mask;

    private bool nom;
 
System.Random rndA = new System.Random();

    public float age;
     public float statAge;
   // Selection parameters
     public float moveAllele1;
     public float moveAllele2;
     private float moveForce;
    public float turnTorque;
    public int turnDice;
    public float lookDistance;
    public float  energyToReproduce;

    public float ageToReproduce;
    public float lifeLength;
    public float intron1;
    public float intron2;
    public float intron3;
   public float intron4;
    public float redGene, greenGene, blueGene;
        public float redAllele1, redAllele2,greenAllele1,greenAllele2,blueAllele1,blueAllele2;
     public  float initDiversity;
    public int rDice;
    private int deathDice;
    // Size stuff
  
    private float sigmoid;
    public int generation;

   SpriteRenderer m_SpriteRenderer;

   
    // Start is called before the first frame update
    void Start()
    {   

            
        moveForce = (moveAllele1 + moveAllele2)/2f;
        

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
         geneticColor = m_SpriteRenderer.color;
        layer_mask = LayerMask.GetMask("Predator","Predator2");

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



        eCost = rb.mass/1000f;
                // rCo = 10 + (L/a)^2
       int rCo = 10 + (int)Mathf.Pow((lifeLength/age),2f); 
        
        rDice = Random.Range(1, rCo);

        int dC = (int) ( (lifeLength*Mathf.Pow((3f*lifeLength/age),2f)) - (9f*lifeLength) );
        deathDice = Random.Range(1,dC);
        Look();
        age += Time.deltaTime;
        statAge = age;
            if(energy >= energyToReproduce && rDice == 1){
               
                Reproduce();

            }




            
            if (energy <= 0f || age > lifeLength || deathDice == 1)
            {
                
         
                GameObject.Destroy(gameObject);
                
            }
            



            if(nom == true){
                
                nom = false; 
               
                Resizer();
                
                
            }

        




        if (bump == false && energy > 0f)
        {
            
            
            GoForward();
        }


       

            
    }

            void OnCollisionEnter2D(Collision2D col)
            {   
                GameObject booper = col.gameObject;

                if (booper.tag == ("Predator"))
                {
                    BlobControls blob = booper.GetComponent<BlobControls>();
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
                    BlubControls mate = booper.GetComponent<BlubControls>();
                    float distIntron1 = Mathf.Abs(((float)intron1 - (float)mate.intron1));
                    float distIntron2 = Mathf.Abs(((float)intron2 - (float)mate.intron2));
                    float distIntron3 = Mathf.Abs(((float)intron3 - (float)mate.intron3));
                    float distIntron4 = Mathf.Abs(((float)intron4 - (float)mate.intron4));
                    float geneticDistance = (distIntron1 + distIntron2 + distIntron3 + distIntron4)/4f;
                    if(geneticDistance < 10f)
                    {
                    moveAllele1 = (moveAllele1 + mate.moveAllele1)/2;
                    moveAllele2 = (moveAllele2 + mate.moveAllele2)/2;
                    turnTorque = (turnTorque + mate.turnTorque)/2;
                    turnDice = (turnDice + mate.turnDice)/2;
                    lookDistance = (lookDistance + mate.lookDistance)/2;
                    energyToReproduce = (energyToReproduce + mate.energyToReproduce)/2f;
                    lifeLength = (lifeLength + mate.lifeLength)/2f;

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
                    Debug.Log("blubC" + geneticColor  );

                    }
                }

                
                


                

               
                
                
            }

            void OnCollisionStay2D(Collision2D col)
            {
                GameObject booper = col.gameObject;
                ContactPoint2D contact = col.GetContact(0);
                float thisDir = rb.rotation*Mathf.Deg2Rad;
                Vector2 norm = contact.normal;
                if (booper.tag != ("Predator")|| booper.tag != ("Predator2")){

                
                Vector2 turney = new Vector2((contact.point.x - transform.position.x),(contact.point.y - transform.position.y));
                
                    float rot_z = Mathf.Atan2(turney.y, turney.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                    rb.AddForce(transform.up*moveForce*rb.mass);
                    energy = energy - eCost * moveForce;
                 
                
                }


            }

            void OnCollisionExit2D(Collision2D col)
            {
                
                bump = false;
                GoForward();
            }




            void GoForward()

            {     
                     
                    rb.AddForce(transform.up * moveForce*rb.mass);
                    energy = energy - eCost*moveForce;
                    int randTurner = Random.Range(0,turnDice);
                    if (randTurner == 0)
                    {
                        rb.AddTorque(turnTorque* rb.inertia * Random.Range(-1,2),ForceMode2D.Impulse);
                        rb.AddForce(transform.up * moveForce*rb.mass);
                    }

            }

            void Reproduce()
            {   
                
                    
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
                    turnTorque += randNosA[3]*rndA.Next(2);
                    lookDistance += (float)randNosA[4]*rndA.Next(2);
                    energyToReproduce += (float)randNosA[5]*rndA.Next(2);
                    turnDice += randNosA[6]*rndA.Next(2);

                    intron1 += randNosA[7]*rndA.Next(2);
                    intron2 += randNosA[8]*rndA.Next(2);
                    intron3 += randNosA[9]*rndA.Next(2);
                    intron4 += randNosA[10]*rndA.Next(2);

                    redAllele1   += (float)randNosA[11]*rndA.Next(2)*0.01f;
                    redAllele2   += (float)randNosA[12]*rndA.Next(2)*0.01f;
                    greenAllele1 += (float)randNosA[13]*rndA.Next(2)*0.01f;
                    greenAllele2 += (float)randNosA[14]*rndA.Next(2)*0.01f;
                    blueAllele1  += (float)randNosA[15]*rndA.Next(2)*0.01f;
                    blueAllele2  += (float)randNosA[16]*rndA.Next(2)*0.01f;

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
                    Debug.Log("blubC" + geneticColor  );
                    m_SpriteRenderer.color = geneticColor;
                    //Reproduction
                    energy = (energy/2f);
                    
                
                float x = energy/10000f;
                float k = 0.7f;
                sigmoid = 4f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;

                    clone = Instantiate(this.gameObject);
                    clone.GetComponent<BlubControls>().generation +=1;
                    clone.GetComponent<BlubControls>().age = 0f;
                    if (generation == 100|| generation == 200 || generation == 300 || generation == 400 || generation == 500 || generation == 600 || generation == 800 || generation == 1000)
                    {
                        Debug.Log( 
                            "blubgen "        +
                            generation        + "," + 
                            moveAllele1       + "," +
                            moveAllele2       + "," +
                            turnTorque        + "," +
                            turnDice          + "," +
                            lookDistance      + "," +
                            energyToReproduce 
                                    );
                    }
                    
                   
                    
                        
                        rb.AddTorque(turnTorque * rb.inertia * Random.Range(-1,2));
                        Resizer();
                        
                             
                    }




            void Look()
            {
                
            RaycastHit2D hitF = Physics2D.Raycast(transform.position,transform.up,lookDistance, layer_mask);
            RaycastHit2D hitL = Physics2D.Raycast(transform.position,-transform.right,lookDistance, layer_mask);
            RaycastHit2D hitR = Physics2D.Raycast(transform.position,transform.right,lookDistance, layer_mask);
            if (hitF.collider != null)
                 { Vector2 huntDir = new Vector2((hitF.point.x - transform.position.x),(hitF.point.y - transform.position.y) );
                                          
                                           huntDir.Normalize();
                    float rot_z = Mathf.Atan2(huntDir.y, huntDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    rb.AddForce(transform.up*moveForce*rb.mass);
                    energy = energy - eCost * moveForce;
                 
                 }

                if (hitL.collider != null)
                {Vector2 huntDir = new Vector2((hitL.point.x - transform.position.x),(hitL.point.y - transform.position.y) ); 
                    
                                       huntDir.Normalize();
                    float rot_z = Mathf.Atan2(huntDir.y, huntDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    rb.AddForce(transform.up*moveForce*rb.mass);
                    energy = energy - eCost  * moveForce;
                }

                if (hitR.collider != null)
                {Vector2 huntDir = new Vector2((hitR.point.x - transform.position.x),(hitR.point.y - transform.position.y) );
                
                                   huntDir.Normalize();
                    float rot_z = Mathf.Atan2(huntDir.y, huntDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    rb.AddForce(transform.up*moveForce*rb.mass);
                    energy = energy - eCost * moveForce;
                }





                



                    GoForward();

 
            }

            void Resizer()
            {
                float x = energy/10000;
                float k = 0.7f;
                sigmoid = 6f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;
                GoForward();
            }

        void InitDiversifier()
        {
                       

                    //Mutation
                    moveAllele1 += (float)rndA.Next(-1,2)*initDiversity;
                    moveAllele2 += (float)rndA.Next(-1,2)*initDiversity;
                    lifeLength += (float)rndA.Next(-1,2)*initDiversity;
                    moveForce = (moveAllele1 + moveAllele2)/2.0f;
                    turnTorque += rndA.Next(-1,2)*initDiversity;
                    lookDistance += (float)rndA.Next(-1,2)*initDiversity;
                    energyToReproduce += (float)rndA.Next(-1,2)*initDiversity;
                    turnDice += rndA.Next(-1,2)*(int)initDiversity;

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
