using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public GameObject ForceExplosion = null;
	public GameObject ForceAttraction = null;

	public float DurationBeforeExplosion = 0;
	public float DuractionOfExplosion = 0;

	public bool MouseState;
	private TCParticleSystem Particles;

	void Start () {
		Particles = GameObject.Find ("Particles").GetComponent<TCParticleSystem> ();
		StartCoroutine (InitExplosion ());
		StartCoroutine (AttractionSystem ());
	}

	IEnumerator AttractionSystem () {
		yield return new WaitForSeconds (DuractionOfExplosion + DurationBeforeExplosion);
		while (true) {
			if (MouseState) {
				Particles.Damping = 0.9f;
				ForceAttraction.GetComponent<TCForce> ().enabled = true;
			} else {
				Particles.Damping = 0.2f;
				ForceAttraction.GetComponent<TCForce> ().enabled = false;
			}
			yield return null;
		}
	}

	IEnumerator InitExplosion ()
	{
		Debug.Log("initExplosion");

		yield return new WaitForSeconds (DurationBeforeExplosion);
		Debug.Log("force enabled");
		ForceExplosion.GetComponent<TCForce> ().enabled = true;
		yield return new WaitForSeconds (DuractionOfExplosion);
		ForceExplosion.GetComponent<TCForce> ().enabled = false;
		Debug.Log("force disabled");
		Particles.Damping = 0.2f;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0))
			MouseState = true;
		else if (Input.GetMouseButtonUp (0))
			MouseState = false;
	}
}
