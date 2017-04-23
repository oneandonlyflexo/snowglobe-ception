using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour {

	public GameObject DoorSpace;

	private float timeDoorInactive = 0;

	// Use this for initialization
	private void Start () {
		
	}

	private void Update() {
		if (timeDoorInactive > 0) {
			timeDoorInactive -= Time.deltaTime;
		}
	}

	private void OnTriggerStay2D(Collider2D collider) {
		if (timeDoorInactive <= 0 && IsAxisActive ("Use")) {
			timeDoorInactive = 1;
			DoorSpace.SetActive (!DoorSpace.activeSelf);

            var mcScript = collider.GetComponent<MoveCharacter>();
            if (mcScript != null)
            {
                mcScript.isInside = !mcScript.isInside;
            }

		}
	}

	private void OnTriggerExit2D(Collider2D collider) {
		timeDoorInactive = 0;
	}

	private bool IsAxisActive(string axis)
	{
		return !Mathf.Approximately(Input.GetAxis(axis), 0.0f);
	}
}
