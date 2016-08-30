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

	public AudioSource son0;
	public AudioSource son1;
	public AudioSource son2;
	public AudioSource son3;
	public AudioSource son4;

	private int son_index = 0;
	private AudioSource[] sons ;


	// Use this for initialization
	void Start () {
		sons = new AudioSource[] {son0, son1, son2, son3, son4};
	}


	public void Soufflant(bool param)
	{
		soufflant = param;
	}

	private void joueSon()
	{
		sons[son_index].Play();
		son_index++;

		if(son_index > 4)
			son_index = 0;
	}

	public void premierSouffle()
	{
		//Debug.Log("premierSouffle "+Time.time);
		StartCoroutine ("BoucleEnvoi");
		StartCoroutine (Coroutine ());

		joueSon();

	}

	IEnumerator Coroutine () {
		while (soufflant) {
			yield return null;
		}
		//Debug.Log("Stop "+Time.time);
		StopCoroutine("BoucleEnvoi");
		yield return null;
	}

	IEnumerator BoucleEnvoi ()
	{
		//Debug.Log("Begin BoucleEnvoi "+Time.time);
		while(true)
		{
			if (soufflant) {
				CreeEtLanceSouffle();
			}
			else
			{
				//Debug.Log("BoucleEnvoi yield break "+Time.time);
				yield break;

			}
			yield return new WaitForSeconds(intervalle);
		//StartCoroutine (BoucleEnvoi ()); //si while false, loop
		}

		yield return null;
	}

	void CreeEtLanceSouffle(bool changeCouleur = false)
	{
		//Debug.Log("CreeEtLanceSouffle a "+Time.time);
		GameObject child = Instantiate(prefabSouffleur) as GameObject;

		//child.transform.SetParent(transform);
		if(changeCouleur)
			child.transform.FindChild("Sphere").GetComponent<MeshRenderer>().material.color = new Color(
				Random.Range(0.0f,1.0f),
				Random.Range(0.0f,1.0f),
				Random.Range(0.0f,1.0f));


		//lancer le souffle de cet enfant
		child.transform.position = transform.position;
		child.transform.Rotate(GameObject.Find("Camera").transform.rotation.eulerAngles);

		child.GetComponent<Souffleur> ().LanceSouffle();

		//meme position que SouffleurParent, meme orientation que la camera

		//et là l'orpheliner
		//child.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		volume = MicInputer.GetComponent<MicInput> ().MicLoudness;
	}
}
