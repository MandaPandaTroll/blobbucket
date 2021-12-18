using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BrainBlub : Agent
{

RayPerceptionSensorComponent2D thisRay;
Rigidbody2D rb;
BrainBlubControls bctrl;
bool alive = true;
bool eaten = false;
bool hasReproduced = false;
bool starvation;

void Start() 
{
    rb = GetComponent<Rigidbody2D>();
    bctrl = gameObject.GetComponent<BrainBlubControls>();
    energy = bctrl.energy;
    thisRay = GetComponent<RayPerceptionSensorComponent2D>();
    thisRay.RayLength = bctrl.lookDistance;

}


public override void OnEpisodeBegin()
{



}




public override void CollectObservations(VectorSensor sensor)
{

float v = rb.velocity.magnitude/1000.0f;
float angV = rb.angularVelocity/1000.0f;
sensor.AddObservation(v);
sensor.AddObservation(angV);


sensor.AddObservation(energy/bctrl.energyToReproduce);
sensor.AddObservation(starvation);

}
float moveForce, turnTorque;
float forwardSignal, rotSignal;
float energy;
public override void OnActionReceived(ActionBuffers actionBuffers)
{   energy = bctrl.energy;
alive = bctrl.alive;
eaten = bctrl.eaten;
hasReproduced = bctrl.hasReproduced;




    moveForce = bctrl.moveForce;
    turnTorque = bctrl.turnTorque;
 forwardSignal = actionBuffers.ContinuousActions[0];
 rotSignal = actionBuffers.ContinuousActions[1];
 Vector2 fwd = transform.up*(forwardSignal)*moveForce;
 
    if(alive == true)
    {
 rb.AddForce(fwd*rb.mass*4f);
 rb.AddTorque(rotSignal*turnTorque*rb.inertia*4f);
 bctrl.energy -=  bctrl.eCost*moveForce;
    
    if (energy < 150f)
{
    starvation = true;
    AddReward(-0.001f);
}



        if(bctrl.hasReproduced == true)
    {
        AddReward(1.0f);
        bctrl.hasReproduced = false;
        EndEpisode();
        
    }
    }

}

  void OnCollisionEnter2D(Collision2D col)
{

    GameObject booper = col.gameObject;
    if(alive == true && starvation == false)
    {

         if (booper.tag == "Predator" || booper.tag == "Predator2" || booper.tag == "Carcass" )
         {
            AddReward(0.7f);
            starvation = false;
        
         }
            else if (booper.tag == "ApexPred" && energy >= bctrl.energyToReproduce*0.75f )
            {
            AddReward(bctrl.geneticDistance);        
            }


    }    
        
}



public override void Heuristic(in ActionBuffers actionsOut)
{

}


}