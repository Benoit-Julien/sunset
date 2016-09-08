using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicVerb : MonoBehaviour {

	public AudioSource audioSource = null;
	AudioClip _clipRecord = new AudioClip();
	int _sampleWindow = 128;

	public string _device;

	//public float MicLoudness;

	// Use this for initialization
	void Start () 
	{
		if(_device == null) 
			_device = Microphone.devices[0];

		//Debug.Log(_device);

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = Microphone.Start(_device, true, 999, 44100);

		while(!( Microphone.GetPosition(null) > 0))
		{}
		audioSource.Play();

	}

	/*
	float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone

		Debug.Log(micPosition);

		if (micPosition < 0) return 0;
		audioSource.clip.GetData(waveData, micPosition);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _sampleWindow; i++) {
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak) {
				levelMax = wavePeak;
			}
		}
		return levelMax;
	}
	*/

	
	// Update is called once per frame
	void Update () {

		//MicLoudness = LevelMax ();

	}
}
