using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;
using Valve.VR;

public class MeshManager : MonoBehaviour {
	[System.Serializable]
	public class Properties {
		public enum WallType {
			Left,
			Right,
			Front,
			Back,
			Ground,
			Ceilling
		}

		public WallType Type;
		public Material Mat;
		public float X;
		public float Y;
		public float Z;
		[Range(0.0f, 1.0f)] public float HoleSizeX;
		[Range(0.0f, 1.0f)] public float HoleSizeZ;
		public float HoleThickness = 0.025f;
		public Vector3[] Vertices;
		#if UNITY_EDITOR
			public bool Show = false;
		#endif
	}

	public Light DirectionalLight;
	public Color ChangeColorSky;
	public List<Properties> Walls;

	private AmbientMode DefaultAmbientMode;
	private float DefaultAmbientIntensity;
	private float DefaultLightIntensity;

	private float Find_On_List(Properties.WallType type, string letter) {
		for (int c = 0; c < Walls.Count; c++) {
			if (Walls [c].Type == type) {
				if (letter == "X")
					return Walls [c].X;
				else if (letter == "Y")
					return Walls [c].Y;
				else
					return Walls [c].Z;
			}
		}
		return 0;
	}

	private Material Get_Material(Properties current) {
		if (current.Mat == null)
			return Resources.Load ("Material/Default-Material", typeof(Material)) as Material;
		return current.Mat;
	}

	private void Create_GameObject(Properties current, int c) {
		GameObject Wall = new GameObject ();

		Wall.transform.parent = transform;
		Wall.name = "GameObject n°" + c;
		if (current.Type == Properties.WallType.Ceilling)
			Wall.AddComponent<CreateCeilling> ().Init (current.Vertices, Get_Material (current), new Vector2 (current.HoleSizeX, current.HoleSizeZ), current.HoleThickness);
		else
			Wall.AddComponent<CreateWallAndGround> ().Init (current.Vertices, Get_Material (current), current.Type);
	}

