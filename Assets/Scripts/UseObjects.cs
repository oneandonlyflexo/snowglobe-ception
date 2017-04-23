using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseObjects : MonoBehaviour
{
    public string tagToMatch;
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
        var usable = collider.transform.root.GetComponentInChildren<Usable>();
        if (usable != null && !usables.Contains(usable) && (usable.tag == tagToMatch))
        {
            usables.Add(usable);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var usable = collider.transform.root.GetComponentInChildren<Usable>();
        if (usable != null && usables.Contains(usable) && (usable.tag == tagToMatch))
        {
            usables.Remove(usable);
        }
    }
}
