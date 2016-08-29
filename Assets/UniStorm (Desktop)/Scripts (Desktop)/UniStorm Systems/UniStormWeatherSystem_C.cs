//UniStorm Weather System C# Version 2.2.0 @ Copyright
//Black Horizon Studios

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using System;

public class UniStormWeatherSystem_C : MonoBehaviour {

	public int CloudDensity = 100;

	public bool disableTemperatureGeneration = false;
	public bool useMoonLightShafts = true;

	public float SunIntensityMultiplier = 1;

	public int PrecipitationType = 1;
	public bool hourlyUpdate = false;

	public float CloudHeight = 1700.0f;
	public float CloudFade = 1699.0f;

	public GameObject sunTrans;

	public AnimationCurve moonIntensityCurve = AnimationCurve.Linear(0,0,24,5);

	public float tempSunIntensity;
	public float tempMoonIntensity;

	public AnimationCurve sunIntensityCurve = AnimationCurve.Linear(0,0,24,5);
	public float sunIntensityCalculator;

	public float CurrentTemperatureFloat;

	public float ambientLightMultiplier = 0.75f; 
	
	public float PreciseCurveTime;
	public float roundingCorrection;

	public float CurrentPrecipitationAmountFloat = 1;
	public int CurrentPrecipitationAmountInt;

	public AnimationCurve TemperatureCurve = AnimationCurve.Linear(1,-100,13,125);
	public AnimationCurve PrecipitationGraph = AnimationCurve.Linear(1,0,13,100);
	public AnimationCurve TemperatureFluctuationn = AnimationCurve.Linear(0,-25,24,25);

	public int AutoCalculateAmbientIntensity = 2;

	public float TwilightAmbientIntensity = 1.0f;
	public float MorningAmbientIntensity = 0.75f;
	public float DayAmbientIntensity = 0.5f;
	public float EveningAmbientIntensity = 0.75f;
	public float NightAmbientIntensity = 0.5f;

	public int HourToChangeWeather;
	public int NextForecast;
	public string WeatherTypeGenerated;
	public bool WeatherGenerated = false;

	public int[] PrecipitationWeatherTypes = {1, 2, 3, 4, 12};
	public int[] ClearWeatherTypes = {4, 7, 8, 10, 11, 13};

	public bool precipitationWeatherTypeGenerated = false;

	public int precipitationOdds = 70;
	public int precipitationOddsSpring = 30;
	public int precipitationOddsSummer = 80;
	public int precipitationOddsFall = 50;
	public int precipitationOddsWinter = 30;
	public int generatedOdds;

	public float RegularCloudFadeInMultiplier = 1;
	public float RegularCloudFadeOutMultiplier = 1;

	public float CloudFadeInMultiplier = 1;
	public float CloudFadeOutMultiplier = 1;

	public float SoundFadeInMultiplier = 1;
	public float SoundFadeOutMultiplier = 1;

	public float EffectsFadeInMultiplier = 1;
	public float EffectsFadeOutMultiplier = 1;

	public float FogDistanceFadeInMultiplier = 1;
	public float FogDistanceFadeOutMultiplier = 1;

	public float FogColorFadeInMultiplier = 1;
	public float FogColorFadeOutMultiplier = 1;

	public float ParticleEffectsFadeInMultiplier = 1;
	public float ParticleEffectsFadeOutMultiplier = 1;

	public float WindFadeInMultiplier = 1;
	public float WindFadeOutMultiplier = 1;

	public Color nightTintColor;
	public int generateDateAndTime = 1;

	public Color stormCloudColor1;
	public Color stormCloudColor2;

	//Gets all our components on start and stores them here
	Renderer starSphereComponent;
	Renderer heavyCloudsComponent;
	public Renderer moonObjectComponent;

	//Dyanmic Clouds
	Renderer mostlyClearClouds1Component;
	Renderer heavyCloudsLayerLightComponent;
	Renderer partlyCloudyClouds1Component;
	Renderer mostlyCloudyClouds1Component;

	Light sunComponent;
	Light moonComponent;
	Light lightSourceComponent;

	AudioSource soundComponent;
	public AudioSource rainSoundComponent;
	public AudioSource windSoundComponent;
	public AudioSource windSnowSoundComponent;

	ParticleEmitter mistFogComponent;

	public Camera cameraObjectComponent;

	public bool useInstantStartingWeather;

	public float sunCalculator;

	public int environmentType;

	public float startTimeHour;
	public int startTimeMinute;
	//public float startTimeMinuteFloat;

	public float sunHeight = 0.95f;
	public float mostlyCloudyFloat;
	public float mostlyClearFloat;
	public float colorFogFadeInFloat;
	public float sunPosition = 90;

	public Color skyColorMorning; 
	public Color skyColorDay; 
	public Color skyColorEvening; 
	public Color skyColorNight;

	public string weatherString;
	public bool menuEnabled = false;
	bool moonChanger = false;

	public GameObject menuObject;

	public int StormyMoonLightIntensity;

	public bool timeOptions = true;
	public bool caledarOptions = true;
	public bool skyOptions = true;
	public bool atmosphereOptions = true;
	public bool fogOptions = true;
	public bool lightningOptions = true;
	public bool temperatureOptions = true;
	public bool sunOptions = true;
	public bool moonOptions = true;
	public bool precipitationOptions = true;
	public bool GUIOptions = true;
	public bool soundManagerOptions = true;
	public bool colorOptions = true;
	public bool objectOptions = true;
	public bool helpOptions = true;
	public bool WindOptions = true;

	public bool terrainDetection = false;

	public float nightLength;
	public int nightLengthHour;
	public int dayLengthHour;
	public int nightLengthMinute;
	public int dayLengthMinute;

	public float stormGrassWavingSpeed;
	public float stormGrassWavingAmount;
	public float stormGrassWavingStrength;
	public float normalGrassWavingSpeed;
	public float normalGrassWavingAmount;
	public float normalGrassWavingStrength;

	public int StormySunIntensity;

	public string timeOfDayDisplay;
	public GameObject sunObject;
	
	public int calendarType = 0;
	public int numberOfDaysInMonth = 31;
	public int numberOfMonthsInYear = 12;
	
	public GameObject partlyCloudyClouds1;

	public GameObject mostlyCloudyClouds1;

	public float partlyCloudyFloat;

	public int cloudType = 1;
	public bool UseRainSplashes = true;
	public bool UseRainMist = true;
	public ParticleSystem rainSplashes;
	public ParticleSystem snow; 
	public ParticleSystem snowMistFog; 
	public float sunOffSetX;
	public float sunOffSetY;
	public Vector2 sunOffSet = new Vector2( 0.0f, 0.0f );

	public float dynamicPartlyCloudyFloat1;
	public float dynamicPartlyCloudyFloat2;
	
	public GameObject dynamicPartlyCloudy1;
	public GameObject dynamicPartlyCloudy2;
	
	public int springTemp = 0;
	public int summerTemp = 0;
	public int fallTemp = 0;
	public int winterTemp = 0;
	
	public bool weatherShuffled = false;
	
	public float minHeavyRainMistIntensity = 0;
	public int maxHeavyRainMistIntensity = 20;
	
	public ParticleSystem rainMist;
	
	public float moonSize = 4;
	public float moonRotationY = 4;
	public bool customMoonSize = false;
	public bool customMoonRotation = false;
	public Quaternion moonRotation = Quaternion.identity;

	public Color cloudColorTwilight;
	public Color cloudColorMorning; 
	public Color cloudColorDay; 
	public Color cloudColorEvening; 
	public Color cloudColorNight;

	public Color stormyFogColorTwilight;
	public Color stormyFogColorMorning; 
	public Color stormyFogColorDay; 
	public Color stormyFogColorEvening; 
	public Color stormyFogColorNight; 

	public Color originalFogColorTwilight;
	public Color originalFogColorMorning; 
	public Color originalFogColorDay; 
	public Color originalFogColorEvening; 
	public Color originalFogColorNight;
	
	public float FogFadeInFloat = 0;
	public float FogFadeOutFloat = 0;
	
	public bool weatherHappened = false;
	
	public float moonFade = 0;
	public float moonFade2 = 0;
	
	public Color moonColorFade;
	
	public int temperatureType = 1;
	public int temperatureControlType = 1;
	public int temperature_Celsius = 1;
	
	public bool stormControl = true;
	
	public int forceWeatherChange = 0;

	public int randomizedRainIntensity;
	public int currentGeneratedIntensity;
	public int lastWeatherType;
	public bool randomizedPrecipitation = false;
	public int moonShadowQuality = 2;

	public bool useRainStreaks = true;
	
	public int timeToWaitMin;
	public int timeToWaitMax;
	public int timeToWaitCurrent = 3;
	
	public float TODSoundsTimer;
	public bool playedSound = false;
	public bool getDelay = false;
	public int amountOfSounds;
	public bool useMorningSounds = false;
	public bool useDaySounds = false;
	public bool useEveningSounds = false;
	public bool useNightSounds = false;
	
	public Material tempMat;
	public Color lightningColor;
	
	public int morningSize;
	public int daySize;
	public int eveningSize;
	public int nightSize;
	public List<AudioClip> ambientSoundsMorning = new List<AudioClip>();
	public List<AudioClip> ambientSoundsDay = new List<AudioClip>();
	public List<AudioClip> ambientSoundsEvening = new List<AudioClip>();
	public List<AudioClip> ambientSoundsNight = new List<AudioClip>();
	public List<bool> foldOutList = new List<bool>();
	
	public bool shadowsDuringDay = true;
	public float dayShadowIntensity = 1;
	public int dayShadowType = 1;
	
	public bool shadowsDuringNight;
	public float nightShadowIntensity;
	public int nightShadowType = 1;
	
	public bool shadowsLightning;
	public float lightningShadowIntensity;
	public int lightningShadowType = 1;
	
	public int fogMode = 1;
	
	public string version;
	
	public int materialIndex = 0;
	
	public Vector2 uvAnimationRateA = new Vector2( 1.0f, 0.0f );
	public string CloudA = "_MainTex1";
	
	public Vector2 uvAnimationRateB = new Vector2( 1.0f, 0.0f );
	public string CloudB = "_MainTex2";
	
	public Vector2 uvAnimationRateC = new Vector2( 1.0f, 0.0f );
	public string CloudC = "_MainTex3";

	public Vector2 uvAnimationRateStars = new Vector2( 1.0f, 0.0f );
	
	public Vector2 uvAnimationRateHeavyA = new Vector2( 1.0f, 0.0f );
	public Vector2 uvAnimationRateHeavyB = new Vector2( 1.0f, 0.0f );
	public Vector2 uvAnimationRateHeavyC = new Vector2( 1.0f, 0.0f );
	
	Vector2 uvOffsetA = Vector2.zero;
	Vector2 uvOffsetB = Vector2.zero;
	Vector2 uvOffsetC = Vector2.zero;
	
	Vector2 uvOffsetHeavyA = Vector2.zero;
	Vector2 uvOffsetHeavyB = Vector2.zero;
	Vector2 uvOffsetHeavyC = Vector2.zero;

	Vector2 uvOffsetStars = Vector2.zero;
	
	public int cloudDensity;
	
	public AudioClip customRainSound;
	public AudioClip customRainWindSound;
	public AudioClip customSnowWindSound;
	
	public bool useCustomPrecipitationSounds = false;
	
	public bool useUnityFog = true;
	
	public float moonLightIntensity;
	public Color moonColor;

	//Time keeping variables
	public int minuteCounter = 0; 
	public int minuteCounterNew = 0;
	public string minute;
	public int hourCounter = 0; 
	public int hourCounter2 = 0;
	public int dayCounter = 0; 
	public int monthCounter = 0; 
	public int yearCounter = 0; 
	public int temperature = 0; 
	public float dayLength;
	public int cloudSpeed;
	public int heavyCloudSpeed;
	public int starSpeed;
	public float starSpeedCalculator;
	public float starRotationSpeed;
	public bool timeStopped = false;
	public bool staticWeather = false;
	public bool timeScrollBar = false;
	public bool horizonToggle  = true;
	public bool dynamicSnowEnabled = true;
	public bool weatherCommandPromptUseable = false;
	public bool timeScrollBarUseable = false;
	public float startTime;
	public float moonPhaseChangeTime;
	public int weatherForecaster = 0;
	private string stringToEdit = "0";
	
	public float SnowAmount;
	
	//Sun intensity control
	public float sunIntensity;
	public float maxSunIntensity; 
	
	//Sun angle control
	public float sunAngle;
	
	//Ambient light colors
	public Color MorningAmbientLight;
	public Color MiddayAmbientLight;
	public Color DuskAmbientLight;
	public Color TwilightAmbientLight;
	public Color NightAmbientLight;
	
	//Sun colors
	public Color SunMorning;
	public Color SunDay;
	public Color SunDusk;
	public Color SunNight;
	
	//Storm color variables Global Fog
	public Color stormyFogColorDay_GF;
	public Color stormyFogColorDuskDawn_GF;
	public Color stormyFogColorNight_GF;
	
	//Fog colors
	public Color fogTwilightColor;
	public Color fogMorningColor;
	public Color fogDayColor;
	public Color fogDuskColor;
	public Color fogNightColor;
	
	public float fogDensity;
	
	//Skyboxes
	public Material SkyBoxMaterial;
	
	//Atmospheric colors
	public Color MorningAtmosphericLight;
	public Color MiddayAtmosphericLight;
	public Color DuskAtmosphericLight;
	
	//Star System game objects
	public GameObject starSphere;
	public Color starBrightness;
	public GameObject moonObject;
	
	public int moonPhaseCalculator = 0;
	public Color moonFadeColor;
	public Material moonPhase1;
	public Material moonPhase2;
	public Material moonPhase3;
	public Material moonPhase4;
	public Material moonPhase5;
	public Material moonPhase6;
	public Material moonPhase7;
	public Material moonPhase8;
	public Material moonPhase9;
	
	//Clouds game objects
	public GameObject mostlyClearClouds1;
	public GameObject heavyClouds;
	public GameObject heavyCloudsLayerLight;
	
	//Max rain particles
	public int maxLightRainIntensity = 400;
	public int maxLightRainMistCloudsIntensity = 4;
	public int maxStormRainIntensity = 1000;
	public int maxStormMistCloudsIntensity = 15;
	
