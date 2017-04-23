using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirection : MonoBehaviour
{
    public Animator animator;
    public float minimumTimeToWait;
    public float maximumTimetoWait;

    private void Start()
    {
        StartCoroutine(WaitAndSwitch(Random.Range(minimumTimeToWait, maximumTimetoWait)));
    }

    private IEnumerator WaitAndSwitch(float duration)
    {
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("SwitchDirection");

        StartCoroutine(WaitAndSwitch(Random.Range(minimumTimeToWait, maximumTimetoWait)));
    }
}
