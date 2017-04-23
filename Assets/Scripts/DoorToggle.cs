using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    public string doorTriggerName =  "DoorTrigger";
    bool touchingDoor = false;

    private void Awake()
    {
        EventManager.Listen("ToggleIndoors", ToggleIndoorOutDoor);
    }

    private void OnDisable()
    {
        EventManager.StopListen("ToggleIndoors", ToggleIndoorOutDoor);
    }

    private void ToggleIndoorOutDoor()
    {//resets for when colliders disappear before being able to send exit

        touchingDoor = false;
    }

    private void Update()
    {       
        if (IsAxisActive("Use") && touchingDoor)
        {
            Debug.LogError(">> OUT: " + "ToggleIndoors");
            EventManager.Dispatch("ToggleIndoors");
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == doorTriggerName)
        {
            Debug.LogError(">> IN: " + collider.name);
            touchingDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == doorTriggerName)
        {
            Debug.LogError(">> OUT: " + collider.name);
            touchingDoor = false;
        }
    }

	private bool IsAxisActive(string axis)
	{
        //return !Mathf.Approximately(Input.GetAxis(axis), 0.0f);
        return (Input.GetKeyDown(KeyCode.E));
    }
}
