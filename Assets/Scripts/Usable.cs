using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Usable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent actions;

    public void Use()
    {
        actions.Invoke();
    }
}
