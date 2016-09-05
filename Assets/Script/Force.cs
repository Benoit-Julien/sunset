using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.ComponentModel.Design;
//using LibNoise;

public class Force : MonoBehaviour
{
	public GameObject ForceExplosion = null;
	public GameObject ForceTurbulence = null;
	public float DurationBeforeActivation = 0.1f;

	private float power;

	void Awake () {
		ForceTurbulence.GetComponent<TCForce> ().enabled = false;
		ForceExplosion.GetComponent<TCForce> ().enabled = false;
	}

	public void SendForce(float _power) {
		power = _power;
		StartCoroutine (Forces ());
	}

	IEnumerator Forces () {
		//yield return new WaitForSeconds (2.0f);

		StartTween();
		StartCoroutine (WaitForEnable ());

		//ForceTurbulence.GetComponent<TCForce> ().enabled = true;
		//StartCoroutine (WaitToCut ());

		//todo : tween pour avancer sur Z Vector force ET Turbulence
		//en fadant leur puissance avant DurationBeforeCalm, du coup

		yield return null;
	}
		
	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 5.0f;
	private float startTime;
	private float journeyLength;

	void StartTween() {
		startMarker = new Vector3();
		startMarker = transform.position;

		endMarker = new Vector3();
		endMarker = startMarker + 10 * transform.forward;

		//Debug.Log(transform.position+" ?");
		//Debug.Log(startMarker.position+" "+endMarker.position);

		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
	}

	bool UpdateTween() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		if(fracJourney > 1.0f)
			return true;

		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	
		ForceExplosion.GetComponent<TCForce> ().power = (2 * power) - fracJourney * (2 * power);
		ForceTurbulence.GetComponent<TCForce> ().power = (1 * power) - fracJourney * (1 * power);

		//ForceTurbulence.GetComponent<TCForce> ().power = 20+fracJourney*20;

		ForceTurbulence.GetComponent<TCForce> ().radius.Value = 20 + fracJourney * 10;
		ForceTurbulence.transform.Rotate(0,0, 0.8f);

		//Debug.Log(Time.time+" "+startTime+" "+fracJourney+" "+distCovered+" / "+journeyLength);
		return false;
	}

	void Update() {
		//cf lerp pour tween https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html 

		if(UpdateTween())
			Destroy(gameObject);
	}

	IEnumerator WaitForEnable () {

		//acceleration force plutot que delai
		yield return new WaitForSeconds (DurationBeforeActivation);
		ForceTurbulence.GetComponent<TCForce> ().enabled = true;
		ForceExplosion.GetComponent<TCForce> ().enabled = true;

		yield return null;

	}
}
