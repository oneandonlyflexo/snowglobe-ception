using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCrab : MonoBehaviour
{
    public float speed;
    public float chanceToChangeDirection;
    public float minimumDelay;
    public float minX;
    public float maxX;

    private bool waiting;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);

        if ((!waiting && Random.Range(0.0f, 1.0f) < chanceToChangeDirection) ||
            (transform.position.x <= minX || transform.position.x >= maxX))
        {
            speed = (speed < 0) ? Mathf.Abs(speed) : (0f - speed);
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(minimumDelay);
        waiting = false;
    }
}
