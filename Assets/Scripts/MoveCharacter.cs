using UnityEngine;
using System.Collections.Generic;

public class MoveCharacter : MonoBehaviour
{
    private const string WALKING = "Walking";
    private const string CLIMBING = "Climbing";
    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

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
    }

    private void FixedUpdate()
    {
        var moveVertical = Input.GetAxis(VERTICAL);
        var moveHorizontal = Input.GetAxis(HORIZONTAL);

        if (Climbing)
        {
            rb.velocity = (new Vector3(0.0f, moveVertical)) * verticalSpeed;
        }
        else if (!sneezing)
        {
            if (IsAxisActive("Use"))
            {
                StopWalking();

                sneezing = true;
                animator.SetTrigger("Sneeze");
            }
            else if (!Mathf.Approximately(moveHorizontal, 0.0f))
            {
				rb.velocity = new Vector3(moveHorizontal * horizontalSpeed, rb.velocity.y, 0);

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
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
        animator.SetBool(WALKING, false);
    }

    private void StopClimbing()
    {
        animator.SetBool(CLIMBING, false);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
		if (IsLadder (collider.gameObject)) {
			if (IsAxisActive (VERTICAL)) {
				// Force them to stop climbing if they reach the top
				if (Climbing && AbleToLeaveLadder (collider.gameObject)) {
					StopClimbing ();
				}
                // Start climbing
                else if (!Climbing) {
					StopWalking ();
					animator.SetBool (CLIMBING, true);
					transform.position = new Vector3 (collider.gameObject.transform.position.x, transform.position.y);
				}
			} else if (Climbing && IsAxisActive (HORIZONTAL) && AbleToLeaveLadder (collider.gameObject)) {
				StopClimbing ();
			}
		}
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
		if (IsLadder (collider.gameObject)) {
			if (AbleToLeaveLadder (collider.gameObject)) {
				StopClimbing ();
			}
		}
    }

    private bool IsAxisActive(string axis)
    {
        return !Mathf.Approximately(Input.GetAxis(axis), 0.0f);
    }

    private bool IsLadder(GameObject gameObject)
    {
        return (gameObject.tag == "Ladder_Bottom") ||
               (gameObject.tag == "Ladder_Top");
	}

	private bool IsDoor(GameObject gameObject)
	{
		return (gameObject.tag == "DoorTrigger");
	}

    private bool AbleToLeaveLadder(GameObject ladder)
    {
        var bounds = gameObject.GetComponent<Collider2D>().bounds;
        var ladderBounds = ladder.GetComponent<Collider2D>().bounds;

        return (ladder.tag == "Ladder_Top" && (bounds.min.y >= ladderBounds.min.y)) ||
               (ladder.tag == "Ladder_Bottom" && (bounds.min.y <= ladderBounds.min.y));
    }
}
