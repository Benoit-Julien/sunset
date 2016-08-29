using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public GameObject Target = null;

	void Update () {
		if (Target.gameObject.activeInHierarchy) {
			gameObject.GetComponent<TCForce> ().enabled = true;
		} else
			gameObject.GetComponent<TCForce> ().enabled = false;
		
		var targetT = Target.transform;
		transform.position = targetT.TransformPoint (new Vector3 (0, 0, 0));
	}
}