	void Start () {
		DefaultAmbientMode = RenderSettings.ambientMode;
		DefaultAmbientIntensity = RenderSettings.ambientIntensity;
		DefaultLightIntensity = DirectionalLight.intensity;

		RenderSettings.ambientMode = AmbientMode.Flat;
		RenderSettings.ambientSkyColor = Color.white;
		//DirectionalLight.intensity = 0;

		var rect = new HmdQuad_t ();
		if (!SteamVR_PlayArea.GetBounds (SteamVR_PlayArea.Size.Calibrated, ref rect)) {
			Vector3 CamPos = Camera.main.transform.position;

			for (int c = 0; c < Walls.Count; c++) {
				Properties current = Walls [c];

				switch (current.Type) {
				case Properties.WallType.Left:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x - current.X, 0, CamPos.z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (CamPos.x - current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (CamPos.x - current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x - current.X, 0, CamPos.z + Find_On_List (Properties.WallType.Front, "Z"))
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Right:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x + current.X, 0, CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (CamPos.x + current.X, 0, CamPos.z - Find_On_List (Properties.WallType.Back, "Z"))

					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Front:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), 0, CamPos.z + current.Z),
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z + current.Z),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z + current.Z),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), 0, CamPos.z + current.Z)
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Back:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), 0, CamPos.z - current.Z),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z - current.Z),
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), CamPos.z - current.Z),
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), 0, CamPos.z - current.Z)
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Ground:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), 0, CamPos.z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), 0, CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), 0, CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), 0, CamPos.z - Find_On_List (Properties.WallType.Back, "Z"))
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Ceilling:
					current.Vertices = new Vector3[] {
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), current.Y, CamPos.z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (CamPos.x - Find_On_List (Properties.WallType.Left, "X"), current.Y, CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), current.Y, CamPos.z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (CamPos.x + Find_On_List (Properties.WallType.Right, "X"), current.Y, CamPos.z - Find_On_List (Properties.WallType.Back, "Z"))
					};
					Create_GameObject (current, c);
					break;
				}
			}
		} else {
			Vector3[] ZonePos = new Vector3[] {
				new Vector3 (rect.vCorners1.v0, rect.vCorners1.v1, rect.vCorners1.v2),
				new Vector3 (rect.vCorners2.v0, rect.vCorners2.v1, rect.vCorners2.v2),
				new Vector3 (rect.vCorners3.v0, rect.vCorners3.v1, rect.vCorners3.v2),
				new Vector3 (rect.vCorners0.v0, rect.vCorners0.v1, rect.vCorners0.v2)
			};

			for (int c = 0; c < Walls.Count; c++) {
				Properties current = Walls [c];

				switch (current.Type) {
				case Properties.WallType.Left:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[0].x - current.X, 0, ZonePos[0].z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (ZonePos[0].x - current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[0].z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (ZonePos[1].x - current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[1].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[1].x - current.X, 0, ZonePos[1].z + Find_On_List (Properties.WallType.Front, "Z"))
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Right:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[2].x + current.X, 0, ZonePos[2].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[2].x + current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[2].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[3].x + current.X, Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[3].z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (ZonePos[3].x + current.X, 0, ZonePos[3].z - Find_On_List (Properties.WallType.Back, "Z"))

					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Front:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[1].x - Find_On_List (Properties.WallType.Left, "X"), 0, ZonePos[1].z + current.Z),
						new Vector3 (ZonePos[1].x - Find_On_List (Properties.WallType.Left, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[1].z + current.Z),
						new Vector3 (ZonePos[2].x + Find_On_List (Properties.WallType.Right, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[2].z + current.Z),
						new Vector3 (ZonePos[2].x + Find_On_List (Properties.WallType.Right, "X"), 0, ZonePos[2].z + current.Z)
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Back:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[3].x + Find_On_List (Properties.WallType.Right, "X"), 0, ZonePos[3].z - current.Z),
						new Vector3 (ZonePos[3].x + Find_On_List (Properties.WallType.Right, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[3].z - current.Z),
						new Vector3 (ZonePos[0].x - Find_On_List (Properties.WallType.Left, "X"), Find_On_List (Properties.WallType.Ceilling, "Y"), ZonePos[0].z - current.Z),
						new Vector3 (ZonePos[0].x - Find_On_List (Properties.WallType.Left, "X"), 0, ZonePos[0].z - current.Z)
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Ground:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[0].x - Find_On_List (Properties.WallType.Left, "X"), 0, ZonePos[0].z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (ZonePos[1].x - Find_On_List (Properties.WallType.Left, "X"), 0, ZonePos[1].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[2].x + Find_On_List (Properties.WallType.Right, "X"), 0, ZonePos[2].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[3].x + Find_On_List (Properties.WallType.Right, "X"), 0, ZonePos[3].z - Find_On_List (Properties.WallType.Back, "Z"))
					};
					Create_GameObject (current, c);
					break;

				case Properties.WallType.Ceilling:
					current.Vertices = new Vector3[] {
						new Vector3 (ZonePos[0].x - Find_On_List (Properties.WallType.Left, "X"), current.Y, ZonePos[0].z - Find_On_List (Properties.WallType.Back, "Z")),
						new Vector3 (ZonePos[1].x - Find_On_List (Properties.WallType.Left, "X"), current.Y, ZonePos[1].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[2].x + Find_On_List (Properties.WallType.Right, "X"), current.Y, ZonePos[2].z + Find_On_List (Properties.WallType.Front, "Z")),
						new Vector3 (ZonePos[3].x + Find_On_List (Properties.WallType.Right, "X"), current.Y, ZonePos[3].z - Find_On_List (Properties.WallType.Back, "Z")),
					};
					Create_GameObject (current, c);
					break;
				}
			}
		}
	}
}
