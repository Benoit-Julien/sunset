using UnityEngine;
using System.Collections;

public class CreateCeilling : MonoBehaviour {
	private Material Mat;
	private Vector2 HoleSize;
	private float Thickness;
	private float WallThickness = 0.1f;
	private int number = 40;

	private Vector3[] vertices;
	private Vector3[] Verts;
	private int[] triangles;
	private Vector2[] uvs;

	public void Init (Vector3[] _Verts, Material _Mat, Vector2 _HoleSize, float _Thickness) {
		Verts = _Verts;
		Verts [0].Set (Verts [0].x - WallThickness, Verts [0].y, Verts [0].z - WallThickness);
		Verts [1].Set (Verts [1].x - WallThickness, Verts [1].y, Verts [1].z + WallThickness);
		Verts [2].Set (Verts [2].x + WallThickness, Verts [2].y, Verts [2].z + WallThickness);
		Verts [3].Set (Verts [3].x + WallThickness, Verts [3].y, Verts [3].z - WallThickness);

		Mat = _Mat;
		HoleSize = _HoleSize;
		Thickness = _Thickness;
	}

	void Start () {
		gameObject.AddComponent<MeshRenderer> ().material = Mat;
		Mesh mesh = gameObject.AddComponent<MeshFilter> ().mesh;
		mesh.Clear ();

		Create_Ceilling ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
		mesh.Optimize ();
	}

	private int Get_Next(int i, int Length) {
		if (i + 1 >= Length)
			return 0;
		return i + 1;
	}

	private Vector3[] CreateSecondVerts() {
		Vector3[] VertsSec = new Vector3[] {
			new Vector3 (Verts [0].x, Verts [0].y + Thickness, Verts [0].z),
			new Vector3 (Verts [1].x, Verts [1].y + Thickness, Verts [1].z),
			new Vector3 (Verts [2].x, Verts [2].y + Thickness, Verts [2].z),
			new Vector3 (Verts [3].x, Verts [3].y + Thickness, Verts [3].z)
		};

		return VertsSec;
	}

	void Create_Ceilling () {
		Vector3 center = new Vector3 ((Verts [0].x + Verts [2].x) / 2, Verts [0].y, (Verts [0].z + Verts [2].z) / 2);

		float X = Mathf.Abs (Verts [0].x - Verts [3].x);
		float Z = Mathf.Abs (Verts [0].z - Verts [1].z);
		HoleSize.x = (X - X * (1 - HoleSize.x)) / 2;
		HoleSize.y = (Z - Z * (1 - HoleSize.y)) / 2;

		vertices = new Vector3[number * 4 + (number * 3 + 3 * 4) * 2 + 4 * 4];
		float AnglePos = 0;
		float AngleSpeed = 360 / number;

		int c = 0;
		while (c < number * 4) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;

			vertices [c] = new Vector3 (x, center.y, z);
			vertices [c + 1] = new Vector3 (x, center.y + Thickness, z);
			AnglePos -= AngleSpeed;
			x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;
			vertices [c + 2] = new Vector3 (x, center.y + Thickness, z);
			vertices [c + 3] = new Vector3 (x, center.y, z);
			c += 4;
		}

		int i = 3;
		AnglePos = 0;
		while (c < number * 4 + number * 3 + 3 * 4) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;

			vertices [c + 2] = new Vector3 (x, center.y, z);
			AnglePos -= AngleSpeed;
			x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;
			vertices [c + 1] = Verts [i];
			vertices [c] = new Vector3 (x, center.y, z);

