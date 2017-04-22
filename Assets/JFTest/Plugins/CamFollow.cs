using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    //public Vector3 pos; .. these are important i can't remember why tho
    //public Vector3 myPos;
    public float easeTime = .02f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //check	current pos, last pos, target pos
        //will need to normalize/fix 2d / manage zoom a lil
        transform.position = Vector3.Lerp(target.position, transform.position, easeTime *Time.deltaTime);
	}
}
