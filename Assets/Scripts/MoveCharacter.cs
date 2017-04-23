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
    public const string DOOR_TRIGGER_NAME = "DoorTrigger";

    public DudeSounds charSoundManager;
    public bool isDog = false;
    public bool touchingDoor = false;

    public UseObjects point;
    public UseObjects sneeze;

    public float verticalSpeed;
    public float horizontalSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public float waitTimeOnSwitch;

    public bool isInside;

    private bool shaking;
    private bool sneezing;
    private bool pointing;
    private bool waiting;

    private bool Walking { get { return animator.GetBool(WALKING); } }
    private bool Climbing { get { return animator.GetBool(CLIMBING); } }

    private bool PerformingBlockingAnimation
    {
        get
        {
            return sneezing || shaking || pointing || Climbing || waiting;
        }
    }

    [SerializeField]
    float timeBetweenPuffs = 3f;
    float timeSinceLastPuff = 0f;

    public Transform prefabFootSnow;
    public Transform anchor;

    public void AnimationComplete(string animationName)
    {
        if (animationName == "Sneezing")
        {
            sneezing = false;
        }
        else if (animationName == "Point")
        {
            pointing = false;
        }
        else if (animationName == "ShakeGlobe")
        {
            shaking = false;
        }
        else if (animationName == "All")
        {
            shaking = sneezing = pointing = false;
        }
    }

    private void Awake()
    {
        EventManager.Listen("ToggleIndoors", ToggleIndoorOutDoor);
    }

    private void OnEnable()
    {
        animator.SetTrigger("SwitchCharacters");
        StartCoroutine(Wait());
    }

    private void OnDisable()
    {
        animator.SetTrigger("SwitchCharacters");
    }

    private void ToggleIndoorOutDoor()
    {
        // lol
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (touchingDoor)
        {
            isInside = !isInside;
        }
        else
        {
            spriteRenderer.sortingOrder = (spriteRenderer.sortingOrder == 4) ? -1 : 4;
        }

        //resets for when colliders disappear before being able to send exit
        touchingDoor = false;
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
        if (!PerformingBlockingAnimation)
        {
            if (Input.GetButtonDown("Use"))
            {
                Stop(WALKING);

                if (!touchingDoor)
                {
                    pointing = true;
                    animator.SetTrigger("Point");
                    point.UseAll();

                    if (isDog)
                    {
                        charSoundManager.PlaySneeze();
                    }
                }
                else
                {
                    EventManager.Dispatch("ToggleIndoors");
                }
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (IsTopOfLadder(collider.gameObject))
        {
            animator.SetBool(TOP_OF_LADDER, true);
        }

        if(!isDog)
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

        if (collider.name == DOOR_TRIGGER_NAME)
        {
            touchingDoor = true;
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

        if (collider.name == DOOR_TRIGGER_NAME)
        {
            touchingDoor = false;
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

    private bool AbleToLeaveLadder(GameObject ladder)
    {
        var minY = gameObject.GetComponent<Collider2D>().bounds.min.y;
        var ladderBounds = ladder.GetComponent<Collider2D>().bounds;

        return (minY <= ladderBounds.min.y) || (minY >= ladderBounds.max.y);
    }

    private System.Collections.IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTimeOnSwitch);
        waiting = false;
    }
}
