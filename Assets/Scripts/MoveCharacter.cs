using UnityEngine;
using System.Collections.Generic;

public class MoveCharacter : MonoBehaviour
{
    private const string WALKING = "Walking";
    private const string CLIMBING = "Climbing";
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";
    private const string TOP_OF_LADDER = "TopOfLadder";
    private const string BOTTOM_OF_LADDER = "BottomOfLadder";

    public float verticalSpeed;
    public float horizontalSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    private bool sneezing;
    private bool Walking { get { return animator.GetBool(WALKING); } }
    private bool Climbing { get { return animator.GetBool(CLIMBING); } }
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

        if (Climbing)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (verticalSpeed * Time.deltaTime));
        }
    }

    private void FixedUpdate()
    {
        if (!sneezing && !Climbing)
        {
            if (IsAxisActive("Use"))
            {
                Stop(WALKING);

                sneezing = true;
                animator.SetTrigger("Sneeze");
            }
            else if (IsAxisActive(HORIZONTAL))
            {
                var moveHorizontal = Input.GetAxis(HORIZONTAL);
				rb.velocity = new Vector3(moveHorizontal * horizontalSpeed, rb.velocity.y, 0);

                animator.SetBool(WALKING, true);

                transform.localScale = new Vector3(moveHorizontal, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                Stop(WALKING);
            }
        }
    }

    private void Stop(string animatorParameter)
    {
        rb.velocity = Vector3.zero;
        animator.SetBool(animatorParameter, false);
    }

    private void StopClimbing()
    {
        animator.SetBool(CLIMBING, false);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (IsTopOfLadder(collider.gameObject))
        {
            animator.SetBool(TOP_OF_LADDER, true);
        }

        if (IsLadder(collider.gameObject))
        {
            var verticalInput = Input.GetAxis(VERTICAL);

            if (!Climbing && IsAxisActive(VERTICAL) &&
                ((verticalInput > 0 && animator.GetBool(BOTTOM_OF_LADDER)) ||
                 (verticalInput < 0 && animator.GetBool(TOP_OF_LADDER))))
            {
                Stop(WALKING);
                animator.SetBool(CLIMBING, true);
                transform.position = new Vector3(collider.gameObject.transform.position.x, transform.position.y);
                verticalSpeed = Mathf.Abs(verticalSpeed) * Input.GetAxis(VERTICAL);
            }
            else if (Climbing && AbleToLeaveLadder(collider.gameObject))
            {
                Stop(CLIMBING);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsTopOfLadder(collider.gameObject))
        {
            animator.SetBool(TOP_OF_LADDER, true);
        }
        else if (IsBottomOfLadder(collider.gameObject))
        {
            animator.SetBool(BOTTOM_OF_LADDER, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (Climbing && IsLadder(collider.gameObject) && AbleToLeaveLadder(collider.gameObject))
        {
            Stop(CLIMBING);
        }
        else if (IsTopOfLadder(collider.gameObject))
        {
            animator.SetBool(TOP_OF_LADDER, false);
        }
        else if (IsBottomOfLadder(collider.gameObject))
        {
            animator.SetBool(BOTTOM_OF_LADDER, false);
        }
    }

    private bool IsAxisActive(string axis)
    {
        return !Mathf.Approximately(Input.GetAxis(axis), 0.0f);
    }

    private bool IsLadder(GameObject gameObject)
    {
        return (gameObject.tag == "Ladder");
    }

    private bool IsTopOfLadder(GameObject gameObject)
    {
        return (gameObject.tag == "Ladder_Top");
    }

    private bool IsBottomOfLadder(GameObject gameObject)
    {
        return (gameObject.tag == "Ladder_Bottom");
    }

	private bool IsDoor(GameObject gameObject)
	{
		return (gameObject.tag == "DoorTrigger");
	}

    private bool AbleToLeaveLadder(GameObject ladder)
    {
        var minY = gameObject.GetComponent<Collider2D>().bounds.min.y;
        var ladderBounds = ladder.GetComponent<Collider2D>().bounds;

        return (minY <= ladderBounds.min.y) || (minY >= ladderBounds.max.y);
    }
}
