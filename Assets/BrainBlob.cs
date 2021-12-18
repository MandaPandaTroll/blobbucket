using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BrainBlob : Agent
{

RayPerceptionSensorComponent2D thisRay;
Rigidbody2D rb;
BrainBlobControls bctrl;
bool alive = true;
bool eaten = false;
bool hasReproduced = false;
bool starvation;

bool bump;
GameObject box;
public static float boxExp;
public static float boxScale;
BlibControls[] blibControls;

GameObject extBooper;


void Start() 
{
    box = GameObject.Find("box");
    rb = GetComponent<Rigidbody2D>();
    bctrl = gameObject.GetComponent<BrainBlobControls>();
    energy = bctrl.energy;
    thisRay = GetComponent<RayPerceptionSensorComponent2D>();
    thisRay.RayLength = bctrl.lookDistance;


    

   
}


public override void OnEpisodeBegin()
{


}




public override void CollectObservations(VectorSensor sensor)
{

if (bump == false)
{
    extBooper = null;
}
float v = rb.velocity.magnitude/1000.0f;
float angV = rb.angularVelocity/1000.0f;
sensor.AddObservation(v);
sensor.AddObservation(angV);


sensor.AddObservation(bctrl.energy/bctrl.energyToReproduce);
sensor.AddObservation(bctrl.age);
sensor.AddObservation(bump);
sensor.AddObservation(extBooper);




    
}
float moveForce, turnTorque;
float forwardSignal, rotSignal;
float energy;
public override void OnActionReceived(ActionBuffers actionBuffers)
{  
    if(bctrl.age >= bctrl.lifeLength*0.99f)
    {
        SetReward(1.0f);
        EndEpisode();
    }
     energy = bctrl.energy;
alive = bctrl.alive;
eaten = bctrl.eaten;
hasReproduced = bctrl.hasReproduced;


    moveForce = bctrl.moveForce;
    turnTorque = bctrl.turnTorque;
    forwardSignal = actionBuffers.DiscreteActions[0];
    rotSignal = actionBuffers.DiscreteActions[1];
    float fwdMag = 0;
    float rotMag = 0;

    if(forwardSignal == 0)
    {
        fwdMag = -1.0f;
    }
     if(forwardSignal == 1)
    {
        fwdMag = -0.5f;
    }
     if(forwardSignal == 2)
    {
        fwdMag = 0.0f;
    }
     if(forwardSignal == 3)
    {
        fwdMag = 0.5f;
    }
     if(forwardSignal == 4)
    {
        fwdMag = 1.0f;
    }
     if(forwardSignal == 5)
    {
        fwdMag = 1.5f;
    }
     if(forwardSignal == 6)
    {
        fwdMag = 2.0f;
    }
     if(forwardSignal == 7)
    {
        fwdMag = 3.0f;
    }
     if(forwardSignal == 8)
    {
        fwdMag = 4.0f;
    }

    if(rotSignal == 0)
    {
        rotMag = -2.0f;
    }
     if(rotSignal == 1)
    {
        rotMag = -0.75f;
    }
     if(rotSignal == 2)
    {
        rotMag = -0.5f;
    }
     if(rotSignal == 3)
    {
        rotMag = -0.25f;
    }
     if(rotSignal == 4)
    {
        rotMag = 0.0f;
    }
     if(rotSignal == 5)
    {
        rotMag = 0.25f;
    }
     if(rotSignal == 6)
    {
        rotMag = 0.5f;
    }
     if(rotSignal == 7)
    {
        rotMag = 0.75f;
    }
     if(rotSignal == 8)
    {
        rotMag = 2.0f;
    }


 Vector2 fwd = transform.up*(fwdMag)*moveForce*rb.mass;
 
    if(alive == true)
    {
 rb.AddForce(fwd);
 rb.AddTorque(rotMag*turnTorque*rb.inertia);
 bctrl.energy -=  bctrl.eCost*Mathf.Abs(fwd.magnitude);


 
        if(bctrl.energy<= 101f)
        {
        
            SetReward(-1.0f);
            EndEpisode();
        }

    if(bctrl.hasReproduced == true)
    {
        SetReward(1.0f);
    
        bctrl.hasReproduced = false;
    }



    if(bctrl.age >= bctrl.lifeLength*0.99)
    {
        SetReward(1.0f);
        EndEpisode();
    }



    


        }
}
  void OnCollisionEnter2D(Collision2D col)
{
    
    bump = true;
    GameObject booper = col.gameObject;
    
    if(alive == true )
    {
        extBooper = booper;
     if (booper.tag == "ApexPred")
        {
            SetReward(-1.0f);
            EndEpisode();
        }

             if (booper.tag == "Predator" && energy >= bctrl.energyToReproduce*0.75f && bctrl.geneticDistance > 0.2f)
            {
             SetReward(bctrl.geneticDistance);
                EndEpisode();
            }

            if(booper.tag == "Predator" && bctrl.geneticDistance < 0.2f)
            {
                SetReward(-bctrl.geneticDistance);
            }

         if (booper.tag == "Prey" || booper.tag == "Carcass" )
         {
            SetReward(1.0f);
            EndEpisode();     
         }

        if(booper.tag == "Wall")
         {
         
                SetReward(-1.0f);
                EndEpisode();
                Destroy(gameObject, 0.2f);
        }    

    }

        
}



    void OnCollisionExit2D(Collision2D col)
    {
        bump = false;
        
    }



public override void Heuristic(in ActionBuffers actionsOut)
{

}


}