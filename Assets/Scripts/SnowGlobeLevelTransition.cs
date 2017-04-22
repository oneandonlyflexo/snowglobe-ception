using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowGlobeLevelTransition : MonoBehaviour {

	public Camera Camera;
	public string Scene;

	private bool startTransition = false;
	private Vector3 myVelocity = Vector3.zero;
	private float initialZ;
	private Vector3 scratchPos;

	private float startOrthographicSize;
	private float endOrthographicSize;

	private float transitionTimeTotal;
	private float changeSceneTime;
	private float time;

	private Vector3 startCameraPosition;
	private Vector3 endCameraPosition;

	void OnMouseDown()
	{
		startTransition = true;

		startOrthographicSize = Camera.orthographicSize;
		endOrthographicSize = startOrthographicSize * .4f;

		startCameraPosition = Camera.transform.position;
		endCameraPosition = new Vector3 (transform.position.x, transform.position.y, startCameraPosition.z);

		transitionTimeTotal = 1;
		changeSceneTime = 2;
		time = 0;
		//TODO: transition "into" the snowglobe
	}

	void Update ()
	{
		if (startTransition) {
			time += Time.deltaTime;
			if (time < transitionTimeTotal) {
				scratchPos = Vector3.Lerp (startCameraPosition, endCameraPosition, time / transitionTimeTotal);
				Camera.transform.position = new Vector3 (scratchPos.x, scratchPos.y, Camera.transform.position.z);
				Camera.orthographicSize = startOrthographicSize + (endOrthographicSize - startOrthographicSize) * time / transitionTimeTotal;

			} else if (time < changeSceneTime) {
				Camera.transform.position = endCameraPosition;
				Camera.orthographicSize = endOrthographicSize;

				//TODO: do the transition stuff here
			} else {
				Camera.transform.position = endCameraPosition;
				Camera.orthographicSize = endOrthographicSize;
				SceneManager.LoadScene (Scene);
			}
				
		}
	}
}
