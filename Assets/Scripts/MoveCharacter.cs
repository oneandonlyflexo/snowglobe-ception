using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private const string WALKING = "Walking";

    public float speed;
    public Rigidbody2D rb;
    public Animator animator;

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");

        if (!Mathf.Approximately(moveHorizontal, 0.0f))
        {
            var movement = new Vector3(moveHorizontal, 0.0f);
            rb.velocity = movement * speed;
            animator.SetBool(WALKING, true);

            transform.localScale = new Vector3(moveHorizontal, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            rb.velocity = Vector3.zero;
            animator.SetBool(WALKING, false);
        }
    }
}
