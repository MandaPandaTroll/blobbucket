using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobControls : MonoBehaviour
{


    Rigidbody2D rb;
   
    bool bump;
    
    float energy;
   float maxEnergy;
    int layer_mask; 

    bool nom;
    int goForwardCount = 0;
    GameObject blibObj;
    BlibControls BlibControls;
    public int blobNumber;
   public static List <BlobControls> blobs;
    Vector3 standardSize = new Vector3 (1f,1f,1f);
    float sigmoid;
    Vector3 newSize;

   // Selection parameters
   float moveForce;
   float turnTorque;
   float lookDistance;
   float energyToReproduce;
   
    // Start is called before the first frame update
    void Start()
    {   
        
        //Initial selection parameters
        moveForce = 100f;
        turnTorque = 90f;
        lookDistance = 5f;
        
        energyToReproduce = 5000f;

        rb = GetComponent<Rigidbody2D>();
        energy = 4500f;
        layer_mask = LayerMask.GetMask("Prey");
        blibObj = GameObject.FindWithTag("Prey");
        BlibControls = blibObj.GetComponent<BlibControls>();
        


       

            if (blobs == null)
                blobs  = new List<BlobControls>();
                blobs.Add(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(blobs.Contains(this) == false)
        {

            blobs.Add(this);
            }

        maxEnergy = 20000f*transform.localScale.x;

        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }


            if (energy <= 0f || transform.localScale.x < 0.2f)
            {
                
                blobs.Remove(this);
                GameObject.Destroy(gameObject);
                
            }
            
            if(energy >= energyToReproduce){
                Rest();

            }


            if(nom == true){
                energy = energy + 250f;
                nom = false; 
                
                Resizer();
            }

        Look();




        if (bump == false && energy > 0f)
        {
        GoForward();
        }


       

            
    }

            void OnCollisionEnter2D(Collision2D col)
            {   
                GameObject booper = col.gameObject;

                if (booper.tag == ("Prey"))
                {
                    nom = true;
                }
                


                
                ContactPoint2D contact = col.GetContact(0);
                bump = true;


                
                Vector2 norm = contact.normal.normalized;
                
                Quaternion turney = new Quaternion();
                turney.Set(0.0f, 0.0f, norm.x, norm.y);
                transform.rotation = turney;
                
                
                
            }

            void OnCollisionStay2D(Collision2D col)
            {
                
                ContactPoint2D contact = col.GetContact(0);
                float thisDir = rb.rotation*Mathf.Deg2Rad;
                Vector2 norm = contact.normal;
                
            
                rb.AddForce(contact.normal * moveForce*5f);
                
                rb.AddTorque((norm.y + norm.x )*Mathf.Rad2Deg);
                
                
                
                rb.AddForce(transform.up * moveForce);
                
                

                



            }

            void OnCollisionExit2D(Collision2D col)
            {
                
                bump = false;
                GoForward();
            }




            void GoForward()

            {     
                goForwardCount = goForwardCount + 1;
                    rb.AddForce(transform.up * moveForce*rb.mass);
                    energy = energy - (0.5f*rb.mass*moveForce/50f);
                    int randTurner = Random.Range(0,128);
                    if (randTurner == 64)
                    {
                        rb.AddTorque(turnTorque* rb.inertia * Random.Range(-1,2),ForceMode2D.Impulse);
                        
                    }

                    if(goForwardCount >= 100)
                    {
                        rb.AddTorque(turnTorque* rb.inertia * Random.Range(-1,2));
                        
                        goForwardCount = 0;
                    }
                    
            }

            void Rest()
            {   
                
                    
                    
                    GameObject clone;

                    //Mutation
                    moveForce = moveForce + Random.Range(-0.1f,0.2f);
                    turnTorque = turnTorque + Random.Range(-0.1f,0.2f);
                    lookDistance = lookDistance + Random.Range(-0.1f,0.2f);
                    energyToReproduce = energyToReproduce + Random.Range(-1f,2f);
                    //Reproduction
                    energy = energy/2f;
                float x = energy/10000f;
                float k = 0.7f;
                sigmoid = 4f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;
                    clone = Instantiate(gameObject);
                    blobs.Add(clone.GetComponent <BlobControls>());
                    
                        
                        rb.AddTorque(turnTorque * rb.inertia * Random.Range(-1,2));
                        
                        GoForward();
                             
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
                    rb.AddForce(transform.up*moveForce);
                    energy = energy - moveForce/50f;
                 
                 }

                if (hitL.collider != null)
                {Vector2 huntDir = new Vector2((hitL.point.x - transform.position.x),(hitL.point.y - transform.position.y) ); 
                
                                       huntDir.Normalize();
                    float rot_z = Mathf.Atan2(huntDir.y, huntDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    rb.AddForce(transform.up*moveForce);
                    energy = energy - moveForce/50f;
                }

                if (hitR.collider != null)
                {Vector2 huntDir = new Vector2((hitR.point.x - transform.position.x),(hitR.point.y - transform.position.y) );
                
                                   huntDir.Normalize();
                    float rot_z = Mathf.Atan2(huntDir.y, huntDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    rb.AddForce(transform.up*moveForce);
                    energy = energy - moveForce/50f;
                }

                    GoForward();

 
            }



            void Resizer()
            {
                float x = energy/10000;
                float k = 0.7f;
                sigmoid = 4f/ (1f+ Mathf.Exp(-k*(x-1.5f)));
                newSize = new Vector3(sigmoid,sigmoid,sigmoid);
                transform.localScale = newSize;
                GoForward();
            }

}