	public int maxLightSnowIntensity = 400;
	public int maxLightSnowDustIntensity = 4;
	public int maxSnowStormIntensity = 1000;
	public int maxHeavySnowDustIntensity = 15;
	
	//Weather game objects
	public ParticleSystem rain;
	public ParticleSystem butterflies;
	public GameObject mistFog;
	public ParticleSystem windyLeaves;
	public GameObject windZone;
	public GameObject snowObject;
	
	public Light sun;
	public Light moon;
	
	//Storm sound effects
	public GameObject rainSound;
	public GameObject windSound;
	public GameObject windSnowSound;
	
	public GameObject cameraObject;
	
	//Our fade number values
	private float StormCloudFadeFloat1 = 0;
	private float StormCloudFadeFloat2 = 0;
	private float butterfliesFade = 0;
	private float windyLeavesIntensity = 0;
	private float fadeStars = 0;
	private float sunShaftFade = 0;
	private float windControl = 0;
	private float windControlUp = 0;

	private float dynamicSnowFade = 0;

	public float stormCounter = 0.0f;
	private float forceStorm = 0;  
	private float changeWeather = 0;  
	
	//1.6 weather commands
	private string foggy = "01";
	private string lightRain_lightSnow = "02";
	private string rainStorm_snowStorm = "03";
	private string partlyCloudy1 = "04";
	private string partlyCloudy2 = "05";
	private string partlyCloudy3 = "06";
	private string clear1 = "07";
	private string clear2 = "08";
	private string cloudy = "09";
	private string mostlyCloudy = "001";
	private string heavyRain = "002";
	private string fallLeaves = "003";
	private string butterfliesSummer = "004";
	private bool commandPromptActive = false;
	
	//Rain particle density controls
	public float currentRainIntensity;
	private float currentRainFogIntensity;
	
	//Snow particle density controls
	public float currentSnowIntensity;
	public float currentSnowFogIntensity;
	
	//Priavte vars
	//private float TimeDecimal;
	public float Hour;
	public float minuteCounterCalculator = 0; 
	public SunShafts sunShaftScript;
	public SunShafts moonShaftScript;
	
	public int weatherOdds = 0;
	public int weatherChanceSpring = 0;
	public bool isSpring;
	public int weatherChanceSummer = 0;
	public bool isSummer;
	public int weatherChanceFall = 0;
	public bool isFall;
	public int weatherChanceWinter = 0;
	public bool isWinter;
	public bool nightTime;
	
	public int stormyFogDistance;
	public int stormyFogDistanceStart;
	
	public int fogStartDistance;
	public int fogEndDistance;
	
	public Transform lightningSpawn;
	public int lightningNumber;
	
	public GameObject lightningBolt1;
	
	public bool fadeLightning;
	public bool lightingGenerated;
	
	public Light lightningLight;
	
	public AudioClip thunderSound1;
	public AudioClip thunderSound2;
	public AudioClip thunderSound3;
	public AudioClip thunderSound4;
	public AudioClip thunderSound5;
	
	//Temperature variables
	public int minSpringTemp;
	public int maxSpringTemp;
	public int minSummerTemp;
	public int maxSummerTemp;
	public int minFallTemp;
	public int maxFallTemp;
	public int minWinterTemp;
	public int maxWinterTemp;
	public int startingSpringTemp;
	public int startingSummerTemp;
	public int startingFallTemp;
	public int startingWinterTemp;
	
	//Weather Timers	
	private float LightningTimer;
	public float LightningOnTime;
	public float lightningOdds;
	public float lightningFlashLength;
	public int lightningMinChance = 5;
	public int lightningMaxChance = 10;
	
	public float minIntensity;
	public float maxIntensity;
	public float lightningIntensity;
	
	public Color MorningGradientLight;
	public Color DayGradientLight;
	public Color DuskGradientLight;
	public Color NightGradientLight;
	
	//Gradient Light Colors
	public Color MorningGradientContrastLight;
	public Color DayGradientContrastLight;
	public Color DuskGradientContrastLight;
	public Color NightGradientContrastLight;
	
	public float sunSize = 0.01f;
	public Color skyTintColor;
	public Color groundColor;
	public float atmosphereThickness;
	public float exposure;

	public int TabNumber = 1;
	public DateTime UniStormDate;
	public GameObject sunShaftsPositionObject;
	public GameObject moonPositionObject;

	public float cloudHeight = 750; 
	
	void Awake () 
	{
		if (useCustomPrecipitationSounds)
		{
			rainSound.GetComponent<AudioSource>().clip = customRainSound;
			rainSound.GetComponent<AudioSource>().enabled = false;
			
			windSound.GetComponent<AudioSource>().clip = customRainWindSound;
			windSound.GetComponent<AudioSource>().enabled = false;
			
			windSnowSound.GetComponent<AudioSource>().clip = customSnowWindSound;
			windSnowSound.GetComponent<AudioSource>().enabled = false;
		}	
	}
	
	void Start ()
	{
		//Initialize and setup UniStorm
		InitializeUniStorm();
	}