			c += 3;
			if (AnglePos == -90 || AnglePos == -180 || AnglePos == -270 || AnglePos == -360) {
				vertices [c] = new Vector3 (x, center.y, z);
				vertices [c + 2] = Verts [i];
				i = Get_Next (i, Verts.Length);
				vertices [c + 1] = Verts [i];
				c += 3;
			}
		}

		i = 3;
		AnglePos = 0;
		Vector3[] VertsSec = CreateSecondVerts ();
		while (c < number * 4 + (number * 3 + 3 * 4) * 2) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;

			vertices [c] = new Vector3 (x, center.y + Thickness, z);
			AnglePos -= AngleSpeed;
			x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;
			vertices [c + 1] = VertsSec [i];
			vertices [c + 2] = new Vector3 (x, center.y + Thickness, z);

			c += 3;
			if (AnglePos == -90 || AnglePos == -180 || AnglePos == -270 || AnglePos == -360) {
				vertices [c + 1] = VertsSec [i];
				i = Get_Next (i, Verts.Length);
				vertices [c + 2] = VertsSec [i];
				vertices [c] = new Vector3 (x, center.y + Thickness, z);
				c += 3;
			}
		}
		vertices [c] = Verts [0];
		vertices [c + 1] = Verts [1];
		vertices [c + 2] = VertsSec [1];
		vertices [c + 3] = VertsSec [0];

		vertices [c + 4] = Verts [3];
		vertices [c + 5] = VertsSec [3];
		vertices [c + 6] = VertsSec [2];
		vertices [c + 7] = Verts [2];

		vertices [c + 8] = Verts [1];
		vertices [c + 9] = Verts [2];
		vertices [c + 10] = VertsSec [2];
		vertices [c + 11] = VertsSec [1];

		vertices [c + 12] = Verts [0];
		vertices [c + 13] = VertsSec [0];
		vertices [c + 14] = VertsSec [3];
		vertices [c + 15] = Verts [3];

		triangles = new int[number * 6 + (number * 3 + 3 * 4) * 2 + 6 * 4];

		c = 0;
		i = 0;
		while (c < number * 6) {
			if (c != 0 && c % 3 == 0 && c % 6 != 0) {
				triangles [c] = c - (3 + i);
				i += 2;
			} else
				triangles [c] = c - i;
			c++;
		}
		while (c < number * 6 + (number * 3 + 3 * 4) * 2) {
			triangles [c] = c - i;
			c++;
		}
		while (c < triangles.Length) {
			if (c != 0 && c % 3 == 0 && c % 6 != 0) {
				triangles [c] = c - (3 + i);
				i += 2;
			} else
				triangles [c] = c - i;
			c++;
		}

		uvs = new Vector2[vertices.Length];
		Vector2[] tabp = new Vector2[] {
			new Vector2 (0f, 0f),
			new Vector2 (0f, 1f),
			new Vector2 (1f, 1f),
			new Vector2 (1f, 0f)
		};
		c = 0;
		AnglePos = 0;
		while (c < number * 4)
			uvs [c++] = tabp[0];
		
		i = 3;
		while (c < number * 4 + number * 3 + 3 * 4) {
			float x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			float z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;

			float px = (x - Verts [0].x) / (Verts [3].x - Verts [0].z);
			float pz = (z - Verts [0].z) / (Verts [1].z - Verts [0].z);
			uvs [c + 2] = new Vector2 (px, pz);
			uvs [c + 1] = tabp [i];

			AnglePos -= AngleSpeed;
			x = Mathf.Cos (AnglePos * Mathf.PI / 180) * HoleSize.x + center.x;
			z = Mathf.Sin (AnglePos * Mathf.PI / 180) * HoleSize.y + center.z;
			px = (x - Verts [0].x) / (Verts [3].x - Verts [0].z);
			pz = (z - Verts [0].z) / (Verts [1].z - Verts [0].z);
			uvs [c] = new Vector2 (px, pz);

			c += 3;
			if (AnglePos == -90 || AnglePos == -180 || AnglePos == -270 || AnglePos == -360) {
				uvs [c] = new Vector2 (px, pz);
				uvs [c + 2] = tabp [i];
				i = Get_Next (i, Verts.Length);
				uvs [c + 1] = tabp [i];
				c += 3;
			}
		}
		while (c < uvs.Length) {
			uvs [c++] = tabp [0];
		}
	}
}
