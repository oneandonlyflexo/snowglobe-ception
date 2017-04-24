using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class UIToggler : MonoBehaviour
{
    public List<string> acceptedActorNames;
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsValidEnabler(collider))
        {
            triggerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (IsValidEnabler(collider))
        {
            triggerExit.Invoke();
        }
    }

    private bool IsValidEnabler(Collider2D collider)
    {
        return collider.gameObject.tag == "UIEnabler" && acceptedActorNames.Contains(collider.transform.root.name);
    }
}
