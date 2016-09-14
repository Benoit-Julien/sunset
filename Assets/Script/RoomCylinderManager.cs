using UnityEngine;
using System.Collections;
using Valve.VR;

public class RoomCylinderManager : MonoBehaviour
{
	public float RadiusX;
	public float RadiusZ;
	public int FaceNumber = 60;
	public Vector3 Dimension;
	public Material WallMaterial1;
	//public Material WallMaterial2;
	public Material GroundMaterial;
	public Material CeillingMaterial;

	public float HoleThickness;
	[Range(0.0f, 1.0f)] public float HoleSizeX;
	[Range(0.0f, 1.0f)] public float HoleSizeZ;

	[Range(0.0f, 1.0f)] public float GroundLightHole;

	public float Thickness;
	public int LightNumber;

	public GameObject CylinderWall1;
	public GameObject CylinderWall2;
	public GameObject CylinderGround;
	public GameObject CylinderCeilling;
	public GameObject LightsParent;

	public void InitGameObject () {
		if (CylinderWall1 == null) {
			CylinderWall1 = new GameObject ("CylinderWall");
			CylinderWall1.transform.parent = transform;
			CylinderWall1.AddComponent<MeshFilter> ().sharedMesh = new Mesh ();
			CylinderWall1.AddComponent<MeshRenderer> ();
		}
		CylinderWall1.GetComponent<MeshRenderer> ().material = (WallMaterial1 != null) ? WallMaterial1 : Resources.Load ("Material/Default-Material") as Material;

		/*CylinderWall2 = GameObject.Find ("CylinderWall2");
		if (CylinderWall2 == null) {
			CylinderWall2 = new GameObject ("CylinderWall2");
			CylinderWall2.transform.parent = transform;
			CylinderWall2.AddComponent<MeshFilter> ().sharedMesh = new Mesh ();
			CylinderWall2.AddComponent<MeshRenderer> ();
		}
		CylinderWall2.GetComponent<MeshRenderer> ().material = (WallMaterial2 != null) ? WallMaterial2 : Resources.Load ("Material/Default-Material") as Material;*/

		if (CylinderGround == null) {
			CylinderGround = new GameObject ("CylinderGround");
			CylinderGround.transform.parent = transform;
			CylinderGround.AddComponent<MeshFilter> ().sharedMesh = new Mesh ();
			CylinderGround.AddComponent<MeshRenderer> ();
		}
		CylinderGround.GetComponent<MeshRenderer> ().material = (GroundMaterial != null) ? GroundMaterial : Resources.Load ("Material/Default-Material") as Material;

		if (CylinderCeilling == null) {
			CylinderCeilling = new GameObject ("CylinderCeilling");
			CylinderCeilling.transform.parent = transform;
			CylinderCeilling.AddComponent<MeshFilter> ().sharedMesh = new Mesh ();
			CylinderCeilling.AddComponent<MeshRenderer> ();
		}
		CylinderCeilling.GetComponent<MeshRenderer> ().material = (CeillingMaterial != null) ? CeillingMaterial : Resources.Load ("Material/Default-Material") as Material;


		if (LightsParent == null) {
			LightsParent = new GameObject ("LightsParent");
			LightsParent.transform.parent = transform;
		}

		int diff = LightsParent.transform.childCount - LightNumber;

		if (diff < 0) {
			for (int i = 0; i < diff * -1; i++) {
				Instantiate (Resources.Load ("Prefabs/LightPrefab") as GameObject).name = "Point Light";
			}
		} else {
			for (int i = LightsParent.transform.childCount - 1; i >= LightNumber; i--) {
				DestroyImmediate (LightsParent.transform.GetChild (i).gameObject);
			}
		}
	}

	private Vector3[] CreateVerts (int faceNumber, float radiusX, float radiusZ, float Y, Vector3 center) {
		Vector3[] Verts = new Vector3[faceNumber];

		int AnglePos = 0;
		int AngleSpeed = 360 / faceNumber;
		for (int c = 0; c < faceNumber; c++) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * radiusX + center.x;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * radiusZ + center.z;

			Verts [c] = new Vector3 (x, Y, z);
			AnglePos += AngleSpeed;
		}

