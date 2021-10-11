using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlibControls : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject box;
    
    EdgeCollider2D boxCol;
    
    Collider2D thisBlib;
   
    bool bump;
    static GameObject[] blibs;
    int energy;
    float restTime;
   float lookTimer;
    int layer_mask; 
    int blib_mask;
    int rDice;
   
    int diceSize;
    // Selection parameters
   float moveForce;
   float turnTorque;
   float lookDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        //Initial selection parameters
        moveForce = 100f;
        turnTorque = 90f;
        lookDistance = 5f;
        diceSize = 512;
        rb = GetComponent<Rigidbody2D>();
        box = GameObject.Find("box");
        boxCol = box.GetComponent<EdgeCollider2D>();
        energy = 500;
        layer_mask = LayerMask.GetMask("Predator");
        thisBlib = GetComponent<CircleCollider2D>();
        blib_mask = LayerMask.GetMask("Prey");

    }




    // Update is called once per frame
    void Update()
    {
                    blibs =  GameObject.FindGameObjectsWithTag("Prey");
                    diceSize =100 + blibs.Length*blibs.Length/16;
     
       rDice=(Random.Range(0,diceSize));
        if(rDice == 69){
            Reproduce();
        }


        if (bump == false && energy > 0)
        {
        GoForward();
        }


            



            

    }



            void OnCollisionEnter2D(Collision2D col)

            {       
                
                GameObject dangerBooper = col.gameObject;

                if(dangerBooper.tag == ("Predator"))
                {   
                    Destroy(gameObject, 0.2f);
                    
        
                }
                

                ContactPoint2D contact = col.GetContact(0);
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
                    rb.AddForce(transform.up * moveForce);
                    
                    int randTurner = Random.Range(0,128);
                    if (randTurner == 64)
                    {
                        rb.AddTorque(turnTorque * Random.Range(-1,2f));
                        
                    }
                    
                    
            }

            void Reproduce()
            {   
                    if (bump== true)
                    {
                        return;
                    }
                    


                        
                        if(bump == false ){
                        
                        GameObject clone;
                        
                        
                    //Mutation
                    moveForce = moveForce + Random.Range(-0.1f,0.2f);
                    turnTorque = turnTorque + Random.Range(-0.1f,0.2f);
                    lookDistance = lookDistance + Random.Range(-0.1f,0.2f);

                    clone = Instantiate(gameObject);
                    

                    }
                        
                        
                }
                
            





}
