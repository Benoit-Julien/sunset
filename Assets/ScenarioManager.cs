using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using DigitalRuby.Tween;
using Leap.Unity;

public class ScenarioManager : MonoBehaviour {

	public int sceneActuelle = 0;

	//public MeshManager manager;
	public RoomCylinderManager manager;

	public MicVerb micverb;

	bool AddedLayer = false;

	public Material particleMaterialSunset;


	IEnumerator TransitionScene()
	{
		/*
		//test pour changer couleur ambient unistorm, chaque couleur de Color, donc 3 tweens
		TweenFactory.Tween("unsitorm_ambient_changeR", GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.r, 0.2f, 1.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.r = t.CurrentValue;			}, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.r = t.CurrentValue;			});
		
		TweenFactory.Tween("unsitorm_ambient_changeG", GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.g, 0.0f, 1.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.g = t.CurrentValue;			}, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.g = t.CurrentValue;			});


		TweenFactory.Tween("unsitorm_ambient_changeB", GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.b, 0.0f, 1.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.b = t.CurrentValue;			}, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().MiddayAmbientLight.b = t.CurrentValue;			});
		*/

		//TODO : ou changement immédiat pendant le blanc, bien sur 


		//ajouter bloom supplementaire transition

		/*
		bool endBloomAjout = false, endBloomBasique = false;
		float timeTween = 2.0f;

		BloomOptimized bloomBasique = GameObject.Find("CameraObjets").GetComponents<BloomOptimized>()[0];

		BloomOptimized bloomAjout = GameObject.Find("CameraObjets").GetComponents<BloomOptimized>()[1];

		TweenFactory.Tween("bloomAjout.intensity", bloomAjout.intensity, 2.5f, timeTween,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloomAjout.intensity = t.CurrentValue;								}, 
			(t) => {				bloomAjout.intensity = t.CurrentValue; endBloomAjout = true;		});


		TweenFactory.Tween("bloomBasique.threshold", bloomBasique.threshold, 0.0f, timeTween,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloomBasique.threshold = t.CurrentValue;							}, 
			(t) => {				bloomBasique.threshold = t.CurrentValue; endBloomBasique = true;	});


		//tween bloom
		Debug.Log("tween bloom");

		while (!endBloomAjout || !endBloomBasique)
			yield return null;

		Debug.Log ("end bloom");

		//yield return new WaitForSeconds(2.0f);

		Debug.Log("fade blanc");
		//fade blanc



		yield return new WaitForSeconds(2.0f);

		bloomAjout.enabled = false;


		Debug.Log("la suite");

		particleMaterialSunset.SetColor("_TintColor", new Color(0.5f,0.5f,0.5f,0.5f));
*/

		/*
		
		particleMaterialSunset.SetColor("_TintColor", new Color(0.5f,0.5f,0.5f,0.5f));

		micverb.audioSource.enabled = false;

		//TODO : reactiver le son de chimes (soit coupé via mixer soit via audiosource sur camera)

		//TEST : changer les valeurs du bloom via un tween pour coller aux valeurs de la scene en extérieur
		BloomOptimized bloom = GameObject.Find("CameraObjets").GetComponent<BloomOptimized>();

		TweenFactory.Tween("bloom.threshold", 0.25f, 0.65f, 3.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloom.threshold = t.CurrentValue;			}, 
			(t) => {				bloom.threshold = t.CurrentValue;			});

		TweenFactory.Tween("bloom.intensity", 1.3f, 1.8f, 3.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloom.intensity = t.CurrentValue;			}, 
			(t) => {				bloom.intensity = t.CurrentValue;			});

		TweenFactory.Tween("bloom.blurSize", 4.1f, 1.05f, 3.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloom.blurSize = t.CurrentValue;			}, 
			(t) => {				bloom.blurSize = t.CurrentValue;			});

		TweenFactory.Tween("bloom.blurIterations", bloom.blurIterations, 2.0f, 3.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				bloom.blurIterations = (int)t.CurrentValue;			}, 
			(t) => {				bloom.blurIterations = (int)t.CurrentValue;			});

		//tweens heure de unistorm pour faire tomber le soleil  vers 18h
		TweenFactory.Tween("bloom.blurIterations", GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().startTime, 0.75f, 3.00f,TweenScaleFunctions.QuinticEaseInOut, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().startTime = t.CurrentValue;			}, 
			(t) => {				GameObject.Find("UniStormSystemEditor").GetComponent<UniStormWeatherSystem_C>().startTime = t.CurrentValue;			});

		*/

		yield return null;
	}

	// Use this for initialization
	void Start ()
	{
		//activer SSAO sur camera APRES démarrage pour éviter bug
		GameObject.Find("CameraObjets").GetComponent<ScreenSpaceAmbientOcclusion>().enabled = true;

		particleMaterialSunset.SetColor("_TintColor", new Color(0.5f,0.5f,0.5f,0.0f));

		//manager.StartCreating ();

		AddTagRecursively (manager.gameObject, 8);
		AddTagRecursively (GameObject.Find("CameraObjects"), 8);

		float x = manager.RadiusX;
		float z = manager.RadiusZ;
		Debug.Log(x+", "+z);

		//GameObject.Find("Piedestal").transform.position =  new Vector3(x, 0.7f, 0.0f); 
	}


	void AddTagRecursively(GameObject obj, int layer)
	{
		if (obj == null)
			return;
		obj.layer = layer;
		if(obj.transform.GetChildCount() > 0)
			for (int i=0; i< obj.transform.GetChildCount(); i++)
			{
				GameObject t = obj.transform.GetChild(i).gameObject;
				AddTagRecursively(t, layer);
			}
	}

	// Update is called once per frame
	void Update () {

		//clic souris demarre transition


		//TODO : leap Proximity Detector lance Anim particules + transition
		if(Input.GetMouseButtonDown(0) && sceneActuelle == 0)
		{
			sceneActuelle = 1;

			//StartCoroutine ("TransitionScene");
		}
	}

	void OnDisable () {
		particleMaterialSunset.SetColor("_TintColor", new Color(0.5f,0.5f,0.5f,0.0f));
	}
}
