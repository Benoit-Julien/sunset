using UnityEngine;
using System.Collections;

public class CreateWallAndGround : MonoBehaviour {
	private Material Mat;
	private MeshManager.Properties.WallType Type;
	private float Thickness = 0.1f;

	private Vector3[] vertices;
	private Vector3[] Verts;
	private int[] triangles;
	private Vector3[] normales;
	private Vector2[] uvs;

	public void Init (Vector3[] _Verts, Material _Mat, MeshManager.Properties.WallType _Type) {
		Verts = _Verts;
		Mat = _Mat;
		Type = _Type;
	}

	void Start () {
		gameObject.AddComponent<MeshRenderer> ().material = Mat;

		Mesh mesh = gameObject.AddComponent<MeshFilter> ().mesh;
		mesh.Clear ();

		Create ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		mesh.Optimize ();
	}

	private Vector3[] CreateSecondVerts () {
		Vector3[] VertsSec = new Vector3[4];

		switch (Type) {
		case MeshManager.Properties.WallType.Left:
			Verts [0].Set (Verts [0].x, Verts [0].y - Thickness, Verts [0].z - Thickness);
			Verts [1].Set (Verts [1].x, Verts [1].y + Thickness, Verts [1].z - Thickness);
			Verts [2].Set (Verts [2].x, Verts [2].y + Thickness, Verts [2].z + Thickness);
			Verts [3].Set (Verts [3].x, Verts [3].y - Thickness, Verts [3].z + Thickness);


			VertsSec [0] = new Vector3 (Verts [0].x - Thickness, Verts [0].y, Verts [0].z);
			VertsSec [1] = new Vector3 (Verts [1].x - Thickness, Verts [1].y, Verts [1].z);
			VertsSec [2] = new Vector3 (Verts [2].x - Thickness, Verts [2].y, Verts [2].z);
			VertsSec [3] = new Vector3 (Verts [3].x - Thickness, Verts [3].y, Verts [3].z);
			return VertsSec;

		case MeshManager.Properties.WallType.Right:
			Verts [0].Set (Verts [0].x, Verts [0].y - Thickness, Verts [0].z + Thickness);
			Verts [1].Set (Verts [1].x, Verts [1].y + Thickness, Verts [1].z + Thickness);
			Verts [2].Set (Verts [2].x, Verts [2].y + Thickness, Verts [2].z - Thickness);
			Verts [3].Set (Verts [3].x, Verts [3].y - Thickness, Verts [3].z - Thickness);

			VertsSec [0] = new Vector3 (Verts [0].x + Thickness, Verts [0].y, Verts [0].z);
			VertsSec [1] = new Vector3 (Verts [1].x + Thickness, Verts [1].y, Verts [1].z);
			VertsSec [2] = new Vector3 (Verts [2].x + Thickness, Verts [2].y, Verts [2].z);
			VertsSec [3] = new Vector3 (Verts [3].x + Thickness, Verts [3].y, Verts [3].z);
			return VertsSec;

		case MeshManager.Properties.WallType.Front:
			Verts [0].Set (Verts [0].x - Thickness, Verts [0].y - Thickness, Verts [0].z);
			Verts [1].Set (Verts [1].x - Thickness, Verts [1].y + Thickness, Verts [1].z);
			Verts [2].Set (Verts [2].x + Thickness, Verts [2].y + Thickness, Verts [2].z);
			Verts [3].Set (Verts [3].x + Thickness, Verts [3].y - Thickness, Verts [3].z);

			VertsSec [0] = new Vector3 (Verts [0].x, Verts [0].y, Verts [0].z + Thickness);
			VertsSec [1] = new Vector3 (Verts [1].x, Verts [1].y, Verts [1].z + Thickness);
			VertsSec [2] = new Vector3 (Verts [2].x, Verts [2].y, Verts [2].z + Thickness);
			VertsSec [3] = new Vector3 (Verts [3].x, Verts [3].y, Verts [3].z + Thickness);
			return VertsSec;

		case MeshManager.Properties.WallType.Back:
			Verts [0].Set (Verts [0].x + Thickness, Verts [0].y - Thickness, Verts [0].z);
			Verts [1].Set (Verts [1].x + Thickness, Verts [1].y + Thickness, Verts [1].z);
			Verts [2].Set (Verts [2].x - Thickness, Verts [2].y + Thickness, Verts [2].z);
			Verts [3].Set (Verts [3].x - Thickness, Verts [3].y - Thickness, Verts [3].z);


			VertsSec [0] = new Vector3 (Verts [0].x, Verts [0].y, Verts [0].z - Thickness);
			VertsSec [1] = new Vector3 (Verts [1].x, Verts [1].y, Verts [1].z - Thickness);
			VertsSec [2] = new Vector3 (Verts [2].x, Verts [2].y, Verts [2].z - Thickness);
			VertsSec [3] = new Vector3 (Verts [3].x, Verts [3].y, Verts [3].z - Thickness);
			return VertsSec;

		case MeshManager.Properties.WallType.Ground:
			Verts [0].Set (Verts [0].x - Thickness, Verts [0].y, Verts [0].z - Thickness);
			Verts [1].Set (Verts [1].x - Thickness, Verts [1].y, Verts [1].z + Thickness);
			Verts [2].Set (Verts [2].x + Thickness, Verts [2].y, Verts [2].z + Thickness);
			Verts [3].Set (Verts [3].x + Thickness, Verts [3].y, Verts [3].z - Thickness);

			VertsSec [0] = new Vector3 (Verts [0].x, Verts [0].y - Thickness, Verts [0].z);
			VertsSec [1] = new Vector3 (Verts [1].x, Verts [1].y - Thickness, Verts [1].z);
			VertsSec [2] = new Vector3 (Verts [2].x, Verts [2].y - Thickness, Verts [2].z);
			VertsSec [3] = new Vector3 (Verts [3].x, Verts [3].y - Thickness, Verts [3].z);
			return VertsSec;
		}
		return null;
	}

	private void Create () {
		Vector3[] VertsSec = CreateSecondVerts ();

		vertices = new Vector3[] {
			Verts [0], Verts [1], Verts [2], Verts [3],
			VertsSec [3], VertsSec [2], VertsSec [1], VertsSec [0],
			Verts [0], VertsSec [0], VertsSec [1], Verts [1],
			VertsSec [3], Verts [3], Verts [2], VertsSec [2],
			VertsSec [0], Verts [0], Verts [3], VertsSec [3],
			Verts [1], VertsSec [1], VertsSec [2], Verts [2]
		};

		triangles = new int[] {
			0, 1, 2,
			0, 2, 3,

			4, 5, 6,
			4, 6, 7,

			8, 9, 10,
			8, 10, 11,

			12, 13, 14,
			12, 14, 15,

			16, 17, 18,
			16, 18, 19,

			20, 21, 22,
			20, 22, 23
		};


		Vector2 _00 = new Vector2( 0f, 0f );
		Vector2 _10 = new Vector2( 1f, 0f );
		Vector2 _01 = new Vector2( 0f, 1f );
		Vector2 _11 = new Vector2( 1f, 1f );
		uvs = new Vector2[] {
			_00, _01, _11, _10,
			_00, _00, _00, _00,
			_00, _00, _00, _00,
			_00, _00, _00, _00,
			_00, _00, _00, _00,
			_00, _00, _00, _00
		};
	}
}
