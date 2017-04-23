using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSnowPlotter : MonoBehaviour {

    [SerializeField]
    Transform groundSnowOnly;

    [SerializeField]
    int numOfFlakes = 500;
    public Vector3 box;
    // Use this for initialization
    void Start () {
        PopulateRealm();	
	}


    void PopulateRealm()
    {
        Vector3 tmpv3 = Vector3.zero;

        for (var index = 0; index < numOfFlakes; index++)
        {
            tmpv3 = transform.position + new Vector3(
                (Random.value - 0.5f) * box.x,
                (Random.value - 0.5f) * box.y,
               // (Random.value - 0.5f) * 
               box.z
             );
            //Debug.LogError(">> " + tmpv3);

            Instantiate(
                groundSnowOnly, 
                tmpv3,
                Quaternion.identity  
                );
        }

        Destroy(gameObject);
    }

}
