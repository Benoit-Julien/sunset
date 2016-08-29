using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.ComponentModel.Design;
using LibNoise;


public class Force : MonoBehaviour
{
	public string main;

	public GameObject ForceExplosion = null;
	public GameObject ForceTurbulence = null;
	public float DurationBeforeActivation = 0.01f;

	void Awake () {
		ForceTurbulence.GetComponent<TCForce> ().enabled = false;
		ForceExplosion.GetComponent<TCForce> ().enabled = false;
	}

	public void SendForce(string _main) {
		main = _main;
		StartTween();
		StartCoroutine (WaitToTurbu ());
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

		if(main == "R")
			transform.Rotate(0,20,0);
		if(main == "L")
			transform.Rotate(0,-20,0);
		
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
	
		ForceExplosion.GetComponent<TCForce> ().power = 4;// - Mathf.Min(fracJourney * 10.0f, 1.0f) * 4;
		ForceTurbulence.GetComponent<TCForce> ().power = 1.5f;// - Mathf.Min(fracJourney * 10.0f, 1.0f) * 2.5f;

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

	IEnumerator WaitToTurbu () {

		//acceleration force plutot que delai
		yield return new WaitForSeconds (DurationBeforeActivation);
		ForceTurbulence.GetComponent<TCForce> ().enabled = true;
		ForceExplosion.GetComponent<TCForce> ().enabled = true;

		yield return null;

	}
}
