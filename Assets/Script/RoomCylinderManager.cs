using UnityEngine;
using System.Collections;
using Valve.VR;
using LibNoise;
using UnityEditor;

public class RoomCylinderManager : MonoBehaviour
{
	public float RadiusX;
	public float RadiusZ;
	public int FaceNumber = 60;
	public Vector3 Dimension;
	public Material WallMaterial1;
	public Material WallMaterial2;
	public Material GroundMaterial;
	public Material CeillingMaterial;

	public float HoleThickness;
	[Range(0.0f, 1.0f)] public float HoleSizeX;
	[Range(0.0f, 1.0f)] public float HoleSizeZ;


	private float Thickness = 0.1f;

	private GameObject CylinderWall1;
	private GameObject CylinderWall2;
	private GameObject CylinderGround;
	private GameObject CylinderCeilling;

	public void StartCreating () {
		CylinderWall1 = new GameObject ("CylinderWall1");
		CylinderWall2 = new GameObject ("CylinderWall2");
		CylinderGround = new GameObject ("CylinderGround");
		CylinderCeilling = new GameObject ("CylinderCeilling");

		CylinderWall1.transform.parent = transform;
		CylinderWall2.transform.parent = transform;
		CylinderGround.transform.parent = transform;
		CylinderCeilling.transform.parent = transform;

		CylinderWall1.AddComponent<MeshRenderer> ().material = (WallMaterial1 != null) ? WallMaterial1 : Resources.Load("Material/Default-Material", typeof(Material)) as Material;
		CylinderWall2.AddComponent<MeshRenderer> ().material = (WallMaterial2 != null) ? WallMaterial2 : Resources.Load("Material/Default-Material", typeof(Material)) as Material;
		CylinderGround.AddComponent<MeshRenderer> ().material = (GroundMaterial != null) ? GroundMaterial : Resources.Load("Material/Default-Material", typeof(Material)) as Material;
		CylinderCeilling.AddComponent<MeshRenderer> ().material = (CeillingMaterial != null) ? CeillingMaterial : Resources.Load("Material/Default-Material", typeof(Material)) as Material;
		CalculateVerts ();

		CylinderWall1.transform.Translate(0.0f, -2.0f,  0.0f);
		CylinderWall2.transform.Translate(0.0f, -2.0f,  0.0f);
		CylinderCeilling.transform.Translate(0.0f, -2.0f,  0.0f);
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

	private void CalculateVerts() {
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
	}

	private int GetNext (int index, Vector3[] Tab) {
		if (index + 1 >= Tab.Length)
			return 0;
		return index + 1;
	}

	private void CalculateWall(float radiusX, float radiusZ, Vector3 center) {
		Vector3[] vertices = new Vector3[(FaceNumber * 4) * 4];

		Vector3[] GroundVerts = CreateVerts (FaceNumber, Dimension.x, Dimension.z, 0, center);
		Vector3[] CeillingVerts = CreateVerts (FaceNumber, Dimension.x, Dimension.z, (Dimension.y + Thickness) / 2, center);

		Vector3[] GroundVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, -Thickness / 2, center);
		Vector3[] CeillingVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, (Dimension.y + Thickness) / 2, center);

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

		Mesh mesh = CylinderWall1.AddComponent<MeshFilter> ().mesh;

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();

		CylinderWall2.AddComponent<MeshFilter> ().mesh = mesh;
		CylinderWall2.transform.Translate (0, (Dimension.y + Thickness) / 2, 0);
	}

	private void CalculateGround (float radiusX, float radiusZ, Vector3 center) {

		radiusX -= 2.8f;
		radiusZ -= ((2.8f * Dimension.x)/ Dimension.z);

		//TODO : placer pointlights autour de l'ellipse
		for(float b = 0; b< 2.0f*Mathf.PI; b+= Mathf.PI/16.0f)
		{
			float _x = Mathf.Cos (b) * radiusX + center.x;
			float _z = Mathf.Sin (b) * radiusZ + center.z;

			GameObject tempLightObject = new GameObject();
			Light pointComponent = tempLightObject.AddComponent<Light>();
			pointComponent.type = LightType.Point;
			pointComponent.intensity = 5.4f;
			pointComponent.range = 12.5f;
			pointComponent.renderMode = LightRenderMode.ForcePixel;
			tempLightObject.transform.position = new Vector3(_x, -8.0f, _z);				

		}




		Vector3[] vertices = new Vector3[(FaceNumber * 3) * 2];
		Vector3[] GroundVerts = CreateVerts (FaceNumber, radiusX + 0.0f*Thickness, radiusZ + 0.0f*Thickness, 0, center);
		Vector3[] GroundVertsBis = CreateVerts (FaceNumber, radiusX + 0.0f*Thickness, radiusZ + 0.0f*Thickness, -Thickness, center);

		int c = 0;
		int i = 0;
		center.y = 0;
		while (c < (FaceNumber * 3) * 2) {
			if (c < FaceNumber * 3) {
				vertices [c] = GroundVerts [i];
				vertices [c + 1] = center;

				i = GetNext (i, GroundVerts);

				vertices [c + 2] = GroundVerts [i];
			} else {
				vertices [c] = GroundVertsBis [i];

				i = GetNext (i, GroundVertsBis);

				vertices [c + 1] = GroundVertsBis [i];
				vertices [c + 2] = new Vector3 (center.x, -Thickness, center.y);
			}
			c += 3;
		}

		int[] triangles = new int[(FaceNumber * 3) * 2];
		for (c = 0; c < (FaceNumber * 3) * 2; c++) {
			triangles [c] = c;
		}

		Mesh mesh = CylinderGround.AddComponent<MeshFilter> ().mesh;

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();

		//CylinderGround.transform.localScale = new Vector3(0.08f*Dimension.x, 1.0f, 0.08f*Dimension.z);
	}

	private void CalculateCeilling (float radiusX, float radiusZ, Vector3 center) {
		Vector3[] vertices = new Vector3[(FaceNumber * 4) * 3];
		Vector3[] CeillingVerts = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, Dimension.y, center);
		Vector3[] CeillingVertsBis = CreateVerts (FaceNumber, radiusX + Thickness, radiusZ + Thickness, Dimension.y + Thickness, center);

		HoleSizeX = (radiusX - radiusX * (1 - HoleSizeX)) / 2;
		HoleSizeZ = (radiusZ - radiusZ * (1 - HoleSizeZ)) / 2;
		Vector3[] HoleVerts = CreateVerts (FaceNumber, HoleSizeX, HoleSizeZ, Dimension.y, center);
		Vector3[] HoleVertsBis = CreateVerts (FaceNumber, HoleSizeX, HoleSizeZ, Dimension.y + HoleThickness, center);

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

		Mesh mesh = CylinderCeilling.AddComponent<MeshFilter> ().mesh;

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals (60);
		mesh.Optimize ();

		//AssetDatabase.CreateAsset(mesh, "Assets/Resources");
	}
}

