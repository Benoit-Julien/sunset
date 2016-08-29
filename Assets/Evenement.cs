using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection; 

public class Evenement : MonoBehaviour
{

	private List<float> liste_valeurs = new List<float>();

	public float peak = 0.0f;
	public float average = 0.0f;
	//public float duree = 0.0f;

	public int compte = 0;

	public bool souffle = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		average = 0.0f;

		for (int i=0; i< this.liste_valeurs.Count; i++)
		{
			average +=  this.liste_valeurs[i];

			peak = Math.Max(peak, this.liste_valeurs[i]);

		}

		if(this.liste_valeurs.Count != 0)
		average /= this.liste_valeurs.Count;

		compte = this.liste_valeurs.Count;

	}

	public void AjouteValeur(float val)
	{
		this.liste_valeurs.Add(val);

	}
		
}
