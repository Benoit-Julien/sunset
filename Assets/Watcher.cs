using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


//surveille le volume micro et declenche selon
using UnityEngine.Events;

public class Watcher : MonoBehaviour
{
	public MicInput input;
	public float actuelle;
	public float seuil = 0.0001f; // 0.00005f; 
	public bool en_mesure_silence = false;
	public float timeLeft = 0.2f;
	public Evenement courant;

	//debug editor
	public float courant_average;
	public float courant_peak;
	public int courant_compte;
	public bool courant_souffle;

	private SouffleManager manager;

	public UnityEvent SonCommence;

	// Use this for initialization
	void Start ()
	{
		manager =  GameObject.Find("SouffleurParent").GetComponent<SouffleManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		actuelle = input.MicLoudness;
	
		if (input.MicLoudness < seuil)
		{
			//Debug.Log("Silence.");


			timeLeft -= Time.deltaTime;

			if(timeLeft <= 0 && !en_mesure_silence)
			{
				manager.soufflant = false;
				//Debug.Log("courant CREE");
				courant = this.gameObject.AddComponent<Evenement>();
				en_mesure_silence = true;


			}
		}
		else
		{
			if(en_mesure_silence)
			{
				//Debug.Log("premiere valeur > seuil a "+Time.time);
			
			}

			timeLeft = 0.2f;
			en_mesure_silence = false;

			if(courant) 
			{
				courant.AjouteValeur(input.MicLoudness);

				//Debug.Log("courant check ses valeurs.");
							


				if(/*courant.average > 0.0005f &&*/ courant.peak > 0.001f /*&& courant.compte > 2 && !courant.souffle*/ /*&& courant.peak < 0.1f*/)
					{
						//Debug.Log("EVENEMENT SOUFFLE a "+Time.time);
						courant.souffle = true; //on autorise la scene a utiliser les valeurs de courant pour ses forces de souffle
						//les valeurs vont evoluer

						//todo : do an action here

						bool avant = manager.soufflant;


						manager.soufflant = true;

						if(!avant)
						{
						manager.premierSouffle();
						}



						//Invoke("SonCommence", 0.0f);
					}
				else
				{
						//Debug.Log("courant est null");

					courant.souffle = false;

				}

			}
		}

		if(courant)
		{
		courant_average = courant.average;
		courant_peak = courant.peak;
		courant_compte = courant.compte;
		courant_souffle = courant.souffle;
		}
		else
		{
						//Debug.Log("courant n'existe pas");
		}

	}
}

//