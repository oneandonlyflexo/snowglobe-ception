using UnityEngine;
using UnityEngine.Events;

public class UIToggler : MonoBehaviour
{
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "UIEnabler")
        {
            triggerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "UIEnabler")
        {
            triggerExit.Invoke();
        }
    }
}
