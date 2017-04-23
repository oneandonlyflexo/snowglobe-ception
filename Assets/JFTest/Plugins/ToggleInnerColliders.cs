using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInnerColliders : MonoBehaviour {

    bool isInside = false;

    public List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        EventManager.Listen("ToggleIndoors", ToggleIndoorOutDoor);
    }

    private void OnDisable()
    {
        EventManager.StopListen("ToggleIndoors", ToggleIndoorOutDoor);
    }

    private void ToggleIndoorOutDoor()
    {
        isInside = !isInside;

        foreach (var record in children)
        {
           record.SetActive(isInside);
        }
    }
}
