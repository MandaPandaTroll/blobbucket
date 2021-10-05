using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamCntrl : MonoBehaviour
{
float camSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                if (Input.GetKey("w") == true)
        {

            transform.Translate(transform.up * Time.deltaTime * camSpeed);
        }

                if (Input.GetKey("a") == true)
        {

            transform.Translate(-transform.right * Time.deltaTime * camSpeed);
        }

                if (Input.GetKey("s") == true)
        {

            transform.Translate(-transform.up * Time.deltaTime * camSpeed);
        }

                if (Input.GetKey("d") == true)
        {

            transform.Translate(transform.right * Time.deltaTime * camSpeed);
        }

            if (Input.GetKey("escape") == true)
            {
                Application.Quit();
            }

            if(Input.GetKey("r") == true)
            {
                 SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }

            if(Input.GetKey("up") == true)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize - 1;
            }
                        if(Input.GetKey("down") == true)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + 1;
            }
    }
}
