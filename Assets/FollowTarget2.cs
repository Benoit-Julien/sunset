using UnityEngine;
using System.Collections;
using Leap;

public class FollowTarget2 : MonoBehaviour {

	public Transform OffSet;
	public Transform camera;

	void Start () {
		if (camera == null) {
			camera = Camera.main.transform;
		}
	}

	void Update () {
		transform.position = camera.position + OffSet.position;

		transform.rotation.Set (camera.rotation.x, camera.rotation.y, 0, camera.rotation.w);
	}
}
