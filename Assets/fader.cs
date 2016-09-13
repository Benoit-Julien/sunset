using UnityEngine;
using System.Collections;

public class fader : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float speed = 0.8f;

	private float alpha = 1.0f;
	private int fadeDir = -1;
	private int drawDepth = -1000;

	public AnimationCurve courbe;

	private float startTime;

	void OnGUI()
	{
		/*
		alpha += fadeDir * speed * Time.deltaTime;
			alpha = Mathf.Clamp01(alpha);
		*/

		alpha = courbe.Evaluate( 1.0f-Mathf.Clamp01((Time.time - startTime)/speed)	);

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), fadeOutTexture);

	}



	public float BeginFade(int direction)
	{
		fadeDir = direction;
		startTime = Time.time;
		return speed;
	}

	void OnLevelWasLoaded()
	{
		BeginFade(-1);
	}
}
