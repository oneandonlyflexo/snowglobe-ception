using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnStart : MonoBehaviour {

	public bool Active = true;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (Active);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
