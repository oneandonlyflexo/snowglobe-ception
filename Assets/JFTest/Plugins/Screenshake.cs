using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public float force = 1f;
    bool jiggleMe = false;
    private void Awake()
    {
        EventManager.Listen("ShakeSnowGlobe", ToggleIndoorOutDoor);
    }

    private void OnDisable()
    {
        EventManager.StopListen("ShakeSnowGlobe", ToggleIndoorOutDoor);
    }

    private void ToggleIndoorOutDoor()
    {
        jiggleMe = true;
    }

    Vector3 myVelocity = Vector3.zero;
    public float easeTime = .3f;

    private void Update()
    {
        //XYX
        if (Input.GetKeyDown(KeyCode.Y))
        {
            EventManager.Dispatch("ShakeSnowGlobe");
        }

        if (jiggleMe)
            jiggleMe = false;
   }
}
