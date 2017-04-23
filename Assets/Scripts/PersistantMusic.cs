using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantMusic : MonoBehaviour {
	
	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

}