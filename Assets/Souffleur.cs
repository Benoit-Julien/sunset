using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.ComponentModel.Design;
using LibNoise;


public class Souffleur : MonoBehaviour
{

	public GameObject ForceExplosion = null;
	public GameObject ForceTurbulence = null;
	public float DurationBeforeCalm = 4.0f;
	public bool MouseState;

	public bool done = false;

	//private TCParticleSystem Particles; //si on a plusieurs souffleurs en prefab, on ne modifie plus le damping au vol

	void Awake ()
	{
		//Debug.Log(transform.position+" : a wake");

		//Particles = GameObject.Find ("Cube test").GetComponent<TCParticleSystem> ();
		ForceTurbulence.GetComponent<TCForce> ().enabled = false;

		//LanceSouffle();
	}

	public void LanceSouffle()
	{
		StartCoroutine (Souffle ());
	}

	IEnumerator Souffle ()
	{
		//yield return new WaitForSeconds (2.0f);

		StartTween();

		ForceExplosion.GetComponent<TCForce> ().enabled = true;
		//ForceTurbulence.GetComponent<TCForce> ().enabled = true;
		StartCoroutine (WaitToCut ());
		StartCoroutine (WaitToTurbu ());

		//todo : tween pour avancer sur Z Vector force ET Turbulence
		//en fadant leur puissance avant DurationBeforeCalm, du coup

		yield return null;
	}
		
	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 20.0F;
	private float startTime;
	private float journeyLength;

	void StartTween()
	{
		startMarker = new Vector3();
		startMarker = GameObject.Find("Camera").transform.position;

		endMarker = new Vector3();
		endMarker = startMarker + 100*GameObject.Find("Camera").transform.forward;

		//Debug.Log(transform.position+" ?");
		//Debug.Log(startMarker.position+" "+endMarker.position);

		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
	}

	void UpdateTween()
	{
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		if(fracJourney > 1.0)
		{
			done = true;
			return;
		}

		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	
		ForceExplosion.GetComponent<TCForce> ().power = 10+ Mathf.Min(fracJourney*10.0f, 1.0f)*80;
		ForceTurbulence.GetComponent<TCForce> ().power = 0+ Mathf.Min(fracJourney*10.0f, 1.0f)*30;

		//ForceTurbulence.GetComponent<TCForce> ().power = 20+fracJourney*20;

		ForceTurbulence.GetComponent<TCForce> ().radius.Value = 20+fracJourney*10;
		ForceTurbulence.transform.Rotate(0,0, 0.8f);

		//Debug.Log(Time.time+" "+startTime+" "+fracJourney+" "+distCovered+" / "+journeyLength);
	}

	void Update()
	{
		//cf lerp pour tween https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html 
		UpdateTween();

		if(done)
		{
			Destroy(gameObject);
		}
	}

	IEnumerator WaitToCut ()
	{
		yield return new WaitForSeconds (DurationBeforeCalm);

		ForceTurbulence.GetComponent<TCForce> ().enabled = false;
		ForceExplosion.GetComponent<TCForce> ().enabled = false;
		//Particles.Damping = 0.7f;
		yield return null;
	}

	IEnumerator WaitToTurbu () {

		//acceleration force plutot que delai
		yield return new WaitForSeconds (0f);
		ForceTurbulence.GetComponent<TCForce> ().enabled = true;

		yield return null;

	}



}
