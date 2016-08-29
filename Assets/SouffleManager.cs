using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;

public class SouffleManager : MonoBehaviour {

	public GameObject MicInputer;

	private float volume;

	public GameObject prefabSouffleur;

	public float intervalle = 0.5f;
	public int nombre = 0;

	public bool soufflant = false;

	// Use this for initialization
	void Start () {
	

		StartCoroutine (BoucleEnvoi ());
	}


	public void Soufflant(bool param)
	{
		soufflant = param;
	}


	IEnumerator BoucleEnvoi ()
	{
		
		/*
		if(nombre > 4)
			yield break;
		*/

		if(soufflant)
		{

			CreeEtLanceSouffle();
			Debug.Log(nombre);
			nombre++;
		}

		yield return new WaitForSeconds (intervalle);
		StartCoroutine (BoucleEnvoi ()); //si while false, loop

	}

	void CreeEtLanceSouffle()
	{
		GameObject child = Instantiate(prefabSouffleur) as GameObject;

		//child.transform.SetParent(transform);

		//lancer le souffle de cet enfant
		child.GetComponent<Souffleur> ().LanceSouffle();

		//meme position que SouffleurParent, meme orientation que la camera
		child.transform.position = transform.position;
		child.transform.Rotate(GameObject.Find("Camera").transform.rotation.eulerAngles);

		//et là l'orpheliner
		//child.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
	
		volume = MicInputer.GetComponent<MicInput> ().MicLoudness;




	}
}
