using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSnowPlotter : MonoBehaviour
{

    [SerializeField]
    Transform groundSnowPrefab;

    [SerializeField]
    int numOfFlakes = 500;

    [SerializeField]
    Vector3 box;

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

            Instantiate(
                groundSnowPrefab, 
                tmpv3,
                Quaternion.identity  
                );
        }

        Destroy(gameObject);
    }
}
