using UnityEngine;
using System.Collections;

public class MicInput : MonoBehaviour
{

	public float MicLoudness;

	private string _device;

	//mic initialization
	void InitMic(){
		if(_device == null) _device = Microphone.devices[0];
		_clipRecord = Microphone.Start(_device, true, 999, 44100);
	}

	void StopMicrophone()
	{
		Microphone.End(_device);
	}


	AudioClip _clipRecord = new AudioClip();
	int _sampleWindow = 128;

	//get data from microphone into audioclip
	float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
		if (micPosition < 0) return 0;
		_clipRecord.GetData(waveData, micPosition);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _sampleWindow; i++) {
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak) {
				levelMax = wavePeak;
			}
		}
		return levelMax;
	}


	void Start()
	{
		InitMic();
		_isInitialized=true;
	}

	void Update()
	{
		// levelMax equals to the highest normalized value power 2, a small number because < 1
		// pass the value to a static var so we can access it from anywhere
		MicLoudness = LevelMax ();


		/*
		float[] spectrum = new float[256];

		AudioListener.GetSpectrumData( spectrum, 0, FFTWindow.Rectangular );

		Debug.Log(spectrum[128]);

		for( int i = 1; i < spectrum.Length-1; i++ )
		{
			Debug.DrawLine( new Vector3( i - 1, spectrum[i] + 10, 0 ), new Vector3( i, spectrum[i + 1] + 10, 0 ), Color.red );
			Debug.DrawLine( new Vector3( i - 1, Mathf.Log( spectrum[i - 1] ) + 10, 2 ), new Vector3( i, Mathf.Log( spectrum[i] ) + 10, 2 ), Color.cyan );
			Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), spectrum[i - 1] - 10, 1 ), new Vector3( Mathf.Log( i ), spectrum[i] - 10, 1 ), Color.green );
			Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), Mathf.Log( spectrum[i - 1] ), 3 ), new Vector3( Mathf.Log( i ), Mathf.Log( spectrum[i] ), 3 ), Color.blue );
		}
		*/
	}

	bool _isInitialized;
	// start mic when scene starts
	void OnEnable()
	{
		
	}

	//stop mic when loading a new level or quit application
	void OnDisable()
	{
		StopMicrophone();
	}

	void OnDestroy()
	{
		StopMicrophone();
	}


	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus) {
		
	}

}