	public void InitializeUniStorm ()
	{
		CloudA = "_MainTex1";
		CloudB = "_MainTex2";
		CloudC = "_MainTex3";

		cloudType = 1;

		if (yearCounter <= 0)
		{
			yearCounter = 1;
		}

		if (useCustomPrecipitationSounds)
		{
			rainSound.GetComponent<AudioSource>().enabled = true;
			windSound.GetComponent<AudioSource>().enabled = true;
			windSnowSound.GetComponent<AudioSource>().enabled = true;
		}

		//Call the GetAllComponents to assign all of our needed components
		GetAllComponents();

		RenderSettings.skybox = SkyBoxMaterial;
		//SkyBoxMaterial.SetColor("_SkyTint", skyTintColor);
		//SkyBoxMaterial.SetColor("_GroundColor", groundColor);
		//SkyBoxMaterial.SetFloat("_AtmosphereThickness", atmosphereThickness);
		//SkyBoxMaterial.SetFloat("_Exposure", exposure);
		//SkyBoxMaterial.SetColor("_NightSkyTint", nightTintColor);
		//SkyBoxMaterial.SetFloat("_SunSize", sunSize);

		//Calculates our start time based off the user's input
		float startTimeMinuteFloat = (int)startTimeMinute;
		startTime = startTimeHour / 24 + startTimeMinuteFloat / 1440;
		HourToChangeWeather = UnityEngine.Random.Range(0,25);

		//If no terrain is found, set terrainDetection to false to disable certain terrain only features
		if (Terrain.activeTerrain == null)
		{	
			terrainDetection = false;
		}

		if (Terrain.activeTerrain != null)
		{	
			terrainDetection = true;
		}

		//If a terrain is found, set our wind values from the UniStorm Editor
		if (terrainDetection)
		{
			if (Terrain.activeTerrain.terrainData.wavingGrassSpeed <= normalGrassWavingSpeed || Terrain.activeTerrain.terrainData.wavingGrassSpeed >= normalGrassWavingSpeed)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassAmount <= normalGrassWavingStrength || Terrain.activeTerrain.terrainData.wavingGrassAmount >= normalGrassWavingStrength)
			{
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassStrength <= normalGrassWavingAmount || Terrain.activeTerrain.terrainData.wavingGrassStrength >= normalGrassWavingAmount)
			{
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		//Instantly fade in the clouds according to the current weather type
		if (cloudType == 1 && weatherForecaster == 4 || cloudType == 1 && weatherForecaster == 5 || cloudType == 1 && weatherForecaster == 6)
		{
			partlyCloudyFloat = 1;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 0;
		}

		if (cloudType == 1 && weatherForecaster == 7)
		{
			partlyCloudyFloat = 0;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 0;
		}

		if (cloudType == 1 && weatherForecaster == 8)
		{
			partlyCloudyFloat = 0;
			colorFogFadeInFloat = 0;
			mostlyCloudyFloat = 0;
		}

		if (cloudType == 1 && weatherForecaster == 11)
		{
			partlyCloudyFloat = 1;
			colorFogFadeInFloat = 1;
			mostlyCloudyFloat = 1;
		}
			
		timeScrollBar = false;
		FogFadeOutFloat = 1;

		//Call the LogErrorCheck function to check for errors or missing components
		LogErrorCheck();

		if (UseRainSplashes)
		{
			rainSplashes.gameObject.SetActive(true);
		}

		if (!UseRainSplashes)
		{
			rainSplashes.gameObject.SetActive(false);
		}

		//Grab our originally set fog values for they're altered during stormy weather types
		originalFogColorTwilight = fogTwilightColor;
		originalFogColorMorning = fogMorningColor;
		originalFogColorDay = fogDayColor;
		originalFogColorEvening = fogDuskColor;
		originalFogColorNight = fogNightColor;

		//Setup our Unity fog settings
		if (useUnityFog)
		{
			RenderSettings.fog = true;
		}

		if (fogMode == 1)
		{
			RenderSettings.fogMode = FogMode.Linear;
		}

		if (fogMode == 2)
		{
			RenderSettings.fogMode = FogMode.Exponential;
		}

		if (fogMode == 3)
		{
			RenderSettings.fogMode = FogMode.ExponentialSquared;
		}

		//Set our light intensities
		moon.color = moonColor;
		lightningLight.color = lightningColor;

		//Set dynamic cloud values
		uvAnimationRateA = new Vector2(0.001f, 0.0f);
		uvAnimationRateB = new Vector2(0.001f, 0.001f);
		uvAnimationRateC = new Vector2(0.0001f, 0.0f);

		uvAnimationRateHeavyA = new Vector2(0.005f, 0.001f);
		uvAnimationRateHeavyB = new Vector2(0.004f, 0.0035f);
		uvAnimationRateHeavyC = new Vector2(0.0001f, 0.0f);

		uvAnimationRateStars = new Vector2(0.1f, 0.0f);

		GenerateMostlyClear();
		GeneratePartlyCloudy();
		GenerateMostlyCloudy();

		CreateSun();
		CreateMoon();

		if (useRainStreaks)
		{
			mistFog.SetActive(true);
		}

		if (!useRainStreaks)
		{
			mistFog.SetActive(false);
		}

		if (RenderSettings.fogMode == FogMode.Linear)
		{
			RenderSettings.fogStartDistance = fogStartDistance;
			RenderSettings.fogEndDistance = fogEndDistance;
		}

		CalculateMonths();
		CalculateSeaon();

		//A custom date time is created for UniStorm here and updated every day.
		if (calendarType == 1)
		{
			UniStormDate = new DateTime(yearCounter, monthCounter, dayCounter);
		}

		//This algorithm uses an Animation curve to calculate the precipitation odds given the date from the Animation Curve
		roundingCorrection = UniStormDate.DayOfYear * 0.00273972602f;
		PreciseCurveTime = ((UniStormDate.DayOfYear / 28.07692307692308f)) + 1 - roundingCorrection;
		PreciseCurveTime = Mathf.Round(PreciseCurveTime * 10f) / 10f;

		CurrentPrecipitationAmountFloat = PrecipitationGraph.Evaluate(PreciseCurveTime);

		sun.intensity = sunIntensityCurve.Evaluate((float)Hour) * SunIntensityMultiplier;

		if (temperatureControlType == 1 && !disableTemperatureGeneration)
		{
			TemperatureGeneration();
		}

		if (temperatureControlType == 2 & !disableTemperatureGeneration)
		{
			CurrentTemperatureFloat = TemperatureCurve.Evaluate(PreciseCurveTime);
			temperature = (int)TemperatureCurve.Evaluate(PreciseCurveTime) + (int)TemperatureFluctuationn.Evaluate((float)startTimeHour);
		}

		if (PrecipitationType == 2)
		{
			CurrentPrecipitationAmountInt = (int)Mathf.Round(CurrentPrecipitationAmountFloat);
			precipitationOdds = CurrentPrecipitationAmountInt;
		}

		if (useInstantStartingWeather)
		{
			InstantWeather();
		}
	}

	public void CreateSun ()
	{
		//UniStorm now uses our procedural Skybox sun.
		//Below, we create a gameobject as a reference point for our sun's position
		//Once it's created, we assign it to the Sun Shafts Sun Transform
		sunTrans = GameObject.Find("Light Parent");
		sunShaftsPositionObject = new GameObject();
		sunShaftsPositionObject.name = "Sun Transform";
		sunShaftsPositionObject.transform.parent = sun.transform; 
		sunShaftsPositionObject.transform.localPosition = new Vector3 (0,0,-999999);
		sunShaftsPositionObject.transform.localRotation = Quaternion.Euler (0,0,0);
		sunShaftsPositionObject.transform.localScale = new Vector3 (1,1,1);

		//Don't try to assign sun if no Sun Shafts component is found
		if (cameraObjectComponent.GetComponent<SunShafts>() != null)
		{
			cameraObjectComponent.GetComponent<SunShafts>().sunTransform = sunShaftsPositionObject.transform;
		}
	}

	public void CreateMoon ()
	{
		//UniStorm's moon is now assigned and positioned at runtime.
		moonObject.transform.parent = moon.transform; 
		moonObject.transform.localPosition = new Vector3 (0,0,-11000);

		moonObject.transform.localEulerAngles = new Vector3(270, 0, 0);

		if (customMoonSize)
		{
			moonObject.transform.localScale = new Vector3(moonSize*10, moonSize*10, moonSize*10);
		}

		moonPhaseCalculator -= 1;

		//If useMoonLightShafts is enabled, assign light reference.
		//Don't try to assign light reference if a second Sun Shafts component isn't found
		if (useMoonLightShafts)
		{
			if (cameraObjectComponent.GetComponent<SunShafts>() != null)
			{
				SunShafts[] sunShaftsComponent = cameraObjectComponent.GetComponents<SunShafts>();

				if (sunShaftsComponent.Length >= 2)
				{
					sunShaftsComponent[1].sunTransform = moonObject.transform;
					moonShaftScript = sunShaftsComponent[1];
				}
			}
		}

		//If a second Sun Shafts component is found, but useMoonLightShafts is disabled, disable the second Sun Shafts
		if (!useMoonLightShafts)
		{
			if (cameraObjectComponent.GetComponent<SunShafts>() != null)
			{
				SunShafts[] sunShaftsComponent = cameraObjectComponent.GetComponents<SunShafts>();

				if (sunShaftsComponent.Length >= 2)
				{
					sunShaftsComponent[1].enabled = false;
				}
			}
		}

		//Run the MoonPhaseCalculator function to assign the moon phase
		MoonPhaseCalculator();
	}

	//Gets all our needed components on Start
	void GetAllComponents ()
	{
		if (lightningLight == null)
		{
			//Lightning Light
			lightningLight = GameObject.Find("Lightning Light").GetComponent<Light>();
		}

		//Renderer Components
		heavyCloudsComponent = heavyClouds.GetComponent<Renderer>();; 
		starSphereComponent = starSphere.GetComponent<Renderer>();
		mostlyClearClouds1Component = mostlyClearClouds1.GetComponent<Renderer>();
		heavyCloudsLayerLightComponent = heavyCloudsLayerLight.GetComponent<Renderer>();
		partlyCloudyClouds1Component = partlyCloudyClouds1.GetComponent<Renderer>();
		mostlyCloudyClouds1Component = mostlyCloudyClouds1.GetComponent<Renderer>();

		//Moob Object
		moonObjectComponent = moonObject.GetComponent<Renderer>();

		//Light Component
		sunComponent = sun.GetComponent<Light>();
		lightSourceComponent = lightningLight.GetComponent<Light>();
		moonComponent = moon.GetComponent<Light>();

		//Sound Components
		this.gameObject.AddComponent<AudioSource>();
		soundComponent = GetComponent<AudioSource>();
		rainSoundComponent = rainSound.GetComponent<AudioSource>();
		windSoundComponent = windSound.GetComponent<AudioSource>();
		windSnowSoundComponent = windSnowSound.GetComponent<AudioSource>();

		//Particle Components
		mistFogComponent = mistFog.GetComponent<ParticleEmitter>();

		//Camera Component
		cameraObjectComponent = cameraObject.GetComponent<Camera>();

		//SunShafts Component
		sunShaftScript = cameraObjectComponent.GetComponent<SunShafts>();
		
		if (sunShaftScript == null)
		{
			Debug.LogError("Please apply a C# Sun Shaft Script to your camera GameObject.");
		}
	}
		
	void Update () 
	{
		//UniStorm 2.2 uses an Animation Curve for calculating the intenisty of the sun.
		//This allows user to control the precise intentensity throughout the day all the way up till the time the sun sets.
		//This allows the sun to be as bright as needed up until it sets where the intensity can be faded to 0 based on the exact time it's needed.
		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13)
		{
			if (tempSunIntensity >= sunIntensityCurve.Evaluate((float)Hour))
			{
				sun.intensity = sunIntensityCurve.Evaluate((float)Hour) * SunIntensityMultiplier;
				tempSunIntensity = 1;
			}

			if (tempMoonIntensity >= moonIntensityCurve.Evaluate((float)Hour))
			{
				tempMoonIntensity = 1;
				moon.intensity = moonIntensityCurve.Evaluate((float)Hour);
			}
				
			if (tempSunIntensity < sunIntensityCurve.Evaluate((float)Hour))
			{
				tempSunIntensity += Time.deltaTime * 0.03f * EffectsFadeInMultiplier;

				if (tempSunIntensity >= 1)
				{
					tempSunIntensity = 1;
				}

				sun.intensity = (sunIntensityCurve.Evaluate((float)Hour) * tempSunIntensity) * SunIntensityMultiplier;
			}
				
			if (tempMoonIntensity < moonIntensityCurve.Evaluate((float)Hour))
			{
				tempMoonIntensity += Time.deltaTime * 0.03f * EffectsFadeInMultiplier;

				if (tempMoonIntensity >= 1)
				{
					tempMoonIntensity = 1;
				}

				moon.intensity = moonIntensityCurve.Evaluate((float)Hour) * tempMoonIntensity;
			}

		}

		//This handles fading the sun and moon according to the percentage allowed vai the editor.
		//If the sun's setting is %50, then the sun intensity will be faded to %50 intensity during precipitation weather types.
		//The same applies to the moon with the moon settings
		if (weatherForecaster == 1 || weatherForecaster == 2 || weatherForecaster == 3 || weatherForecaster == 12 || weatherForecaster == 9)
		{
			tempSunIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;

			if (tempSunIntensity <= StormySunIntensity * 0.01f)
			{
				tempSunIntensity = StormySunIntensity * 0.01f;
			}

			sun.intensity = (sunIntensityCurve.Evaluate((float)Hour) * tempSunIntensity) * SunIntensityMultiplier;

			tempMoonIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;

			if (tempMoonIntensity <= StormyMoonLightIntensity * 0.01f)
			{
				tempMoonIntensity = StormyMoonLightIntensity * 0.01f;
			}

			moon.intensity = moonIntensityCurve.Evaluate((float)Hour) * tempMoonIntensity;

			if (moonComponent.intensity <= 0.01f)
			{
				moonComponent.enabled = false;
			}

			if (moonComponent.intensity > 0.01f)
			{
				moonComponent.enabled = true;
				moonComponent.color = moonColor;
			}
		}

		//Disabled the sun and moon light if they are not on
		if (sun.intensity <= 0.0f)
		{
			sun.enabled = false;
		}

		if (sun.intensity > 0.0f)
		{
			sun.enabled = true;
		}

		if (moonComponent.intensity <= 0.0f)
		{
			moonComponent.enabled = false;
		}

		if (moonComponent.intensity > 0.0f)
		{
			moonComponent.enabled = true;
			moonComponent.color = moonColor;
		}

		if (weatherForecaster == 2 && useRainStreaks)
		{
			if (currentRainFogIntensity <= 0)
			{
				mistFog.gameObject.SetActive(false);
			}
		}

		if (weatherForecaster == 3 && useRainStreaks || weatherForecaster == 12 && useRainStreaks)
		{
			if (currentRainFogIntensity >= 1)
			{
				mistFog.gameObject.SetActive(true);
			}
		}

		if (weatherForecaster == 2 && UseRainMist)
		{
			if (minHeavyRainMistIntensity <= 0)
			{
				rainMist.gameObject.SetActive(false);
			}
		}

		if (weatherForecaster == 3 && UseRainMist || weatherForecaster == 12 && UseRainMist)
		{
			if (minHeavyRainMistIntensity >= 1)
			{
				rainMist.gameObject.SetActive(true);
			}
		}

		//Calculates our time
		hourCounter = (int)Hour;

		stormCounter += Time.deltaTime * .5f;
		
		minuteCounterCalculator = Hour*60f;	
		minuteCounter = (int)minuteCounterCalculator % 60;
		
		//Controls shadows for both day and night light sources
		if (shadowsDuringDay)
		{
			if (dayShadowType == 1)
			{
				sunComponent.shadows = LightShadows.Hard;
			}
			
			if (dayShadowType == 2)
			{
				sunComponent.shadows = LightShadows.Soft;
			}
			
			sunComponent.shadowStrength = dayShadowIntensity;
		}
		
		if (!shadowsDuringDay)
		{
			sunComponent.shadows = LightShadows.None;
		}
		
		if (shadowsDuringNight)
		{
			if (nightShadowType == 1)
			{
				moonComponent.shadows = LightShadows.Hard;
			}
			
			if (nightShadowType == 2)
			{
				moonComponent.shadows = LightShadows.Soft;
			}
			
			moonComponent.shadowStrength = nightShadowIntensity;
		}

		if (!shadowsDuringNight)
		{
			moonComponent.shadows = LightShadows.None;
		}
		
		if (shadowsLightning)
		{
			if (lightningShadowType == 1)
			{
				lightSourceComponent.shadows = LightShadows.Hard;
			}
			
			if (lightningShadowType == 2)
			{
				lightSourceComponent.shadows = LightShadows.Soft;
			}
			
			lightSourceComponent.shadowStrength = lightningShadowIntensity;
		}
		
		if (!shadowsLightning)
		{
			lightSourceComponent.shadows = LightShadows.None;
		}
		
		//Controls wether the weather command prompt is enabled or disabled	
		if (weatherCommandPromptUseable == true)
		{
			if(Input.GetKeyDown(KeyCode.U))
			{
				commandPromptActive = !commandPromptActive;
			}
		} 

		//Toggles the Time Scroll Bar to control the time of day while in-game
		if (timeScrollBarUseable == true)
		{
			if(Input.GetKeyDown(KeyCode.U))
			{
				timeScrollBar = !timeScrollBar;
			}
		} 
		
		if (weatherCommandPromptUseable == false)
		{ 
			commandPromptActive = false;
		}
		
		if (monthCounter == -1)
		{
			monthCounter = 11;
		}

		//Rotates our sun using quaternion rotations so the angles don't coincide (sunAngle angles the sun based off the user's input)	
		sunTrans.transform.eulerAngles = new Vector3(startTime * 360 - 90, sunAngle, 180 + sunAngle);

		if (moonShaftScript != null)
		{
			moonShaftScript.sunShaftIntensity = moon.intensity * 2;
		}

		if (Hour >= 12 && !moonChanger)
		{
			moonPhaseCalculator += 1;
			MoonPhaseCalculator();
			moonChanger = true;
		}

		if (Hour <= 1)
		{
			moonChanger = false;
		}
		
		//Calculates our minutes into hours
		//Any hourly event can be created by watching yhr hourlyUpdate variable
		if(minuteCounter >= 59 && !hourlyUpdate)
		{	
			if (temperatureControlType == 2 && !disableTemperatureGeneration)
			{
				temperature = (int)TemperatureCurve.Evaluate(PreciseCurveTime) + (int)TemperatureFluctuationn.Evaluate((float)hourCounter);
			}

			if (temperatureControlType == 1 && !disableTemperatureGeneration)
			{
				TemperatureGeneration();
			}

			hourlyUpdate = true;
		}

		if (minuteCounter >= 0 && minuteCounter < 10)
		{
			hourlyUpdate = false;
		}

		CalculateMonths();
		CalculateSeaon();

		//Calculates our day length so it stays consistent	
		Hour = startTime * 24;

		//This adds support for night length
		//If timeStopped is checked, time doesn't flow
		//New 2.5 added *0.5f
		if (timeStopped == false)
		{	
			if (Hour >= dayLengthHour && Hour < nightLengthHour) 
			{
				startTime = startTime + Time.deltaTime/dayLength/60 * 0.5f;
			}
			
			if (Hour >= nightLengthHour || Hour < dayLengthHour)
			{
				startTime = startTime + Time.deltaTime/nightLength/60 * 0.5f;
			}
		}

		/*
		if (sunIntensity >= maxSunIntensity)
		{
			sunIntensity = maxSunIntensity;	
		}
		*/

		sunCalculator = Hour / 24;

		//Added 1.8.5
		if (startTime >= 1.0f)
		{
			startTime = 0;
			CalculateDays();
		}
		
		//Forces precipitation if none has happened in an in-game week, prevents drouts
		if (forceStorm >= 7)
		{
			if (staticWeather == false)
			{	
				weatherForecaster = UnityEngine.Random.Range(2,3);
				forceStorm = 0;
			}
		}
		
		//Changes our weather type if there has been precipitation for more than 3 in-game days
		if (changeWeather >= forceWeatherChange && stormControl == true)
		{
			if (staticWeather == false)
			{	
				weatherForecaster = UnityEngine.Random.Range(4,11);
				changeWeather = 0;
			}
		}

		//Controls fading in and out our fog during weather transitions
		if (weatherForecaster == 1 || weatherForecaster == 2 || weatherForecaster == 3 || weatherForecaster == 12 || weatherForecaster == 9)
		{
			//New fade multiplier
			FogFadeInFloat += Time.deltaTime * 0.05f * FogColorFadeInMultiplier; 
			FogFadeOutFloat -= Time.deltaTime * 0.05f * FogColorFadeOutMultiplier; 
			
			if (FogFadeOutFloat <= 0)
			{
				FogFadeOutFloat = 0;
			}
			
			if (FogFadeInFloat >= 1)
			{
				FogFadeInFloat = 1;
				weatherHappened = true;
			}

			fogTwilightColor = Color.Lerp (originalFogColorTwilight, stormyFogColorTwilight, FogFadeInFloat);
			fogMorningColor = Color.Lerp (originalFogColorMorning, stormyFogColorMorning, FogFadeInFloat);
			fogDayColor = Color.Lerp (originalFogColorDay, stormyFogColorDay, FogFadeInFloat);
			fogDuskColor = Color.Lerp (originalFogColorEvening, stormyFogColorEvening, FogFadeInFloat);
			fogNightColor = Color.Lerp (originalFogColorNight, stormyFogColorNight, FogFadeInFloat);
		}
	
		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13)
		{
			FogFadeOutFloat += Time.deltaTime * 0.05f * FogColorFadeInMultiplier; 
			FogFadeInFloat -= Time.deltaTime * 0.05f * FogColorFadeOutMultiplier; 

			fogTwilightColor = Color.Lerp (stormyFogColorTwilight, originalFogColorTwilight, FogFadeOutFloat);
			fogMorningColor = Color.Lerp (stormyFogColorMorning, originalFogColorMorning, FogFadeOutFloat);
			fogDayColor = Color.Lerp (stormyFogColorDay, originalFogColorDay, FogFadeOutFloat);
			fogDuskColor = Color.Lerp (stormyFogColorEvening, originalFogColorEvening, FogFadeOutFloat);
			fogNightColor = Color.Lerp (stormyFogColorNight, originalFogColorNight, FogFadeOutFloat);
			
			if (FogFadeOutFloat >= 1)
			{
				weatherHappened = false;
				FogFadeOutFloat = 1;
			}
			
			if (FogFadeInFloat <= 0)
			{
				FogFadeInFloat = 0;
			}
		}

		//Assigns our generated weather when the hour is reached
		if (WeatherGenerated)
		{
			if (hourCounter == HourToChangeWeather)
			{
				if (WeatherTypeGenerated == "Non-Precipitation")
				{
					weatherForecaster = NextForecast;
					WeatherGenerated = false;
				}

				if (WeatherTypeGenerated == "Precipitation")
				{
					weatherForecaster = NextForecast;
					WeatherGenerated = false;
				}
			}
		}

