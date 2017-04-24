using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseObjects : MonoBehaviour
{
    public string tagToMatch;
    public List<Usable> usables = new List<Usable>();

    public bool ReadyToUse
    {
        get
        {
            usables.RemoveAll(usable => !usable.enabled || !usable.gameObject.activeInHierarchy);
            return usables.Count > 0;
        }
    }

    public void UseAll()
    {
        usables.RemoveAll(usable => !usable.enabled || !usable.gameObject.activeInHierarchy);
        usables.ForEach(usable =>
        {
            if (usable.enabled)
            {
                usable.Use();
            }

            usables.Remove(usable);
        });
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var usable = collider.transform.parent.GetComponentInChildren<Usable>();
        if (usable != null && !usables.Contains(usable) && (usable.tag == tagToMatch))
        {
            usables.Add(usable);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var usable = collider.transform.parent.GetComponentInChildren<Usable>();
        if (usable != null && usables.Contains(usable) && (usable.tag == tagToMatch))
        {
            usables.Remove(usable);
        }
    }
}
