using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    float time;
   public int in1;
   public int in2;
   public int in3;
   public int in4;
    int blib_mask;
   Vector2 p = new Vector2 (0f,0f);


    Vector2 boxA;
    Vector2 boxB;
    Vector2 boxC;
    Vector2 boxD;

   Collider2D[] detect1;
   Collider2D[] detect2;
   Collider2D[] detect3;

   Collider2D[] detect4;
   Transform boxTransform;

    // Start is called before the first frame update
    void Start()
    {
       blib_mask = LayerMask.GetMask("Prey");
       boxTransform = GameObject.Find("box").GetComponent<Transform>();
        boxA = new Vector2((boxTransform.position.x - boxTransform.lossyScale.x/2f),(boxTransform.position.y + boxTransform.lossyScale.y/2f));
        boxB = new Vector2((boxTransform.position.x + boxTransform.lossyScale.x/2f),(boxTransform.position.y + boxTransform.lossyScale.y/2f));
        boxC = new Vector2((boxTransform.position.x - boxTransform.lossyScale.x/2f),(boxTransform.position.y - boxTransform.lossyScale.y/2f));
        boxD = new Vector2((boxTransform.position.x + boxTransform.lossyScale.x/2f),(boxTransform.position.y - boxTransform.lossyScale.y/2f));



        Debug.Log(boxA);
        Debug.Log(boxB);
        Debug.Log(boxC);
        Debug.Log(boxD);
    }

    // Update is called once per frame
    void Update()
    {   time += Time.deltaTime;
       
        if(time >= 1){
        Collider2D[] detect1 = Physics2D.OverlapAreaAll(p, boxA, blib_mask);

        in1 = detect1.Length;

        Collider2D[] detect2 = Physics2D.OverlapAreaAll(p, boxB, blib_mask);
        in2 = detect2.Length;

        Collider2D[] detect3 = Physics2D.OverlapAreaAll(p, boxC, blib_mask);
        in3 = detect3.Length;

        Collider2D[] detect4 = Physics2D.OverlapAreaAll(p, boxD, blib_mask);
        in4 = detect4.Length;
        Debug.Log(in1 + "," + in2 + "," + in3 + "," + in4);
        time = 0f;
        }
        

    }
 
           

           
  
    

    
}
