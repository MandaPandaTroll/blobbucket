using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlibSpawner : MonoBehaviour
{
   
  public int initBlib;
  private bool autoRespawn;
  public int extraBlib;

  public int minBlib;
  public GameObject blib;
  GameObject[] blibs;

  GameObject box;
  int blibN;
  float boxSize;

  

    // Start is called before the first frame update
    void Start()
    {   
      autoRespawn = false;
        box = GameObject.Find("box");
         boxSize = box.transform.localScale.x;

        for(int i = 0; i < initBlib; i++){
        float x = (float)Random.Range(-boxSize/3,boxSize/3);
        float y = (float)Random.Range(-boxSize/3,boxSize/3);
       Instantiate(blib, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    void OnGUI()
    {
      autoRespawn = GUI.Toggle( new Rect(10,500,200,30),autoRespawn,"autoRespawn_blib" );
    }

void Update()
{
  
  blibs = GameObject.FindGameObjectsWithTag("Prey");
  blibN = blibs.Length;
  if (Input.GetKeyDown("i") == true  ){ extraSpawn();}
        if (autoRespawn == true && blibN <= minBlib){extraSpawn();}
        
    }
  

  void extraSpawn()
  {
    
        for(int i = 0; i < extraBlib; i++){
        float x = (float)Random.Range(-boxSize/3,boxSize/3);
        float y = (float)Random.Range(-boxSize/3,boxSize/3);
       Instantiate(blib, new Vector3(x, y, 0), Quaternion.identity);
  }
  }

}
