using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    public string doorTriggerName =  "DoorTrigger";
    //private float timeDoorInactive = 0;

    private void Update() {
		//if (timeDoorInactive > 0) {
		//	timeDoorInactive -= Time.deltaTime;
		//}
	}

	private void OnTriggerStay2D(Collider2D collider) {
        Debug.LogError(">> " + collider.name);
        if (//timeDoorInactive <= 0 && 
            IsAxisActive ("Use") &&
            collider.name == doorTriggerName
            )

        {
			//timeDoorInactive = 1;
            
            EventManager.Dispatch("ToggleIndoors");
        }
	}

	private void OnTriggerExit2D(Collider2D collider) {
		//timeDoorInactive = 0;
	}

	private bool IsAxisActive(string axis)
	{
        //return !Mathf.Approximately(Input.GetAxis(axis), 0.0f);
        return (Input.GetKeyDown(KeyCode.E));
    }
}
