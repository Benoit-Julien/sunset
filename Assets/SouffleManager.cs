using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine.Audio;
using System.Deployment.Internal;

public class SouffleManager : MonoBehaviour {

	public GameObject MicInputer;

	private float volume;

	public GameObject prefabSouffleur;

	public float intervalle = 0.5f;
	public int nombre = 0;

	public bool soufflant = false;

	public AudioMixer mixer;

	public int son_index = 0;
	public AudioSource[] sons ;

	public AnimationCurve courbe;
	public bool[] fadingOut = {false, false, false, false, false};
	public float[] fadeStartTime = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f};


	// Use this for initialization
	void Start () {
		//sons = new AudioSource[] {son0, son1, son2, son3, son4};

		//pour tests

		/*
		joueSon();
		FadeOut();
		*/

	}


	public void Soufflant(bool param)
	{
		soufflant = param;
	}

	private void joueSon()
	{
		son_index++;

		if(son_index > 4)
			son_index = 0;

		sons[son_index].Play();

	}

	public void premierSouffle()
	{
		Debug.Log("premierSouffle "+Time.time);
		StartCoroutine ("BoucleEnvoi");
		StartCoroutine (Coroutine ());

		//son_index est incrémenté dans joueSon à chaque PremierSouffle
		joueSon();


	}

	private void FadeOut()
	{
		Debug.Log("FadeOut "+Time.time);
		fadingOut[son_index] = true;
		fadeStartTime[son_index] = Time.time;
	}

	IEnumerator Coroutine () {
		while (soufflant) {
			yield return null;
		}
		Debug.Log("Stop "+Time.time);
		StopCoroutine("BoucleEnvoi");

		FadeOut();

		yield return null;
	}

	//Envoi de souffle si soufflant
	IEnumerator BoucleEnvoi ()
	{
		Debug.Log("Begin BoucleEnvoi "+Time.time);
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
	void Update ()
	{
		volume = MicInputer.GetComponent<MicInput> ().MicLoudness;

		float duree = 2.0f;

		//pour chaque fade possible
		for (int i =0; i< 5; i++)
		{

			if(fadingOut[i])
			{
				float val = courbe.Evaluate(	(Time.time-fadeStartTime[i])/duree);
				float fade = -80*val;

				//Debug.Log(val +" / "+fade+" > "+Time.time);

					mixer.SetFloat("volCh"+i, fade);
				
				if(val == 1.0f)
				{
					Debug.Log("val == 1.0f");
					sons[i].Stop();
					fadingOut[i] = false;

					mixer.SetFloat("volCh"+i, -0.1f);
				
				}
			}
			else
			{
				//inutile, test anti cliquetis
				mixer.SetFloat("volCh"+i, -0.1f);
			}
		}
	}
}
