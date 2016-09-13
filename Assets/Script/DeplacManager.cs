using UnityEngine;
using System.Collections;

public class DeplacManager : MonoBehaviour {
	public float CurrentSpeed;
	public float MinimunSpeed = 0.4f;

	private float BeforeSpeed;
	private Vector3 BeforePos;
	private Vector3 BeforeRotation;
	private Vector3 Movement;
	private bool check;
	private bool CoroutineState;

	void OnEnable () {
		Movement = new Vector3 ();
		check = true;
		CoroutineState = false;
	}

	void Update () {
		if (!check) {
			Movement = transform.position - BeforePos;
			CurrentSpeed = Movement.magnitude / Time.deltaTime;

			if (!CoroutineState && CurrentSpeed >= MinimunSpeed)
				SendForce ();
			else if (CurrentSpeed - BeforeSpeed > 1.5f)
				SendForce ();

		} else check = false;

		BeforePos = transform.position;
		BeforeRotation = transform.rotation.eulerAngles;
		BeforeSpeed = CurrentSpeed;
	}

	void SendForce () {
		Debug.Log ("SendForce");
		CreateAndSendMouvement (CurrentSpeed);
		StopCoroutine ("WaitTime");
		StartCoroutine (WaitTime (1 - CurrentSpeed / 10));
	}

	void CreateAndSendMouvement(float power) {
		
		GameObject child = Instantiate (Resources.Load ("Prefabs/Force", typeof(GameObject))) as GameObject;
			
		child.transform.Rotate (BeforeRotation);
		child.transform.position = BeforePos;
		child.GetComponent<Force> ().SendForce (power);
	}

	IEnumerator WaitTime(float wait) {
		CoroutineState = true;
		yield return new WaitForSeconds (wait);
		CoroutineState = false;
		yield return null;
	}
}
