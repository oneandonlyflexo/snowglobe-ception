using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooncontroller : MonoBehaviour {

    public float movAmt = 1f;
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+movAmt);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movAmt);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - movAmt, transform.position.y );
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + movAmt, transform.position.y);
        }

 
    }
}