		return Verts;
	}

	public void StartCreating () {
		
		var rect = new HmdQuad_t ();

		if (!SteamVR_PlayArea.GetBounds (SteamVR_PlayArea.Size.Calibrated, ref rect)) {
			Vector3 CamPos = Camera.main.transform.position;

			RadiusX = Dimension.x;
			RadiusZ = Dimension.z;

			CalculateWall (Dimension.x, Dimension.z, CamPos);
			CalculateGround (Dimension.x, Dimension.z, CamPos);
			CalculateCeilling (Dimension.x, Dimension.z, CamPos);

		} else {
			Vector3[] ZonePos = new Vector3[] {
				new Vector3 (rect.vCorners1.v0, rect.vCorners1.v1, rect.vCorners1.v2),
				new Vector3 (rect.vCorners2.v0, rect.vCorners2.v1, rect.vCorners2.v2),
				new Vector3 (rect.vCorners3.v0, rect.vCorners3.v1, rect.vCorners3.v2),
				new Vector3 (rect.vCorners0.v0, rect.vCorners0.v1, rect.vCorners0.v2)
			};
			Vector3 center = (ZonePos [2] - ZonePos [0]) / 2;
			float X = ZonePos [3].x - ZonePos [0].x + Dimension.x;
			float Z = ZonePos [1].z - ZonePos [0].z + Dimension.z;

			RadiusX = X;
			RadiusZ = Z;

			CalculateWall (X, Z, center);
			CalculateGround (X, Z, center);
			CalculateCeilling (X, Z, center);
		}

		float AnglePos = 0;
		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Point Light");
		float AngleSpeed = (lights.Length == 0) ? 0 : 360f / lights.Length;
		for (int i = 0; i < lights.Length; i++) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * RadiusX;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * RadiusZ;

			lights[i].transform.parent = LightsParent.transform;
			lights[i].transform.localPosition = new Vector3 (x, -8f, z);

			AnglePos += AngleSpeed;
		}
	}

	private int GetNext (int index, Vector3[] Tab) {
		if (index + 1 >= Tab.Length)
			return 0;
		return index + 1;
	}

	private void CalculateWall(float radiusX, float radiusZ, Vector3 center) {
		Vector3[] vertices = new Vector3[(FaceNumber * 4) * 4];

		Vector3[] GroundVerts = CreateVerts (FaceNumber, Dimension.x, Dimension.z, -Thickness, center);
		Vector3[] CeillingVerts = CreateVerts (FaceNumber, Dimension.x, Dimension.z, Dimension.y + Thickness, center);

		Vector3[] GroundVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, -Thickness, center);
		Vector3[] CeillingVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, Dimension.y + Thickness, center);

		int i = 0;
		int c = 0;
		while (c < vertices.Length) {
			if (c < FaceNumber * 4) {
				
				vertices [c] = GroundVerts [i];
				vertices [c + 3] = CeillingVerts [i];

				i = GetNext (i, GroundVerts);
				vertices [c + 1] = GroundVerts [i];
				vertices [c + 2] = CeillingVerts [i];

			} else if (c < (FaceNumber * 4) * 2) {

				vertices [c] = GroundVertsBis [i];
				vertices [c + 1] = CeillingVertsBis [i];

				i = GetNext (i, GroundVerts);

				vertices [c + 2] = CeillingVertsBis [i];
				vertices [c + 3] = GroundVertsBis [i];

			} else if (c < (FaceNumber * 4) * 3) {
				
				vertices [c] = CeillingVerts [i];
				vertices [c + 3] = CeillingVertsBis [i];

				i = GetNext (i, CeillingVerts);

				vertices [c + 1] = CeillingVerts [i];
				vertices [c + 2] = CeillingVertsBis [i];

			} else {

				vertices [c] = GroundVerts [i];
				vertices [c + 1] = GroundVertsBis [i];

				i = GetNext (i, GroundVerts);

				vertices [c + 2] = GroundVertsBis [i];
				vertices [c + 3] = GroundVerts [i];

			}
			c += 4;
		}

		c = 0;
		i = 0;
		int[] triangles = new int[(FaceNumber * 6) * 4];
		while (c < triangles.Length) {
			if (c != 0 && c % 3 == 0 && c % 6 != 0) {
				triangles [c] = c - (3 + i);
				i += 2;
			} else
				triangles [c] = c - i;
			c++;
		}

		Vector2[] uv = new Vector2[vertices.Length];
		Vector2 _00 = new Vector2 (0, 0);

		c = 0;
		float AnglePos = 0;
		float AngleSpeed = 360 / FaceNumber;
		float prevX = 0;

		while (c < uv.Length) {
			if (c < FaceNumber * 4) {
				AnglePos += AngleSpeed;

				float x = AnglePos / 360;

				uv [c] = new Vector2 (prevX, 0f);
				uv [c + 1] = new Vector2 (x, 0f);
				uv [c + 2] = new Vector2 (x, 1f);
				uv [c + 3] = new Vector2 (prevX, 1f);
				prevX = x;

				c += 4;
			} else
				uv [c++] = _00;
		}

		Mesh mesh = CylinderWall1.GetComponent<MeshFilter> ().sharedMesh;
		mesh.Clear ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();

		//CylinderWall2.GetComponent<MeshFilter> ().sharedMesh = mesh;
	}

	private void CalculateGround (float radiusX, float radiusZ, Vector3 center) {
		radiusX = radiusX * GroundLightHole;
		radiusZ = radiusZ * GroundLightHole;
		center.y = 0;

		Vector3[] vertices = new Vector3[(FaceNumber * 3) * 2 + FaceNumber * 4];
		Vector3[] GroundVerts = CreateVerts (FaceNumber, radiusX, radiusZ, 0, center);
		Vector3[] GroundVertsBis = CreateVerts (FaceNumber, radiusX, radiusZ, -Thickness, center);
		Vector2 centerBis = new Vector3 (center.x, -Thickness, center.y);

		int c = 0;
		int i = 0;
		while (c < vertices.Length) {
			if (c < FaceNumber * 3) {
				vertices [c] = GroundVerts [i];
				vertices [c + 1] = center;

				i = GetNext (i, GroundVerts);

				vertices [c + 2] = GroundVerts [i];
				c += 3;
			} else if (c < (FaceNumber * 3) * 2) {
				vertices [c] = GroundVertsBis [i];

				i = GetNext (i, GroundVertsBis);

				vertices [c + 1] = GroundVertsBis [i];
				vertices [c + 2] = centerBis;
				c += 3;
			} else {
				vertices [c] = GroundVerts [i];
				vertices [c + 3] = GroundVertsBis [i];

				i = GetNext (i, GroundVerts);

				vertices [c + 1] = GroundVerts [i];
				vertices [c + 2] = GroundVertsBis [i];
				c += 4;
			}
		}

		int[] triangles = new int[(FaceNumber * 3) * 2 + FaceNumber * 6];
		for (c = 0; c < (FaceNumber * 3) * 2; c++) {
			triangles [c] = c;
		}
		i = 0;
		while (c < triangles.Length) {
			if (c != 0 && c % 3 == 0 && c % 6 != 0) {
				triangles [c] = c - (3 + i);
				i += 2;
			} else
				triangles [c] = c - i;
			c++;
		}

		Vector2[] uv = new Vector2[vertices.Length];
		Vector2 _00 = Vector2.zero;
		Vector2 middle = new Vector2 (0.5f, 0.5f);

		i = 0;
		c = 0;
		while (c < uv.Length) {
			if (c < FaceNumber * 3) {
				float x = (GroundVerts [i].x + radiusX) / (radiusX * 2);
				float z = (GroundVerts [i].z + radiusZ) / (radiusZ * 2);

				uv [c] = new Vector2 (x, z);
				uv [c + 1] = middle;

				i = GetNext (i, GroundVerts);
				x = (GroundVerts [i].x + radiusX) / (radiusX * 2);
				z = (GroundVerts [i].z + radiusZ) / (radiusZ * 2);

				uv [c + 2] = new Vector2 (x, z);
				c += 3;
			} else
				uv [c++] = _00;
		}

		Mesh mesh = CylinderGround.GetComponent<MeshFilter> ().sharedMesh;
		mesh.Clear ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();
	}

	private void CalculateCeilling (float radiusX, float radiusZ, Vector3 center) {
		Vector3[] vertices = new Vector3[(FaceNumber * 4) * 3];
		Vector3[] CeillingVerts = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, Dimension.y, center);
		Vector3[] CeillingVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, Dimension.y + Thickness, center);

		float HoleX = radiusX - radiusX * (1 - HoleSizeX);
		float HoleZ = radiusZ - radiusZ * (1 - HoleSizeZ);
		Vector3[] HoleVerts = CreateVerts (FaceNumber, HoleX, HoleZ, Dimension.y, center);
		Vector3[] HoleVertsBis = CreateVerts (FaceNumber, HoleX, HoleZ, Dimension.y + HoleThickness, center);

		int c = 0;
		int i = 0;
		while (c < vertices.Length) {
			if (c < FaceNumber * 4) {

				vertices [c] = HoleVerts [i];
				vertices [c + 3] = HoleVertsBis [i];

				i = GetNext (i, HoleVerts);

				vertices [c + 1] = HoleVerts [i];
				vertices [c + 2] = HoleVertsBis [i];
			} else if (c < (FaceNumber * 4) * 2) {

				vertices [c] = HoleVerts [i];
				vertices [c + 1] = CeillingVerts [i];

				i = GetNext (i, HoleVerts);

				vertices [c + 2] = CeillingVerts [i];
				vertices [c + 3] = HoleVerts [i];

			} else {

				vertices [c] = HoleVertsBis [i];
				vertices [c + 3] = CeillingVertsBis [i];

				i = GetNext (i, HoleVertsBis);

				vertices [c + 1] = HoleVertsBis [i];
				vertices [c + 2] = CeillingVertsBis [i];

			}
			c += 4;
		}
			
		i = 0;
		int[] triangles = new int[(int)(vertices.Length * 1.5f)];
		for (c = 0; c < triangles.Length; c++) {
			if (c != 0 && c % 3 == 0 && c % 6 != 0) {
				triangles [c] = c - (3 + i);
				i += 2;
			} else
				triangles [c] = c - i;
		}

		Mesh mesh = CylinderCeilling.GetComponent<MeshFilter> ().sharedMesh;
		mesh.Clear ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();
	}
}