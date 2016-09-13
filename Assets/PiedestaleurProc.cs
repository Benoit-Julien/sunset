using UnityEngine;
using System.Collections;

public class PiedestaleurProc : MonoBehaviour {

	MeshManager manager;

	bool AddedLayer = false;

	// Use this for initialization
	void Start () {
		manager = GetComponent<MeshManager>();

		float x = manager.Find_On_List(MeshManager.Properties.WallType.Front, "X");
		float z = manager.Find_On_List(MeshManager.Properties.WallType.Front, "Z");

		Debug.Log(x+", "+z);

		GameObject.Find("Piedestal").transform.position =  new Vector3(x, 0.7f, z - (0.6f /2.0f + 0.0f)); 



	}


	void AddTagRecursively(GameObject obj, int layer)
	{
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

		if(!AddedLayer && manager.done)
		{
			AddTagRecursively (manager.gameObject, 8);
			AddedLayer = true;
		}
	}
}
