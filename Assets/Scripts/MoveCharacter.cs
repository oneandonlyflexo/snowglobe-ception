using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");

        if (!Mathf.Approximately(moveHorizontal, 0.0f))
        {
            var movement = new Vector3(moveHorizontal, 0.0f);
            rb.velocity = movement * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
