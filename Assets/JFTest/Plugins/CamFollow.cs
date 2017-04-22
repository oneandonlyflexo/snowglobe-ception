using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    
    [SerializeField]
    public float easeTime = .3f;

    private Vector3 myVelocity = Vector3.zero;
    private float initialZ;
    private Vector3 scratchPos;

    void Start()
    {
        initialZ = transform.position.z;
    }

	void Update ()
    {
        scratchPos = Vector3.SmoothDamp(transform.position, target.position,ref myVelocity,easeTime * Time.deltaTime);
        transform.position = new Vector3(scratchPos.x, transform.position.y, initialZ);
    }
}
