using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseObjects : MonoBehaviour
{
    public List<Usable> usables = new List<Usable>();

    public void UseAll()
    {
        usables.ForEach(usable =>
        {
            if (usable.enabled)
            {
                usable.Use();
            }
            else
            {
                usables.Remove(usable);
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var usable = collider.transform.root.GetComponent<Usable>();

        if (usable != null && !usables.Contains(usable))
        {
            usables.Add(usable);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var usable = collider.transform.root.GetComponent<Usable>();
        if (usable != null && usables.Contains(usable))
        {
            usables.Remove(usable);
        }
    }
}
