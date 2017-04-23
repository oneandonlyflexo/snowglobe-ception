using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private const string WALKING = "Walking";

    public float speed;
    public Rigidbody2D rb;
    public Animator animator;

    private bool sneezing;
    private bool Walking { get { return animator.GetBool(WALKING); } }
    private bool isInside; //not hooked up yet

    [SerializeField]
    float timeBetweenPuffs = 3f;
    float timeSinceLastPuff = 0f;

    public Transform prefabFootSnow;
    public Transform anchor;

    public void StopSneezing()
    {
        sneezing = false;
    }

    private void Update()
    {
        if (timeSinceLastPuff >= timeBetweenPuffs)
        {
            if (!isInside && Walking && Random.Range(0.0f, 10.0f) > 7f)
            {
                Instantiate(prefabFootSnow, anchor.position, Quaternion.identity);
                timeSinceLastPuff = 0;
            }
        }
        else
        {
            timeSinceLastPuff += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");

        if (!sneezing)
        {
            if (Input.GetAxis("Use") > 0.0f)
            {
                StopWalking();

                sneezing = true;
                animator.SetTrigger("Sneeze");
            }
            else if (!Mathf.Approximately(moveHorizontal, 0.0f))
            {
                var movement = new Vector3(moveHorizontal, 0.0f);
                rb.velocity = movement * speed;
                animator.SetBool(WALKING, true);

                transform.localScale = new Vector3(moveHorizontal, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                StopWalking();
            }
        }
    }

    private void StopWalking()
    {
        rb.velocity = Vector3.zero;
        animator.SetBool(WALKING, false);
    }
}