		TimeOfDayCalculator ();
		DynamicTimeOfDaySounds ();
		CloudController(); 
	}

	//This function is called once per UniStorm day.
	//When called, it uses the Precipitation Odds from the UniStorm Editor to generate the odds for weather. The odds are based on percentage of precipitation.
	//If the generated odds are greater than to the precipitation odds, UniStorm will generate weather based on the precipitation weather types.
	//If the generated odds are less than or equal to the precipitation odds, UniStorm will generate weather based on the non-precipitation weather types.
	//Finally, UniStorm genertates an hour that the weather change will happen. When that hour is reached, the weather will change according to what was generated here.
	public void GenerateWeather ()
	{
		if (!staticWeather)
		{
			generatedOdds = UnityEngine.Random.Range(1, 101);
			
			if (generatedOdds > precipitationOdds)
			{
				HourToChangeWeather = UnityEngine.Random.Range(0,23);
				WeatherTypeGenerated = "Non-Precipitation";
				NextForecast = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
				WeatherGenerated = true;

				//Information for recording weather and receiving the next forecast
				//Debug.Log("Hour weather will change: " + HourToChangeWeather + " --- Next Forecast: "  + NextForecast);
				//Debug.Log("Date: " + UniStormDate.Date + "-----" + " Weather: " + "Clear" + "-----" + " Hour weather change will happen: " + HourToChangeWeather);

			}
			
			if (generatedOdds <= precipitationOdds)
			{
				HourToChangeWeather = UnityEngine.Random.Range(0,23);
				WeatherTypeGenerated = "Precipitation";
				NextForecast = PrecipitationWeatherTypes[UnityEngine.Random.Range(0, PrecipitationWeatherTypes.Length)];
				WeatherGenerated = true;

				//Information for recording weather and receiving the next forecast
				//Debug.Log("Hour weather will change: " + HourToChangeWeather + " --- Next Forecast: "  + NextForecast);
				//Debug.Log("Date: " + UniStormDate.Date.ToString() + "-----" + " Weather: " + "Precipitation" + "-----" + " Hour weather change will happen: " + HourToChangeWeather);
			}
		}
	}

	//Handles all of the lerp and time related changes
	void TimeOfDayCalculator ()
	{
		if (moonShaftScript.sunShaftIntensity <= 0.01f)
		{
			moonShaftScript.enabled = false;
		}

		if (moonShaftScript.sunShaftIntensity > 0.01f)
		{
			moonShaftScript.enabled = true;
		}

		//Calculates Twilight
		if(Hour > 2 && Hour < 4)
		{
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0.5f);
			RenderSettings.ambientLight = Color.Lerp (NightAmbientLight, TwilightAmbientLight, (Hour/2)-1);
			RenderSettings.fogColor = Color.Lerp (fogNightColor, fogTwilightColor, (Hour/2)-1);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, TwilightAmbientIntensity,(Hour/2)-1);
			}
		}

		if (hourCounter >= 5 && hourCounter < 6)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorNight, cloudColorTwilight, Mathf.Lerp(0,1, Hour - 5)));
		}


		if (hourCounter >= 6 && hourCounter < 7)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorTwilight, cloudColorMorning, Mathf.Lerp(0,1, Hour - 6)));
		}

		if (hourCounter >= 7 && hourCounter < 8)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorMorning, cloudColorDay, Mathf.Lerp(0,1, Hour - 7)));
		}
		
		//Calculates morning fading in from night
		if(Hour > 4 && Hour < 6)
		{
			RenderSettings.ambientLight = Color.Lerp (TwilightAmbientLight, MorningAmbientLight, (Hour/2)-2);
			sun.color = Color.Lerp (SunNight, SunMorning, (Hour/2)-2);
			RenderSettings.fogColor = Color.Lerp (fogTwilightColor, fogMorningColor, (Hour/2)-2);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorMorning, (Hour/2)-2));
			float lerp = Mathf.Lerp(0.5f, 0, (Hour/2)-2);
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", lerp);
			starSphereComponent.sharedMaterial.SetColor ("_TintColor", Color.Lerp (starBrightness, moonFadeColor, (Hour/2)-2) );

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, MorningAmbientIntensity,(Hour/2)-2);
			}

			if (sunShaftScript != null)
			{
				sunShaftScript.sunColor = Color.Lerp (DuskAtmosphericLight, MorningAtmosphericLight, (Hour/2)-2);
			}
		}
		
		//Calculates Morning
		if(Hour > 6 && Hour < 8)
		{
			RenderSettings.ambientLight = Color.Lerp (MorningAmbientLight, MiddayAmbientLight, (Hour/2)-3);
			sun.color = Color.Lerp (SunMorning, SunDay, (Hour/2)-3);
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  starBrightness * fadeStars);
			RenderSettings.fogColor = Color.Lerp (fogMorningColor, fogDayColor, (Hour/2)-3);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorMorning, skyColorDay, (Hour/2)-3));
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  Color.black * fadeStars);
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(MorningAmbientIntensity, DayAmbientIntensity,(Hour/2)-3);
			}

			if (sunShaftScript != null)
			{
				sunShaftScript.sunColor = Color.Lerp (MorningAtmosphericLight, MiddayAtmosphericLight, (Hour/2)-3);
			}
	
			fadeStars = 0;
		}
		
		//Calculates Day
		if(Hour > 8 && Hour < 16)
		{
			RenderSettings.ambientLight = Color.Lerp (MiddayAmbientLight, MiddayAmbientLight, (Hour/2)-4);
			sun.color = Color.Lerp (SunDay, SunDay, (Hour/2)-4);
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  starBrightness * fadeStars);
			RenderSettings.fogColor = Color.Lerp (fogDayColor, fogDayColor, (Hour/2)-4);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorDay, skyColorDay, (Hour/2)-4));
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorDay, cloudColorDay, (Hour/2)-4));
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  Color.black * fadeStars);
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(DayAmbientIntensity, DayAmbientIntensity,(Hour/2)-4);
			}

			if (sunShaftScript != null)
			{
				sunShaftScript.sunColor = Color.Lerp (MiddayAtmosphericLight, MiddayAtmosphericLight, (Hour/2)-4);
			}
	
			fadeStars = 0;
		}
		
		//Calculates dusk fading in from day
		if(Hour > 16 && Hour < 18)
		{
			RenderSettings.ambientLight = Color.Lerp (MiddayAmbientLight, DuskAmbientLight, (Hour/2)-8);
			sun.color = Color.Lerp (SunDay, SunDusk, (Hour/2)-8);
			RenderSettings.fogColor = Color.Lerp (fogDayColor, fogDuskColor, (Hour/2)-8);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorDay, skyColorEvening, (Hour/2)-8));
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0);
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  Color.black * fadeStars);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(DayAmbientIntensity, EveningAmbientIntensity,(Hour/2)-8);
			}

			if (sunShaftScript != null)
			{
				sunShaftScript.sunColor = Color.Lerp (MiddayAtmosphericLight, DuskAtmosphericLight, (Hour/2)-8);
			}
			
			fadeStars = 0; 
		}
			
		if (hourCounter >= 16 && hourCounter < 17)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorDay, cloudColorEvening, Mathf.Lerp(0,1, Hour - 16)));
		}


		if (hourCounter >= 17 && hourCounter < 18)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorEvening, cloudColorTwilight, Mathf.Lerp(0,1, Hour - 17)));
		}

		//Calculates night fading in from dusk
		if(Hour > 18 && Hour < 20)
		{
			RenderSettings.ambientLight = Color.Lerp (DuskAmbientLight, TwilightAmbientLight, (Hour/2)-9);
			sun.color = Color.Lerp (SunDusk, SunNight, (Hour/2)-9);
			RenderSettings.fogColor = Color.Lerp (fogDuskColor, fogTwilightColor, (Hour/2)-9);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorEvening, skyColorNight, (Hour/2)-9));
			mostlyClearClouds1Component.material.SetColor("_Color", Color.Lerp (cloudColorTwilight, cloudColorNight, (Hour/2)-9) );
			moonObjectComponent.sharedMaterial.SetFloat("_FloatMax", (Hour/2)-9);
			starSphereComponent.sharedMaterial.SetColor ("_TintColor", Color.Lerp (Color.black, starBrightness, (Hour/2)-9) );
			float lerp = Mathf.Lerp(0f, 0.5f, (Hour/2)-9);
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", lerp);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(EveningAmbientIntensity, NightAmbientIntensity,(Hour/2)-9);
			}
	
			if (sunShaftScript != null)
			{
				sunShaftScript.sunColor = Color.Lerp (DuskAtmosphericLight, DuskAtmosphericLight, (Hour/2)-9);
			}
	
			if (fadeStars >= 1)
			{
				fadeStars = 1;
			}
		}

		if(Hour > 20 && Hour < 22)
		{
			RenderSettings.ambientLight = Color.Lerp (TwilightAmbientLight, NightAmbientLight, (Hour/2)-10f);
			RenderSettings.fogColor = Color.Lerp (fogTwilightColor, fogNightColor, (Hour/2)-10f);

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(TwilightAmbientIntensity, NightAmbientIntensity,(Hour/2)-10);
			}

			fadeStars = 1;
		}

		if (Hour > 22)
		{
			RenderSettings.ambientLight = NightAmbientLight;
			RenderSettings.fogColor = fogNightColor;

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = NightAmbientIntensity;
			}

			fadeStars = 1;
		}
		
		//Calculates Night
		if(Hour > 20)
		{
			sun.color = Color.Lerp (SunNight, SunNight, (Hour/2)-10);	
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  starBrightness * fadeStars);
			mostlyClearClouds1Component.material.SetColor("_Color", cloudColorNight);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorNight, (Hour/2)-10));
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0.5f);
			sun.enabled = false;

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, NightAmbientIntensity,(Hour/2)-10);
			}
	
			fadeStars = 1;
		}
		
		if (Hour >= 0 && Hour <= 4)
		{
			mostlyClearClouds1Component.material.SetColor("_Color", cloudColorNight);
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0.5f);

			fadeStars = 1;
		}
		
		if (Hour >= 0 && Hour <= 2)
		{
			moonObjectComponent.sharedMaterial.SetFloat("_MoonFade", 0.5f);
			RenderSettings.ambientLight = NightAmbientLight;
			RenderSettings.fogColor = fogNightColor;

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = NightAmbientIntensity;
			}

			fadeStars = 1;
		}
		

		if(Hour < 4)
		{
			sun.color = Color.Lerp (SunNight, SunNight, (Hour/2)-2);	
			starSphereComponent.sharedMaterial.SetColor ("_TintColor",  starBrightness * fadeStars);	
			mostlyClearClouds1Component.material.SetColor("_Color", cloudColorNight);
			SkyBoxMaterial.SetColor("_SkyTint", Color.Lerp (skyColorNight, skyColorNight, (Hour/2)-10));
			moonObjectComponent.sharedMaterial.SetFloat("_FogAmountMin", (0));
			moonObjectComponent.sharedMaterial.SetFloat("_FogAmountMax", (50000));

			if (AutoCalculateAmbientIntensity == 2)
			{
				RenderSettings.ambientIntensity = Mathf.Lerp(NightAmbientIntensity, NightAmbientIntensity,(Hour/2)-2);
			}

			fadeStars = 1;	
		}
			
		if (AutoCalculateAmbientIntensity == 1)
		{
			RenderSettings.ambientIntensity = RenderSettings.ambientLight.grayscale * ambientLightMultiplier;
		}
	}


	//Puts all fading in and out weather types into 2 function then picks by weather type to control individual weather factors
	void WeatherForecaster () 
	{	
		//Foggy 
		if (weatherForecaster == 1)
		{
			FadeInPrecipitation();
			weatherString = "Foggy";
		}

		//Light Snow / Light Rain
		if (weatherForecaster == 2)
		{
			FadeInPrecipitation();

			if (temperature >= 33)
			{
				weatherString = "Light Rain";
			}

			if (temperature <= 32)
			{
				weatherString = "Light Snow";
			}
		}

		//Heavy Snow
		if (weatherForecaster == 3)
		{
			FadeInPrecipitation();

			if (temperature >= 33)
			{
				weatherString = "Heavy Rain & Thunder Storm";
			}
			
			if (temperature <= 32)
			{
				weatherString = "Heavy Snow";
			}
		}

		//Partly Cloudy
		if (weatherForecaster == 4)
		{
			FadeOutPrecipitation ();
			weatherString = "Partly Cloudy";
		}

		//Partly Cloudy
		if (weatherForecaster == 5)
		{
			FadeOutPrecipitation ();
			weatherForecaster = 3;
		}
		
		//Partly Cloudy
		if (weatherForecaster == 6)
		{
			FadeOutPrecipitation ();
			weatherForecaster = 7;
		}
		
		//Mostly Clear
		if (weatherForecaster == 7)
		{
			FadeOutPrecipitation();
			weatherString = "Mostly Clear";
		}
		
		//Clear
		if (weatherForecaster == 8)
		{
			FadeOutPrecipitation ();
			weatherString = "Clear";
		}
		
		//Mostly Cloudy
		if (weatherForecaster == 11)
		{
			FadeOutPrecipitation ();
			weatherString = "Mostly Cloudy";
		}
		
		//Cloudy aka Foggy
		if (weatherForecaster == 9)
		{
			weatherForecaster = 1;
		}
		
		//Lighning Bugs (Summer Only)
		if (weatherForecaster == 10 && isSummer)
		{
			FadeOutPrecipitation ();

			weatherString = "Lighning Bugs";
		}	

		if (weatherForecaster == 10 && !isSummer)
		{
			weatherForecaster = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}
		
		//Heavy Rain (No Thunder)
		if (weatherForecaster == 12)
		{
			FadeInPrecipitation ();

			weatherString = "Heavy Rain (No Thunder)";
		}
		
		//Falling Fall Leaves
		if (weatherForecaster == 13 && isFall)
		{
			FadeOutPrecipitation ();
			//Debug.Log("Fall");
			weatherString = "Falling Fall Leaves";
		}

		if (weatherForecaster == 13 && !isFall)
		{
			weatherForecaster = ClearWeatherTypes[UnityEngine.Random.Range(0, ClearWeatherTypes.Length)];
		}
	}

	//Gives our users an in-game GUI to change the weather and time
	void OnGUI () 
	{
		//Allows a scrolling GUI bar that controls the time of day by the user
		if (timeScrollBar == true)
		{
			startTime = GUI.HorizontalSlider(new Rect(25, 25, 200, 30), startTime, 0.0F, 0.99F);	
		}

		//Allows a text GUI box that controls the weather based
		if (commandPromptActive == true)
		{
			stringToEdit = GUI.TextField (new Rect (10, 430, 40, 20), stringToEdit, 10);
			
			if(GUI.Button(new Rect(10, 450, 60, 40), "Change"))
			{
				weatherCommandPrompt ();
			}
		}
	}

	//Calculates our weather command prompts
	void weatherCommandPrompt ()
	{
		if (stringToEdit == foggy)
		{
			weatherForecaster = 1;
			print ("Weather Forced: Foggy");
		}
		
		if (stringToEdit == lightRain_lightSnow)
		{
			weatherForecaster = 2;
			print ("Weather Forced: Light Rain/Light Snow (Winter Only)");
		}
		
		if (stringToEdit == rainStorm_snowStorm)
		{
			weatherForecaster = 3;
			print ("Weather Forced: Tunder Storm/Snow Storm (Winter Only)");
		}
		
		if (stringToEdit == partlyCloudy1)
		{
			weatherForecaster = 4;
			print ("Weather Forced: Partly Cloudy");
		}
		
		if (stringToEdit == partlyCloudy2)
		{
			weatherForecaster = 4;
			print ("Weather Forced: Partly Cloudy");
		}
		
		if (stringToEdit == partlyCloudy3)
		{
			weatherForecaster = 4;
			print ("Weather Forced: Partly Cloudy");
		}
		
		if (stringToEdit == clear1)
		{
			weatherForecaster = 7;
			print ("Weather Forced: Mostly Clear");
		}
		
		if (stringToEdit == clear2)
		{
			weatherForecaster = 8;
			print ("Weather Forced: Sunny");
		}
		
		if (stringToEdit == cloudy)
		{
			weatherForecaster = 7;
			print ("Weather Forced: Sunny");
		}
		
		if (stringToEdit == butterfliesSummer)
		{
			weatherForecaster = 10;
			print ("Weather Forced: Lightning Bugs (Summer Only)");
		}
		
		if (stringToEdit == mostlyCloudy)
		{
			weatherForecaster = 11;
			print ("Weather Forced: Mostly Cloudy");
		}
		
		if (stringToEdit == heavyRain)
		{
			weatherForecaster = 12;
			print ("Weather Forced: Heavy Rain (No Thunder)");
		}
		
		if (stringToEdit == fallLeaves)
		{
			weatherForecaster = 13;
			print ("Weather Forced: Falling Fall Leaves (Fall Only)");
		}
	}	
	
	//Calculates our lightning generation
	void Lightning () 
	{
			LightningTimer += Time.deltaTime;
			
			if (LightningTimer >= lightningOdds && lightingGenerated == false)
			{
				lightingGenerated = true;
				lightSourceComponent.enabled = true;
				
				lightningNumber = UnityEngine.Random.Range(1,6);
				
				if (lightningNumber == 1)
				{
					soundComponent.PlayOneShot(thunderSound1);
				}
				
				if (lightningNumber == 2)
				{
					soundComponent.PlayOneShot(thunderSound2);
				}
				
				if (lightningNumber == 3)
				{
					soundComponent.PlayOneShot(thunderSound3);
				}
				
				if (lightningNumber == 4)
				{
					soundComponent.PlayOneShot(thunderSound4);
				}
				
				if (lightningNumber == 5)
				{
					soundComponent.PlayOneShot(thunderSound5);
				}
				
				Instantiate(lightningBolt1, lightningSpawn.position, lightningSpawn.rotation);
			}
			
			if (lightingGenerated == true)
			{
				if (fadeLightning == false)
				{
					lightSourceComponent.intensity += Time.deltaTime * 10;
				}

				
				if (lightSourceComponent.intensity >= lightningIntensity && fadeLightning == false)
				{
					lightSourceComponent.intensity = lightningIntensity;
					fadeLightning = true;
				}
			}
			
			if (fadeLightning == true)
			{
				
				LightningOnTime += Time.deltaTime;

				if (LightningOnTime >= lightningFlashLength)
				{
					lightSourceComponent.intensity -= Time.deltaTime * 10;
				}
				
				
				if (lightSourceComponent.intensity <= 0)
				{
					lightSourceComponent.intensity = 0;
					fadeLightning = false;
					lightingGenerated = false;
					LightningTimer = 0;
					LightningOnTime = 0;
					lightSourceComponent.enabled = false;
					lightSourceComponent.transform.rotation = Quaternion.Euler (50, UnityEngine.Random.Range(0,360), 0);
					
					lightningOdds = UnityEngine.Random.Range(lightningMinChance, lightningMaxChance);
					lightningIntensity = UnityEngine.Random.Range(minIntensity, maxIntensity);
				}
			}
	}
	
	//This function is called on start to explain potential errors and missing components
	void LogErrorCheck () 
	{
		//Check Null and Log Errors for weather effects
		if (rain == null)
		{
			//Error Log if object is not found unable to find UniStorm Editor
			Debug.LogError("<color=red>Rain System Null Reference:</color> There is no Rain Particle System, make sure there is one assigned to the Rain Particle System slot of the UniStorm Editor. ");
		}
		
		if (snow == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Snow System Null Reference:</color> There is no Snow Particle System, make sure there is one assigned to the Snow Particle System slot of the UniStorm Editor. ");
		}
		
		if (butterflies == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Butterflies System Null Reference:</color> There is no Butterflies Particle System, make sure there is one assigned to the Butterflies Particle System slot of the UniStorm Editor. ");
		}
		
		if (mistFog == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Mist System Null Reference:</color> There is no Mist Particle System, make sure there is one assigned to the Mist Particle System slot of the UniStorm Editor. ");
		}
		
		if (snowMistFog == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Snow Dust System Null Reference:</color> There is no Snow Dust Particle System, make sure there is one assigned to the Snow Dust Particle System slot of the UniStorm Editor. ");
		}
		
		if (windyLeaves == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Windy Leaves System Null Reference:</color> There is no Windy Leaves Particle System, make sure there is one assigned to the Windy Leaves Particle System slot of the UniStorm Editor. ");
		}
		
		if (windZone == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Wind Zone Null Reference:</color> There is no Wind Zone System, make sure there is one assigned to the Wind Zone System slot of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the SkyBox Mateirals that UniStorm uses	
		if (SkyBoxMaterial == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Sky Box Material Null Reference:</color> There is a missing Sky Box Material, make sure there is one assigned to each of the Sky Box Material slots of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Moon Phase Material that UniStorm uses	
		if (moonPhase1 == null || moonPhase2 == null || moonPhase3 == null || moonPhase4 == null || moonPhase5 == null || moonPhase6 == null || moonPhase7 == null || moonPhase8 == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Moon Phase Material Null Reference:</color> There is a missing Moon Phase Material, make sure there is one assigned to each of the Moon Phase Material slots of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Cloud GameObjects that UniStorm uses
		if (mostlyClearClouds1 == null || heavyClouds == null || heavyCloudsLayerLight == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Cloud GameObject Null Reference:</color> There is a missing Cloud GameObject, make sure there is one assigned to each of the Cloud GameObject slots of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Sky Sphere GameObjects that UniStorm uses	
		if (starSphere == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Sky Sphere GameObject Null Reference:</color> There is a missing Sky Sphere GameObject, make sure there is one assigned to both the Star Sphere and Gradient Sphere slots of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Cloud GameObjects that UniStorm uses
		if (moonObject == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Moon GameObject Null Reference:</color> The Moon GameObject is missing, make sure there it is assigned to the Moon GameObject slot of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Sun GameObjects that UniStorm uses
		if (sun == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Sun GameObject Null Reference:</color> The Sun GameObject is missing, make sure it is assigned to the Sun GameObject slot of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Moon GameObjects that UniStorm uses
		if (moon == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Moon GameObject Null Reference:</color> The Moon GameObject is missing, make sure it is assigned to the Moon GameObject slot of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the lightning GameObjects that UniStorm uses
		if (lightningLight == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Lightning Light GameObject Null Reference:</color> The Lightning Light GameObject is missing, make sure it is assigned to the Lightning Light GameObject slot of the UniStorm Editor. ");
		}
		
		//Check Null and Log Errors for the Weather Sound GameObjects that UniStorm uses
		if (rainSound == null || windSound == null || windSnowSound == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Weather Sound Effect Null Reference:</color>  There is a missing Weather Sound Effect, make sure there is one assigned to each of the Weather Sound Effect slots of the UniStorm Editor.");
		}
		
		//Check Null and Log Errors for the thunder Sound GameObjects that UniStorm uses
		if (thunderSound1 == null || thunderSound2 == null || thunderSound3 == null || thunderSound4 == null || thunderSound5 == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Weather Sound Effect Null Reference:</color>  There is a missing Thunder Sound Effect, make sure there is one assigned to each of the Thunder Sound Effect slots of the UniStorm Editor.");
		}
		
		if (lightningBolt1 == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Lightning Bolt Null Reference:</color> The Lightning Bolt is missing, make sure there is one attached to the UniStorm UniStorm Editor.");
		}
		
		if (lightningSpawn == null)
		{
			//Error Log if script is unable to find UniStorm Editor
			Debug.LogError("<color=red>Lightning Bolt Spawn Null Reference:</color> The Lightning Bolt Spawn is missing, make sure there is one attached to the UniStorm UniStorm Editor.");
		}
	}

	//This function handles the Time Manager's sound generation. 
	//The Sound Manager randomly generates sounds based on the time of day and the min and max seconds
	void DynamicTimeOfDaySounds () 
	{
		TODSoundsTimer += Time.deltaTime;

		if (TODSoundsTimer >= timeToWaitCurrent && Hour > 4 && Hour < 8 && playedSound == false && useMorningSounds)
		{
			soundComponent.PlayOneShot(ambientSoundsMorning[UnityEngine.Random.Range(0,ambientSoundsMorning.Count)]);
			playedSound = true;
		}
		
		if (TODSoundsTimer > timeToWaitCurrent && Hour > 8 && Hour < 16 && playedSound == false && useDaySounds)
		{
			soundComponent.PlayOneShot(ambientSoundsDay[UnityEngine.Random.Range(0,ambientSoundsDay.Count)]);
			playedSound = true;
		}
		
		if (TODSoundsTimer > timeToWaitCurrent && Hour > 16 && Hour < 20 && playedSound == false && useEveningSounds)
		{
			soundComponent.PlayOneShot(ambientSoundsEvening[UnityEngine.Random.Range(0,ambientSoundsEvening.Count)]);
			playedSound = true;
		}
		
		if (TODSoundsTimer > timeToWaitCurrent && Hour > 20 && Hour < 25 && playedSound == false && useNightSounds)
		{	
			soundComponent.PlayOneShot(ambientSoundsNight[UnityEngine.Random.Range(0,ambientSoundsNight.Count)]);
			playedSound = true;
		}
		
		if (TODSoundsTimer > timeToWaitCurrent && Hour > 0 && Hour < 4 && playedSound == false && useNightSounds)
		{	
			soundComponent.PlayOneShot(ambientSoundsNight[UnityEngine.Random.Range(0,ambientSoundsNight.Count)]);
			playedSound = true;
		}

		if (TODSoundsTimer >= timeToWaitCurrent+2)
		{
			timeToWaitCurrent = UnityEngine.Random.Range(timeToWaitMin,timeToWaitMax);
			TODSoundsTimer = 0;
			playedSound = false;
		}
		
	}

	//Puts all fading out weather types into one function then picks by weather type to control individual weather factors
	void FadeOutPrecipitation ()
	{
			StormCloudFadeFloat1 -= Time.deltaTime * .04f * CloudFadeOutMultiplier; 
			StormCloudFadeFloat2 -= Time.deltaTime * .06f * CloudFadeOutMultiplier; 
			windControl -= Time.deltaTime;
			sunShaftFade += Time.deltaTime * 10 * EffectsFadeInMultiplier;
			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			currentSnowFogIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;	
			currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			sunIntensity += Time.deltaTime * .14f * EffectsFadeInMultiplier;
			dynamicSnowFade -= Time.deltaTime * .0095f; 
			
			heavyCloudsComponent.material.color = new Color(stormCloudColor1.r,stormCloudColor1.g,stormCloudColor1.b,StormCloudFadeFloat1);
			heavyCloudsLayerLightComponent.material.color = new Color(stormCloudColor2.r,stormCloudColor2.g,stormCloudColor2.b,StormCloudFadeFloat2);

			lightSourceComponent.enabled = false;
			
			//Fade in and out leaves for Fall weather type
			if (weatherForecaster == 13 && isFall)
			{
				windyLeavesIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;

				if (windyLeavesIntensity >= 6)
				{
					windyLeavesIntensity = 6;
				}
			}
			else
			{
				windyLeavesIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
			
				if (windyLeavesIntensity <= 0)
				{
					windyLeavesIntensity = 0;
				}
			}

			//Fade in and out butterflies (lightning bugs) for Summer weather type
			if (weatherForecaster == 10 && isSummer)
			{
				butterfliesFade += Time.deltaTime * ParticleEffectsFadeInMultiplier;

				if (butterfliesFade >= 8)
				{
					butterfliesFade = 8;
				}
			}
			else
			{
				butterfliesFade -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;

				if (butterfliesFade <= 0)
				{
					butterfliesFade = 0;
				}
			}
			
		if (terrainDetection)
		{
			FadeOutWind();
		}
			
			if (RenderSettings.fogMode == FogMode.Linear)
			{
				RenderSettings.fogStartDistance += Time.deltaTime * 0.75f  * FogDistanceFadeOutMultiplier; //Was 0.75f
				RenderSettings.fogEndDistance += Time.deltaTime * 10f * FogDistanceFadeOutMultiplier;
				
				if (RenderSettings.fogStartDistance >= fogStartDistance)
				{
					RenderSettings.fogStartDistance = fogStartDistance;
				}
				
				if (RenderSettings.fogEndDistance >= fogEndDistance)
				{
					RenderSettings.fogEndDistance = fogEndDistance;
				}
			}
			
			if (StormCloudFadeFloat2 <= 0)
			{
				StormCloudFadeFloat2 = 0;
			}

		//This is the new way for particles to be manipulated because the old method has been deprecated
		//In the event of an older version of Unity being used, revert to the old method

		//Lightning Bugs
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule butterfliesEmission = butterflies.emission;
		butterfliesEmission.rate = new ParticleSystem.MinMaxCurve(butterfliesFade);
		#else
		butterflies.emissionRate = butterfliesFade;
		#endif

		//Snow	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule snowEmission = snow.emission;
		snowEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowIntensity);
		#else
		snow.emissionRate = currentSnowIntensity;
		#endif

		//Snow Fog	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule snowFogEmission = snowMistFog.emission;
		snowFogEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowFogIntensity);
		#else
		snowMistFog.emissionRate = currentSnowFogIntensity;
		#endif
			
		//Rain	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule rainEmission = rain.emission;
		rainEmission.rate = new ParticleSystem.MinMaxCurve(currentRainIntensity);
		#else
		rain.emissionRate = currentRainIntensity;
		#endif

		//Rain mist
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule rainMistEmission = rainMist.emission;
		rainMistEmission.rate = new ParticleSystem.MinMaxCurve(minHeavyRainMistIntensity);
		#else
		rainMist.emissionRate = minHeavyRainMistIntensity;
		#endif

		//Windy Leaves
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule windyLeavesEmission = windyLeaves.emission;
		windyLeavesEmission.rate = new ParticleSystem.MinMaxCurve(windyLeavesIntensity);
		#else
		windyLeaves.emissionRate = windyLeavesIntensity;
		#endif
			
		mistFogComponent.minEmission = currentRainFogIntensity;
		mistFogComponent.maxEmission = currentRainFogIntensity;
			
			if (dynamicSnowFade <= .25)
			{
				dynamicSnowFade = .25f;
			}

			//Keep snow from going below 0
			if (currentSnowIntensity <= 0)
			{
				currentSnowIntensity = 0;
			}
			
			//Keep snow fog from going below 0
			if (currentSnowFogIntensity <= 0)
			{
				currentSnowFogIntensity = 0;
			}
			
			rainSoundComponent.volume -= Time.deltaTime * .07f * SoundFadeOutMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			windSnowSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			
			//Calculates our wind making it lessen		
			if (currentRainIntensity <= 0)
			{
				currentRainIntensity = 0;

				//Keeps our wind at 0 if there hasn't been a storm
				windZone.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);

				windZone.gameObject.SetActive(false);
				
				if (windZone.transform.eulerAngles.y <= 1)
				{
					windZone.transform.eulerAngles = new Vector3(0, 0, 0);
					windZone.gameObject.SetActive(false);
				}
				
			}
			
			if (currentRainIntensity >= 1)
			{
				//Makes our wind weaker for when the storm ends
				windZone.transform.Rotate(-Vector3.up * Time.deltaTime * 12);
				
				if (windZone.transform.eulerAngles.y <= 1)
				{
					windZone.transform.eulerAngles = new Vector3(0, 0, 0);
				}
				
			}
			
			//Keeps our fade numbers from going below 0
			if (currentRainFogIntensity <= 0)
			{
				currentRainFogIntensity = 0;
			}
			
			if (minHeavyRainMistIntensity <= 0)
			{
				minHeavyRainMistIntensity = 0;
			}
			
			if (StormCloudFadeFloat1 <= 0)
			{
				StormCloudFadeFloat1 = 0;
			}
		    
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity += Time.deltaTime * 0.15f * EffectsFadeInMultiplier;
			}
			
			if (sunShaftScript != null)
			{
				if (sunShaftScript.sunShaftIntensity >= 2)
				{
					sunShaftScript.sunShaftIntensity = 2;
					RenderSettings.fogDensity += .00012f * Time.deltaTime;

					if (RenderSettings.fogDensity >= fogDensity)
					{
						RenderSettings.fogDensity = fogDensity;
					}
				}
			}

	}

	//Puts all fading in weather types into one function then picks by weather type to control individual weather factors
	void FadeInPrecipitation()
	{
			StormCloudFadeFloat1 += Time.deltaTime * .015f * CloudFadeInMultiplier;
			StormCloudFadeFloat2 += Time.deltaTime * .015f * CloudFadeInMultiplier;
			butterfliesFade -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
			windyLeavesIntensity -= Time.deltaTime * ParticleEffectsFadeOutMultiplier;
			windControlUp += Time.deltaTime;
			sunShaftFade -= Time.deltaTime * 0.14f * EffectsFadeOutMultiplier;
			sunIntensity -= Time.deltaTime * .14f * EffectsFadeOutMultiplier;
			dynamicSnowFade -= Time.deltaTime * .0095f; 
			
			if (weatherForecaster != 3)
			{
				lightSourceComponent.enabled = false;
			}

			//Fix 1.8.5
			if (weatherForecaster == 1)
			{
				currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
				currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
				minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
				currentSnowFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;	
				currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
				
				rainSoundComponent.volume -= Time.deltaTime * .07f  * SoundFadeOutMultiplier;
				windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
				windSnowSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
				
				//Keeps our fade numbers from going below 0
				if (currentRainFogIntensity <= 0)
				{
					currentRainFogIntensity = 0;
				}
				
				if (minHeavyRainMistIntensity <= 0)
				{
					minHeavyRainMistIntensity = 0;
				}

				//Keep snow from going below 0
				if (currentSnowIntensity <= 0)
				{
					currentSnowIntensity = 0;
				}
				
				//Keep snow fog from going below 0
				if (currentSnowFogIntensity <= 0)
				{
					currentSnowFogIntensity = 0;
				}

				//Keep rain fog from going below 0
				if (currentRainIntensity <= 0)
				{
					currentRainIntensity = 0;
				}
			}
		
			//Light Rain
			if (temperature >= 33 && temperatureType == 1 && weatherForecaster == 2 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 2)
			{
				//Fixed 1.8.5
				rainSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			
				if (StormCloudFadeFloat2 >= 0.3f)
				{
					currentRainIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				}

				currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
				
				if (currentRainIntensity >= maxLightRainIntensity)
				{
					currentRainIntensity = maxLightRainIntensity;
				}
				
				if (currentRainFogIntensity <= 0)
				{
					currentRainFogIntensity = 0;
				}
				
				if (minHeavyRainMistIntensity <= 0)
				{
					minHeavyRainMistIntensity = 0;
				}
				
				//This keeps the sound levels low because this is just a little rain	
				if (windSoundComponent.volume >= .0)
				{
					windSoundComponent.volume = .0f;
				}
				
				if (rainSoundComponent.volume >= .3)
				{
					rainSoundComponent.volume = .3f;
				}

				//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
				//Light Rain
				if (lastWeatherType != weatherForecaster && randomizedPrecipitation)
				{
					randomizedRainIntensity = UnityEngine.Random.Range(100,maxLightRainIntensity);
					currentGeneratedIntensity = randomizedRainIntensity;
					lastWeatherType = weatherForecaster;
				}

				if (!randomizedPrecipitation)
				{
					if (currentRainIntensity >= maxLightRainIntensity)
					{
						currentRainIntensity = maxLightRainIntensity;
					}
				}
				
				if (randomizedPrecipitation)
				{
					if (currentRainIntensity >= currentGeneratedIntensity)
					{
						currentRainIntensity = currentGeneratedIntensity;
					}
				}
			}

			//Thunder Storm or Heavy Rain (No Thunder)
			if (temperature >= 33 && temperatureType == 1  && weatherForecaster == 3 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 3 || temperature >= 33 && temperatureType == 1  && weatherForecaster == 12 || temperatureType == 2 && temperature >= 1 && weatherForecaster == 12)
			{
				if (StormCloudFadeFloat2 >= 0.3f)
				{
					currentRainIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
					currentRainFogIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;

					if (currentRainIntensity > 400)
					{
						minHeavyRainMistIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;
					}
				}

				currentSnowFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;	
				currentSnowIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;

				rainSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
				windSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;

				if (weatherForecaster == 3 && currentRainIntensity >= 150)
				{
					Lightning ();
				}

				if (!randomizedPrecipitation)
				{
					//Gradually fades our rain effects in
					if (currentRainIntensity >= maxStormRainIntensity)
					{
						currentRainIntensity = maxStormRainIntensity;
					}
				}
				
				if (randomizedPrecipitation)
				{
					if (currentRainIntensity >= currentGeneratedIntensity)
					{
						currentRainIntensity = currentGeneratedIntensity;
					}
				}

				//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
				//Heavy Rain
				if (lastWeatherType != weatherForecaster && randomizedPrecipitation)
				{
					randomizedRainIntensity = UnityEngine.Random.Range(1000,maxStormRainIntensity);
					currentGeneratedIntensity = randomizedRainIntensity;
					lastWeatherType = weatherForecaster;
				}
				
				//Gradually fades our rain effects in
				if (currentRainIntensity >= maxStormRainIntensity)
				{
					currentRainIntensity = maxStormRainIntensity;
				}
				
				if (currentRainFogIntensity >= maxStormMistCloudsIntensity)
				{
					currentRainFogIntensity = maxStormMistCloudsIntensity;
				}
				
				if (minHeavyRainMistIntensity >= maxHeavyRainMistIntensity)
				{
					minHeavyRainMistIntensity = maxHeavyRainMistIntensity;
				}

				//Keep snow from going below 0
				if (currentSnowIntensity <= 0)
				{
					currentSnowIntensity = 0;
				}
				
				//Keep snow fog from going below 0
				if (currentSnowFogIntensity <= 0)
				{
					currentSnowFogIntensity = 0;
				}
			}

		//Snow Storm
		if (temperature <= 32 && temperatureType == 1  && weatherForecaster == 3 || temperatureType == 2 && temperature <= 0  && weatherForecaster == 3 || temperature <= 32 && temperatureType == 1  && weatherForecaster == 12)
		{
			if (StormCloudFadeFloat2 >= 0.3f)
			{
				currentSnowIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				currentSnowFogIntensity += Time.deltaTime * ParticleEffectsFadeInMultiplier;
			}

			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;

			rainSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			windSnowSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;


			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Light Snow
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation)
			{
				randomizedRainIntensity = UnityEngine.Random.Range(100,maxSnowStormIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}

			if (!randomizedPrecipitation)
			{
				//Keeps our snow level maxed at users input
				if (currentSnowIntensity >= maxSnowStormIntensity)
				{
					currentSnowIntensity = maxSnowStormIntensity;
				}
			}

			if (randomizedPrecipitation)
			{
				if (currentSnowIntensity >= currentGeneratedIntensity)
				{
					currentSnowIntensity = currentGeneratedIntensity;
				}
			}
			
			//Keeps our snow level maxed at users input
			if (currentSnowIntensity >= maxSnowStormIntensity)
			{
				currentSnowIntensity = maxSnowStormIntensity;
			}
			
			//Keeps our snow fog level maxed at users input
			if (currentSnowFogIntensity >= maxHeavySnowDustIntensity)
			{
				currentSnowFogIntensity = maxHeavySnowDustIntensity;
			}
			
			//Keeps our fade numbers from going below 0
			if (currentRainIntensity <= 0)
			{
				currentRainIntensity = 0;
			}

			if (currentRainFogIntensity <= 0)
			{
				currentRainFogIntensity = 0;
			}
			
			if (minHeavyRainMistIntensity <= 0)
			{
				minHeavyRainMistIntensity = 0;
			}
		}

		//Light Snow
		if (temperature <= 32 && temperatureType == 1  && weatherForecaster == 2 || temperatureType == 2 && temperature <= 0  && weatherForecaster == 2)
		{
			if (StormCloudFadeFloat2 >= 0.3f)
			{
				currentSnowIntensity += Time.deltaTime * 10 * ParticleEffectsFadeInMultiplier;
				currentSnowFogIntensity += .008f * ParticleEffectsFadeInMultiplier;	
			}

			currentRainIntensity -= Time.deltaTime * 100 * ParticleEffectsFadeOutMultiplier;
			currentRainFogIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;
			minHeavyRainMistIntensity -= Time.deltaTime * 5 * ParticleEffectsFadeOutMultiplier;

			windSnowSoundComponent.volume += Time.deltaTime * .01f * SoundFadeInMultiplier;
			windSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;
			rainSoundComponent.volume -= Time.deltaTime * .04f * SoundFadeOutMultiplier;

			//If generated precipitation is eqaul to last roll, regenerate intensity (If randomized rain is true)
			//Light Snow
			if (lastWeatherType != weatherForecaster && randomizedPrecipitation)
			{
				randomizedRainIntensity = UnityEngine.Random.Range(100,maxLightSnowIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				lastWeatherType = weatherForecaster;
			}

			if (!randomizedPrecipitation)
			{
				//Keeps our snow level maxed at users input
				if (currentSnowIntensity >= maxLightSnowIntensity)
				{
					currentSnowIntensity = maxLightSnowIntensity;
				}
			}

			if (randomizedPrecipitation)
			{
				if (currentSnowIntensity >= currentGeneratedIntensity)
				{
					currentSnowIntensity = currentGeneratedIntensity;
				}
			}

			//Keeps our snow level maxed at users input
			if (currentSnowIntensity >= maxLightSnowIntensity)
			{
				currentSnowIntensity = maxLightSnowIntensity;
			}
			
			//Keeps our snow fog level maxed at users input
			if (currentSnowFogIntensity >= maxLightSnowDustIntensity)
			{
				currentSnowFogIntensity = maxLightSnowDustIntensity;
			}

			//Keeps our fade numbers from going below 0
			if (currentRainIntensity <= 0)
			{
				currentRainIntensity = 0;
			}
			
			if (currentRainFogIntensity <= 0)
			{
				currentRainFogIntensity = 0;
			}
			
			if (minHeavyRainMistIntensity <= 0)
			{
				minHeavyRainMistIntensity = 0;
			}

			if (windSnowSoundComponent.volume >= .3)
			{
				windSnowSoundComponent.volume = .3f;
			}
		}
			
		if (terrainDetection)
		{
			//Fades in our Dynamic Wind, but only if a terrain is present
			if (weatherForecaster == 3 || weatherForecaster == 12)
			{
				FadeInWind();
			}

			//Fades out our Dynamic Wind
			if (weatherForecaster == 1 || weatherForecaster == 2)
			{
				FadeOutWind();
			}
		}
			
			if (sunIntensity <= StormySunIntensity)
			{
				sunIntensity = StormySunIntensity;
			}

			if (sunIntensity <= StormySunIntensity)
			{
				sunIntensity = StormySunIntensity;
			}
			
			if (RenderSettings.fogMode == FogMode.Linear)
			{
				RenderSettings.fogStartDistance -= Time.deltaTime * 0.75f * FogDistanceFadeInMultiplier;
				RenderSettings.fogEndDistance -= Time.deltaTime * 10f * FogDistanceFadeInMultiplier;
				
				if (RenderSettings.fogStartDistance <= stormyFogDistanceStart)
				{
					RenderSettings.fogStartDistance = stormyFogDistanceStart;
				}
				
				if (RenderSettings.fogEndDistance <= stormyFogDistance)
				{
					RenderSettings.fogEndDistance = stormyFogDistance;
				}
			}

			//Slowly increases the wind to make it stronger for the storm
			if (currentRainIntensity >= 1)
			{
				//Makes our wind stronger for the storm
				windZone.transform.Rotate(Vector3.up * Time.deltaTime * 3);	
				windZone.gameObject.SetActive(true);
			}
			
			if (windZone.transform.eulerAngles.y >= 180)
			{
				windZone.transform.eulerAngles = new Vector3(0, 180, 0);
			}
			
			
			if (dynamicSnowFade <= .25)
			{
				dynamicSnowFade = .25f;
			}

			//Fades in storm clouds
			heavyCloudsComponent.material.color = new Color(stormCloudColor1.r,stormCloudColor1.g,stormCloudColor1.b,StormCloudFadeFloat1);
			heavyCloudsLayerLightComponent.material.color = new Color(stormCloudColor2.r,stormCloudColor2.g,stormCloudColor2.b,StormCloudFadeFloat2);
			
			if (StormCloudFadeFloat2 >= .75)
			{
				StormCloudFadeFloat2 = .75f;
			}
			
			if (butterfliesFade <= 0)
			{
				butterfliesFade = 0;
			}

		//This is the new way for particles to be manipulated because the old method has been deprecated
		//In the event of an older version of Unity being used, revert to the old method

		//Lightning Bugs
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule butterfliesEmission = butterflies.emission;
		butterfliesEmission.rate = new ParticleSystem.MinMaxCurve(butterfliesFade);
		#else
		butterflies.emissionRate = butterfliesFade;
		#endif

		//Snow	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule snowEmission = snow.emission;
		snowEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowIntensity);
		#else
		snow.emissionRate = currentSnowIntensity;
		#endif

		//Snow Fog	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule snowFogEmission = snowMistFog.emission;
		snowFogEmission.rate = new ParticleSystem.MinMaxCurve(currentSnowFogIntensity);
		#else
		snowMistFog.emissionRate = currentSnowFogIntensity;
		#endif

		//Rain	
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule rainEmission = rain.emission;
		rainEmission.rate = new ParticleSystem.MinMaxCurve(currentRainIntensity);
		#else
		rain.emissionRate = currentRainIntensity;
		#endif

		//Rain mist
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule rainMistEmission = rainMist.emission;
		rainMistEmission.rate = new ParticleSystem.MinMaxCurve(minHeavyRainMistIntensity);
		#else
		rainMist.emissionRate = minHeavyRainMistIntensity;
		#endif

		//Windy Leaves
		#if UNITY_5_3 || UNITY_5_4
		ParticleSystem.EmissionModule windyLeavesEmission = windyLeaves.emission;
		windyLeavesEmission.rate = new ParticleSystem.MinMaxCurve(windyLeavesIntensity);
		#else
		windyLeaves.emissionRate = windyLeavesIntensity;
		#endif

		mistFogComponent.minEmission = currentRainFogIntensity;
		mistFogComponent.maxEmission = currentRainFogIntensity;
			
			//Fades our fog in	
			RenderSettings.fogDensity -= .00012f * Time.deltaTime;
			
			if (RenderSettings.fogDensity <= .0006)
			{
				RenderSettings.fogDensity = .0006f;
			}
			
			if (StormCloudFadeFloat2 >= .75)
			{
				StormCloudFadeFloat2 = .75f;
			}
			
			if (StormCloudFadeFloat1 >= 1)
			{
				StormCloudFadeFloat1 = 1;
			}
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity -= Time.deltaTime * 0.03f * EffectsFadeOutMultiplier;
				
				if (sunShaftScript.sunShaftIntensity <= 0.025f)
				{
					sunShaftScript.sunShaftIntensity = 0;
				}
			}
	}

	void CalculateSeaon ()
	{
		//Calculates our seasons
		if (monthCounter >= 3 && monthCounter <= 5)
		{
			isSpring = true;
			isSummer = false;
			isFall = false;
			isWinter = false;
			WeatherForecaster ();
		}

		//Calculates our seasons
		if (monthCounter >= 6 && monthCounter <= 8)
		{
			isSummer = true;
			isSpring = false;
			isFall = false;
			isWinter = false;
			WeatherForecaster ();
		}

		//Calculates our seasons
		if (monthCounter >= 9 && monthCounter <= 11)
		{
			isSummer = false;
			isSpring = false;
			isFall = true;
			isWinter = false; 
			WeatherForecaster ();
		}

		//Calculates our seasons
		if (monthCounter == 12 || monthCounter == 1 || monthCounter == 2)
		{
			isSummer = false;
			isSpring = false;
			isFall = false;
			isWinter = true;
			WeatherForecaster ();
		}
	}

	//Calculates our months for an accurate calendar that also calculates leap year
	public void CalculateMonths ()
	{
		if (calendarType == 1)
		{				
			//Calculates our days into months
			if(dayCounter >= 32 && monthCounter == 1 || dayCounter >= 32 && monthCounter == 3 || dayCounter >= 32 && monthCounter == 5 || dayCounter >= 32 && monthCounter == 7 || dayCounter >= 32 && monthCounter == 8 || dayCounter >= 32 && monthCounter == 10 || dayCounter >= 32 && monthCounter == 12)
			{
				dayCounter = dayCounter % 32;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			if(dayCounter >= 31 && monthCounter == 4 || dayCounter >= 31 && monthCounter == 6 || dayCounter >= 31 && monthCounter == 9 || dayCounter >= 31 && monthCounter == 11)
			{
				dayCounter = dayCounter % 31;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates Leap Year
			if(dayCounter >= 30 && monthCounter == 2 && (yearCounter % 4 == 0 && yearCounter % 100 != 0) || (yearCounter % 400 == 0))
			{
				dayCounter = dayCounter % 30;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			else if (dayCounter >= 29 && monthCounter == 2 && yearCounter % 4 != 0)
			{
				dayCounter = dayCounter % 29;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates our months into years
			if (monthCounter > 12)
			{
				monthCounter = monthCounter % 13;
				yearCounter += 1;
				monthCounter += 1;

				//Reset our roundingCorrection variable to 0
				roundingCorrection = 0;
			}
		}

		//A custom calendar option for creating a custom calendar based off of the user's values
		if (calendarType == 2)
		{
			//Calculates our custom days into months
			if(dayCounter > numberOfDaysInMonth)
			{
				dayCounter = dayCounter % numberOfDaysInMonth - 1;
				dayCounter += 1;
				monthCounter += 1;
			}
			
			//Calculates our custom months into years
			if (monthCounter > numberOfMonthsInYear)
			{
				monthCounter = monthCounter % numberOfMonthsInYear - 1;
				yearCounter += 1;
				monthCounter += 1;
			}
		}
	}

	//Calculates our days and updates our Animation curves.
	public void CalculateDays()
	{	
		roundingCorrection += 0.00273972602f;
		PreciseCurveTime = ((UniStormDate.DayOfYear / 28.07692307692308f) + 1) - roundingCorrection;
		PreciseCurveTime = Mathf.Round(PreciseCurveTime * 10f) / 10f;
		
		CurrentPrecipitationAmountFloat = PrecipitationGraph.Evaluate(PreciseCurveTime);
		TemperatureCurve.Evaluate(PreciseCurveTime);
		
		CurrentPrecipitationAmountInt = (int)Mathf.Round(CurrentPrecipitationAmountFloat);

		sunCalculator = 0;
		
		Hour = 0;

		dayCounter += 1;

		if (weatherForecaster == 3 || weatherForecaster == 2 || weatherForecaster == 12) 
		{
			changeWeather += 1; 
		}

		CalculateMonths();
		
		if (calendarType == 1)
		{
			UniStormDate = new DateTime(yearCounter, monthCounter, dayCounter);
		}
			
		if (PrecipitationType == 2)
		{
			precipitationOdds = CurrentPrecipitationAmountInt;
			GenerateWeather();
		}

		if (PrecipitationType == 1)
		{
			//Roll for weather based on the odds for the season
			if (isSpring == true)
			{	
				precipitationOdds = precipitationOddsSpring;
				GenerateWeather();
			}
			
			//Summer Weather Odds
			if (isSummer == true)
			{	
				precipitationOdds = precipitationOddsSummer;
				GenerateWeather();
			}
			
			//Fall Weather Odds
			if (isFall == true)
			{	
				precipitationOdds = precipitationOddsFall;
				GenerateWeather();
			}
			
			//Winter Weather Odds
			if (isWinter == true)
			{	
				precipitationOdds = precipitationOddsWinter;
				GenerateWeather();
			}
		}
	}

	//This function can be called to load user's saved time or to change the time from an external script
	public void LoadTime ()
	{
		float startTimeMinuteFloat = (int)startTimeMinute;
		startTime = startTimeHour / 24 + startTimeMinuteFloat / 1440;
	}

	//Instant Weather can be called if you want the weather to change instantly. This can be done for quests, loading a player's game, events, etc. 
	//It will set all needed values to be fully faded in. This also goes for the starting weather, if desired.
	public void InstantWeather ()
	{
		if(weatherForecaster == 1)
		{
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 0;
			}
			
			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 2 && temperature > 32)
		{
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = maxLightRainIntensity;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0.3f;
			windSoundComponent.volume = 0.3f;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 2 && temperature <= 32)
		{
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = maxLightSnowIntensity;
			currentSnowFogIntensity = maxLightSnowDustIntensity;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = .3f;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 3 && temperature > 32 || weatherForecaster == 12 && temperature > 32)
		{
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			minHeavyRainMistIntensity = maxHeavyRainMistIntensity;
			currentRainFogIntensity = maxStormMistCloudsIntensity;
			rainSoundComponent.volume = 1.0f;
			windSoundComponent.volume = 1.0f;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (randomizedPrecipitation)
			{
				randomizedRainIntensity = UnityEngine.Random.Range(100,maxStormRainIntensity);
				currentGeneratedIntensity = randomizedRainIntensity;
				currentRainIntensity = randomizedRainIntensity;
			}
			else
			{
				currentRainIntensity = maxStormRainIntensity;
			}

			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 0;
			}

			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = stormGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = stormGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = stormGrassWavingAmount;
			}
		}

		//Instant Snow
		if(weatherForecaster == 3 && temperature <= 32 || weatherForecaster == 12 && temperature <= 32)
		{
			StormCloudFadeFloat1 = 1;
			StormCloudFadeFloat2 = 0.75f;
			currentRainIntensity = 0;
			currentRainFogIntensity = 0;
			currentGeneratedIntensity = 1000;
			currentSnowIntensity = 1000;
			currentSnowFogIntensity = 1000;
			rainSoundComponent.volume = 0.0f;
			windSoundComponent.volume = 0.0f;
			windSnowSoundComponent.volume = 1.0f;
			sunShaftFade = 0;
			sunIntensity = StormySunIntensity;
			RenderSettings.fogEndDistance = stormyFogDistance;
			RenderSettings.fogStartDistance = stormyFogDistanceStart;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 0;
			}
			
			FogFadeOutFloat = 0;
			FogFadeInFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = stormGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = stormGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = stormGrassWavingAmount;
			}
		}

		if(weatherForecaster == 4)
		{
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 7 || weatherForecaster == 8)
		{
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;

			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;

			if (weatherForecaster == 7)
			{
				partlyCloudyFloat = 0;
				mostlyClearFloat = 1;
				mostlyCloudyFloat = 0;
			}

			if (weatherForecaster == 9)
			{
				partlyCloudyFloat = 0;
				mostlyClearFloat = 0;
				mostlyCloudyFloat = 0;
			}

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 11)
		{
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 2;
			}

			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			mostlyClearFloat = 1;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 1;

			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 10 && isSummer)
		{
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 8;
			windyLeavesIntensity = 0;
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 2;
			}
			
			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;
			
			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if(weatherForecaster == 13 && isFall)
		{
			StormCloudFadeFloat1 = 0;
			StormCloudFadeFloat2 = 0;
			currentRainIntensity = 0;
			minHeavyRainMistIntensity = 0;
			currentRainFogIntensity = 0;
			currentSnowIntensity = 0;
			currentSnowFogIntensity = 0;
			rainSoundComponent.volume = 0;
			windSoundComponent.volume = 0;
			windSnowSoundComponent.volume = 0;
			sunShaftFade = 0;
			sunIntensity = maxSunIntensity;
			RenderSettings.fogEndDistance = fogEndDistance;
			RenderSettings.fogStartDistance = fogStartDistance;
			butterfliesFade = 0;
			windyLeavesIntensity = 6;
			
			if (sunShaftScript != null)
			{
				sunShaftScript.sunShaftIntensity = 2;
			}
			
			FogFadeOutFloat = 1;
			FogFadeInFloat = 0;
			partlyCloudyFloat = 1;
			mostlyCloudyFloat = 0;
			mostlyClearFloat = 1;
			
			if (terrainDetection)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
		}

		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6 || weatherForecaster == 7 || weatherForecaster == 8 || weatherForecaster == 11 || weatherForecaster == 10 || weatherForecaster == 13)
		{
			sun.intensity = sunIntensityCurve.Evaluate((float)Hour);
			tempSunIntensity = 1;

			moon.intensity = moonIntensityCurve.Evaluate((float)Hour);
			tempMoonIntensity = 1;
		}
	}

	//This function generates tempertures for the Simple option within the UniStorm Editor Temperature Options
	void TemperatureGeneration ()
	{
		if (hourCounter >= 17)
		{
			temperature -= UnityEngine.Random.Range (1,3);
		}

		if (hourCounter >= 0 && hourCounter <= 5)
		{
			temperature -= UnityEngine.Random.Range (1,3);
		}

		if (hourCounter > 5 && hourCounter <= 16)
		{
			temperature += UnityEngine.Random.Range (1,3);
		}

		if (isSpring)
		{
			summerTemp = 0;
			winterTemp = 0;
			fallTemp = 0;
			
			if (springTemp == 0)
			{
				springTemp = 1;
			}

			if (springTemp == 1)
			{
				temperature = startingSpringTemp;
				springTemp = 2;	
			}

			if (temperature <= minSpringTemp && springTemp == 2)
			{
				temperature = minSpringTemp;
			}

			if (temperature >= maxSpringTemp && springTemp == 2)
			{
				temperature = maxSpringTemp;
			}
		}
		
		if (isSummer)
		{
			winterTemp = 0;
			fallTemp = 0;
			springTemp = 0;
			
			if (summerTemp == 0)
			{
				summerTemp = 1;
			}

			if (summerTemp == 1)
			{
				temperature = startingSummerTemp;
				summerTemp = 2;	
			}

			if (temperature <= minSummerTemp && summerTemp == 2)
			{
				temperature = minSummerTemp;
			}

			if (temperature >= maxSummerTemp && summerTemp == 2)
			{
				temperature = maxSummerTemp;
			}
		}
		
		if (isFall)
		{
			summerTemp = 0;
			winterTemp = 0;
			springTemp = 0;
			
			if (fallTemp == 0)
			{
				fallTemp = 1;
			}

			if (fallTemp == 1)
			{
				temperature = startingFallTemp;
				fallTemp = 2;
			}

			if (temperature <= minFallTemp && fallTemp == 2)
			{
				temperature = minFallTemp;
			}

			if (temperature >= maxFallTemp && fallTemp == 2)
			{
				temperature = maxFallTemp;
			}
		}
		
		if (isWinter)
		{
			summerTemp = 0;
			fallTemp = 0;
			springTemp = 0;
			
			if (winterTemp == 0)
			{
				winterTemp = 1;
			}

			if (winterTemp == 1)
			{
				temperature = startingWinterTemp;
				winterTemp = 2;
			}

			if (temperature <= minWinterTemp && winterTemp == 2)
			{
				temperature = minWinterTemp;
			}

			if (temperature >= maxWinterTemp && winterTemp == 2)
			{
				temperature = maxWinterTemp;
			}
		}
	}

	//Calculates our moon phases. This is updated daily at exactly 12:00.
	void MoonPhaseCalculator ()
	{
		if (moonPhaseCalculator == 1)
		{
			moonObjectComponent.sharedMaterial = moonPhase1;	
		}
		
		if (moonPhaseCalculator == 2)
		{
			moonObjectComponent.sharedMaterial = moonPhase2;	
		}
		
		if (moonPhaseCalculator == 3)
		{
			moonObjectComponent.sharedMaterial = moonPhase3;	
		}
		
		if (moonPhaseCalculator == 4)
		{
			moonObjectComponent.sharedMaterial = moonPhase4;	
		}
		
		if (moonPhaseCalculator == 5)
		{
			moonObjectComponent.sharedMaterial = moonPhase5;	
		}
		
		if (moonPhaseCalculator == 6)
		{
			moonObjectComponent.sharedMaterial = moonPhase6;	
		}
		
		if (moonPhaseCalculator == 7)
		{
			moonObjectComponent.sharedMaterial = moonPhase7;	
		}
		
		if (moonPhaseCalculator == 8)
		{
			moonObjectComponent.sharedMaterial = moonPhase8;	
		}
		
		if (moonPhaseCalculator == 9)
		{
			moonPhaseCalculator = 1;	
		}
	}

	//Fades in our terrain wind during stormy weather types.
	void FadeInWind ()
	{
		Terrain.activeTerrain.terrainData.wavingGrassSpeed += Time.deltaTime * 0.001f * WindFadeInMultiplier;
		Terrain.activeTerrain.terrainData.wavingGrassAmount += Time.deltaTime * 0.001f * WindFadeInMultiplier;
		Terrain.activeTerrain.terrainData.wavingGrassStrength += Time.deltaTime * 0.001f * WindFadeInMultiplier;

			if (Terrain.activeTerrain.terrainData.wavingGrassSpeed >= stormGrassWavingSpeed)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = stormGrassWavingSpeed;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassAmount >= stormGrassWavingStrength)
			{
				Terrain.activeTerrain.terrainData.wavingGrassAmount = stormGrassWavingStrength;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassStrength >= stormGrassWavingAmount)
			{
				Terrain.activeTerrain.terrainData.wavingGrassStrength = stormGrassWavingAmount;
			}
	}

	//Fades out our terrain wind to the originally set values during non-stormy weather types.
	void FadeOutWind ()
	{
		Terrain.activeTerrain.terrainData.wavingGrassSpeed -= Time.deltaTime * 0.001f * WindFadeOutMultiplier;
		Terrain.activeTerrain.terrainData.wavingGrassAmount -= Time.deltaTime *  0.001f * WindFadeOutMultiplier;
		Terrain.activeTerrain.terrainData.wavingGrassStrength -= Time.deltaTime *  0.001f * WindFadeOutMultiplier;

			if (Terrain.activeTerrain.terrainData.wavingGrassSpeed <= normalGrassWavingSpeed)
			{
				Terrain.activeTerrain.terrainData.wavingGrassSpeed = normalGrassWavingSpeed;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassAmount <= normalGrassWavingStrength)
			{
				Terrain.activeTerrain.terrainData.wavingGrassAmount = normalGrassWavingStrength;
			}

			if (Terrain.activeTerrain.terrainData.wavingGrassStrength <= normalGrassWavingAmount)
			{
				Terrain.activeTerrain.terrainData.wavingGrassStrength = normalGrassWavingAmount;
			}
	}

	//The 3 functions below generate new dynamic cloud paterns each time a new weather type is generated with increased cloud cover
	void GenerateMostlyClear ()
	{
		float MostlyClearGenerator = UnityEngine.Random.Range(3.0f,7.0f);
		MostlyClearGenerator = Mathf.Round(MostlyClearGenerator * 10f) / 10f;

		mostlyClearClouds1Component.material.SetTextureScale(CloudA, new Vector2(MostlyClearGenerator, MostlyClearGenerator - 1));
		mostlyClearClouds1Component.material.SetTextureScale(CloudB, new Vector2(MostlyClearGenerator, MostlyClearGenerator - 2));
		mostlyClearClouds1Component.material.SetTextureScale(CloudC, new Vector2(MostlyClearGenerator, MostlyClearGenerator - 1));
	}

	void GeneratePartlyCloudy ()
	{
		float PartlyCloudyGenerator = UnityEngine.Random.Range(3.0f,7.0f);
		PartlyCloudyGenerator = Mathf.Round(PartlyCloudyGenerator * 10f) / 10f;

		partlyCloudyClouds1Component.material.SetTextureScale(CloudA, new Vector2(PartlyCloudyGenerator - 1, PartlyCloudyGenerator));
		partlyCloudyClouds1Component.material.SetTextureScale(CloudB, new Vector2(PartlyCloudyGenerator, PartlyCloudyGenerator - 1));
		partlyCloudyClouds1Component.material.SetTextureScale(CloudC, new Vector2(PartlyCloudyGenerator - 1, PartlyCloudyGenerator));
	}

	void GenerateMostlyCloudy ()
	{
		float MostlyCloudyGenerator = UnityEngine.Random.Range(6.0f,8.0f);
		MostlyCloudyGenerator = Mathf.Round(MostlyCloudyGenerator * 10f) / 10f;

		mostlyCloudyClouds1Component.material.SetTextureScale(CloudA, new Vector2(MostlyCloudyGenerator - 2, MostlyCloudyGenerator));
		mostlyCloudyClouds1Component.material.SetTextureScale(CloudB, new Vector2(MostlyCloudyGenerator, MostlyCloudyGenerator - 2));
		mostlyCloudyClouds1Component.material.SetTextureScale(CloudC, new Vector2(MostlyCloudyGenerator - 2, MostlyCloudyGenerator));
	}

	//This function controls all regular cloud, stormy cloud, and star movement
	void CloudController ()
	{
		float heavCloudScrollSpeedCalculator = Time.deltaTime *  heavyCloudSpeed * 0.1f;
		float heavyCloudScrollSpeed = Time.deltaTime * heavCloudScrollSpeedCalculator;
		float starScrollSpeed = Time.deltaTime * starSpeed * 0.015f; 
		float starRotationSpeedCalc = Time.time * starRotationSpeed * 0.1f;

		uvOffsetA += (uvAnimationRateA * Time.deltaTime * cloudSpeed * 0.1f);
		uvOffsetB += (uvAnimationRateB * Time.deltaTime * cloudSpeed * 0.1f);
		uvOffsetC += (uvAnimationRateB * Time.deltaTime * cloudSpeed * 0.1f);

		uvOffsetStars += uvAnimationRateStars * starScrollSpeed;

		mostlyClearClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudA, uvOffsetB );
		mostlyClearClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudB, uvOffsetB );
		mostlyClearClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudC, uvOffsetC );
		partlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudA, uvOffsetB );
		partlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudB, uvOffsetB );
		partlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudC, uvOffsetB );
		mostlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudA, uvOffsetB );
		mostlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudB, uvOffsetB );
		mostlyCloudyClouds1Component.materials[ materialIndex ].SetTextureOffset( CloudC, uvOffsetB );

		uvOffsetHeavyA += (uvAnimationRateHeavyA * Time.deltaTime * heavyCloudSpeed * 0.1f);
		uvOffsetHeavyB += (uvAnimationRateHeavyB * Time.deltaTime * heavyCloudSpeed * 0.1f);
		uvOffsetHeavyC += (uvAnimationRateHeavyB * Time.deltaTime * heavyCloudSpeed * 0.1f);

		heavyCloudsLayerLightComponent.materials[ materialIndex ].SetTextureOffset( CloudA, uvOffsetHeavyA );
		heavyCloudsLayerLightComponent.materials[ materialIndex ].SetTextureOffset( CloudB, uvOffsetHeavyB );
		heavyCloudsLayerLightComponent.materials[ materialIndex ].SetTextureOffset( CloudC, uvOffsetHeavyC );

		starSphereComponent.materials[ materialIndex ].SetTextureOffset("_StarTex1", uvOffsetStars);
		starSphereComponent.materials[ materialIndex ].SetTextureOffset("_StarTex2", uvOffsetStars);

		heavyCloudsComponent.sharedMaterial.mainTextureOffset = new Vector2 (heavyCloudScrollSpeed,heavyCloudScrollSpeed); 
		starSphere.transform.eulerAngles = new Vector3(270, starRotationSpeedCalc, 0);

		partlyCloudyClouds1Component.material.color = new Color(mostlyClearClouds1Component.material.color.r, mostlyClearClouds1Component.material.color.g, mostlyClearClouds1Component.material.color.b, partlyCloudyFloat);
		mostlyCloudyClouds1Component.material.color = new Color(mostlyClearClouds1Component.material.color.r, mostlyClearClouds1Component.material.color.g, mostlyClearClouds1Component.material.color.b, mostlyCloudyFloat);

		mostlyClearClouds1Component.material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));
		partlyCloudyClouds1Component.material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));
		mostlyCloudyClouds1Component.material.SetFloat("_CloudThickness", (CloudDensity * 0.01f));

		mostlyClearClouds1Component.material.SetFloat("_LoY", CloudHeight);
		mostlyClearClouds1Component.material.SetFloat("_HiY", CloudFade);

		partlyCloudyClouds1Component.material.SetFloat("_LoY", CloudHeight);
		partlyCloudyClouds1Component.material.SetFloat("_HiY", CloudFade);

		mostlyCloudyClouds1Component.material.SetFloat("_LoY", CloudHeight);
		mostlyCloudyClouds1Component.material.SetFloat("_HiY", CloudFade);

		//Mostly Cloudy
		if (weatherForecaster == 11)
		{
			partlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			mostlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;

			if (cloudType == 1)
			{
				if (mostlyCloudyFloat >= 1)
				{
					mostlyCloudyFloat = 1;
				}

				if (partlyCloudyFloat >= 1)
				{
					partlyCloudyFloat = 1;
				}

				if (mostlyClearFloat >= 1)
				{
					mostlyClearFloat = 1;
				}
			}

			if (mostlyCloudyFloat <= 0)
			{
				GenerateMostlyCloudy ();
			}
		}

		//Partly Cloudy
		if (weatherForecaster == 4 || weatherForecaster == 5 || weatherForecaster == 6)
		{
			partlyCloudyFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;

			if (cloudType == 1)
			{
				if (mostlyCloudyFloat <= 0)
				{
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat >= 1)
				{
					partlyCloudyFloat = 1;
				}

				if (mostlyClearFloat >= 1)
				{
					mostlyClearFloat = 1;
				}
			}

			if (partlyCloudyFloat <= 0)
			{
				GeneratePartlyCloudy ();
			}
		}

		if(weatherForecaster == 7)
		{
			partlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat += Time.deltaTime * .015f * RegularCloudFadeInMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;

			if (cloudType == 1)
			{
				if (mostlyCloudyFloat <= 0)
				{
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat <= 0)
				{
					partlyCloudyFloat = 0;
				}

				if (mostlyClearFloat >= 1)
				{
					mostlyClearFloat = 1;
				}

				if (mostlyClearFloat <= 0)
				{
					GenerateMostlyClear ();
				}
			}
		}

		if(weatherForecaster == 8)
		{
			partlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			mostlyCloudyFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;

			mostlyClearFloat -= Time.deltaTime * .015f * RegularCloudFadeOutMultiplier;
			cloudColorMorning.a = mostlyClearFloat;
			cloudColorDay.a = mostlyClearFloat;
			cloudColorEvening.a = mostlyClearFloat;
			cloudColorNight.a = mostlyClearFloat;

			if (cloudType == 1)
			{
				if (mostlyCloudyFloat <= 0)
				{
					mostlyCloudyFloat = 0;
				}

				if (partlyCloudyFloat <= 0)
				{
					partlyCloudyFloat = 0;
				}

				if (mostlyClearFloat <= 0)
				{
					mostlyClearFloat = 0;
				}
			}
		}
	}
}











