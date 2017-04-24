using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSnowPlotter : MonoBehaviour
{

    [SerializeField]
    Transform groundSnowPrefab;

    [SerializeField]
    int numOfFlakes;

    [SerializeField]
    Vector3 box;

    public float forceMultiplier = 1f;

    public List<Transform> mySpecialFlakes = new List<Transform>();

    bool jiggleMe = false;
    private void Awake()
    {
        //force
        box = new Vector3(120,40,0);
        forceMultiplier = 1f;

        EventManager.Listen("ShakeSnowGlobe", ToggleIndoorOutDoor);
    }

    private void OnDisable()
    {
        EventManager.StopListen("ShakeSnowGlobe", ToggleIndoorOutDoor);
    }

    private void ToggleIndoorOutDoor()
    {
        jiggleMe = true;
    }

    private void LateUpdate()
    {
        if (jiggleMe)
        {
            foreach (var record in mySpecialFlakes)
            {
                Vector2 randomVector = new Vector2((Random.value - 0.5f), (Random.value - 0.5f));

                   if (record != null)
                    record.GetComponent<Rigidbody2D>().AddRelativeForce(randomVector * forceMultiplier);
            }

            jiggleMe = false;
        }
    }

    void Start ()
    {
        Vector3 tmpv3 = Vector3.zero;

        for (var index = 0; index < numOfFlakes; index++)
        {
            tmpv3 = transform.position + new Vector3(
                (Random.value - 0.5f) * box.x,
                (Random.value - 0.5f) * box.y,
                box.z
                );

            var  flake = Instantiate(
                groundSnowPrefab,
                tmpv3,
                Quaternion.identity
                ) as Transform;

            mySpecialFlakes.Add(flake);
        }

       // Destroy(gameObject);
    }
}
