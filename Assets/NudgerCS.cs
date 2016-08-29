using UnityEngine;
using System.Collections;
using System;
using Leap;
using UnityEngine.UI;

public class NudgerCS : MonoBehaviour
{

	private float RandomRotationStrength = 0.2f;
	private float FloatStrength = 8.0f;

	public float intensite = 0;

	public Vector3 camera_to_object;

	// Use this for initialization
	void Start () {

		//tests maths
		float floatpi = (float)Math.PI;
		float a = 1.0f * floatpi; // période, pas touche

		//Debug.Log("=> "+Triangle(0,1,a,-floatpi/2, -floatpi/2.0f));

		//La formule Triangle avec (0,1,Pi,-Pi/2, X) donne bien
		//Triangle(0,1,(float)Math.PI,-(float)Math.PI/2, valeur)
		// X:0 => 1
		// X:Pi/8 => 0.75
		// X:Pi/4 => 0.5
		// X:Pi/2 => 0
		// X:3Pi/4 => 0.5
		// X:Pi => 1

		// X:-Pi/2 => 0

		//et etc. en triangle toujours entre 0 et 1


		//	StartCoroutine(NudgeRoutine()); //start random nudge loop

	}

	float Triangle(float minLevel, float maxLevel, float period, float phase, float t) {
		float pos = Mathf.Repeat(t - phase, period) / period;

		if (pos < .5f) {
			return Mathf.Lerp(minLevel, maxLevel, pos * 2f);
		} else {
			return Mathf.Lerp(maxLevel, minLevel, (pos - .5f) * 2f);
		}  
	}

	public IEnumerator NudgeRoutine()
	{
		yield return new WaitForSeconds(0.0f+UnityEngine.Random.Range(0.0f, 5.0f));
		Nudge();

		StartCoroutine(NudgeRoutine());

	}


	public void Nudge()
	{
		var dist = Vector3.Distance(transform.position, new Vector3(0,0,0));
		if(dist > 5)
		{
			GetComponent<Rigidbody>().AddForce( new Vector3(
				(-transform.position.x) *  FloatStrength * 0.1f,
				(-transform.position.y) *  FloatStrength * 0.1f,
				(-transform.position.z) *  FloatStrength * 0.1f)
			);

			//todo : plutot un système de vitesses sur 3 axes pour flotter sans a-coups, 
			//avec accelerations & damping
		}
		else
			GetComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere * FloatStrength);
	}
		









	// Update is called once per frame
	void Update ()
	{
		GameObject tete =  GameObject.Find("Camera");

		camera_to_object = new Vector3();
		camera_to_object.x = this.transform.position.x - tete.transform.position.x;
		camera_to_object.y = this.transform.position.y - tete.transform.position.y;
		camera_to_object.z = this.transform.position.z - tete.transform.position.z;
		camera_to_object.Normalize();

		Vector3 tete_forward = tete.transform.forward; //note: pourrait suffir pour force vectorielle TC particles ?
		Vector3 vecDiff = tete_forward - camera_to_object;

		//si on prend forward et camera_to_object, que les deux sont normalisés (et c'est le cas)
		//alors la magnitude de leur difference vectorielle correspond à la longueur de la corde sur le cercle trigo
		//entre ces 2 points, donc leur écart.
		//todo : test
		float ecart =  vecDiff.magnitude;

		//sinon on avait
		float moyDiff =  (Math.Abs(vecDiff.x) + Math.Abs(vecDiff.y) + Math.Abs(vecDiff.z))/3.0f;
		intensite = 1.0f-moyDiff;

		//ou avec ecart de corde trigo
		intensite = 2.0f-ecart; //2 car cercle trigo diametre=2 (longueur max d'une corde quand objet dans le dos)

		//debug ici OK qd on a UNE SEULE BOULE

		//debug à l'ecran
		GameObject texte =  GameObject.Find("Text");

		texte.GetComponent<Text>().text = 
			"intensite = "+roundTo100(intensite)+
			"\n----------------"+
			"\vecDiff.x "+roundTo100(vecDiff.x)+
			"\vecDiff.y "+roundTo100(vecDiff.y)+
			"\vecDiff.z "+roundTo100(vecDiff.z);
	}


	//au moment ponctuel du souffle on déclenche cette fonction avec les valeurs actuelles selon update()
	public void NudgeTo(Vector3 direction, float force)
	{

		transform.Rotate(0, 0, 0);

		float intensite_effective = intensite;

		if(intensite_effective < 0.3f) //palier bas
			intensite_effective = 0.0f;

		//intensite_effective *= 10.0f;

		float distance = 2.0f / camera_to_object.magnitude;

		force *= 8.0f;
	
		float forceX = distance * (50.0f * force *intensite_effective * camera_to_object.x) ;
		float forceY = distance * (50.0f * force *intensite_effective * camera_to_object.y) ;
		float forceZ = distance * (50.0f * force *intensite_effective * camera_to_object.z) ;

		GetComponent<Rigidbody>().AddForce(new Vector3(forceX, forceY, forceZ));
	}


















	//outil pour arrondir a 0.01ème
	float roundTo100(float nombre)
	{
		return (float)Math.Round(nombre*100.0f)/100.0f;

	}


}
