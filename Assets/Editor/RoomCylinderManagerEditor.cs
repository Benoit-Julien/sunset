using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RoomCylinderManager))]
public class RoomCylinderManagerEditor : Editor {

	RoomCylinderManager manager;
	SerializedObject Target;

	void OnEnable () {
		manager = (RoomCylinderManager)target;

		if(manager == null)
			Debug.Log("Oulala");

		Target = new SerializedObject (manager);

		if(Target == null)
			Debug.Log("Rololo");

		manager.InitGameObject ();
	}

	public override void OnInspectorGUI () {
		Target.Update ();

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		if (GUILayout.Button ("Update Mesh And Lights"))
			manager.InitGameObject ();

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (Target.FindProperty ("FaceNumber"), new GUIContent ("Face Number"));
		EditorGUILayout.PropertyField (Target.FindProperty ("Thickness"), new GUIContent ("Wall Thickness"));
		EditorGUILayout.PropertyField (Target.FindProperty ("Dimension"), new GUIContent ("Dimension"));
		EditorGUILayout.PropertyField (Target.FindProperty ("LightNumber"), new GUIContent ("Light Number"));

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (Target.FindProperty ("WallMaterial1"), new GUIContent ("Wall Material"));
		//EditorGUILayout.PropertyField (Target.FindProperty ("WallMaterial2"), new GUIContent ("Wall Material Top"));
		EditorGUILayout.PropertyField (Target.FindProperty ("GroundMaterial"), new GUIContent ("Ground Material"));
		EditorGUILayout.PropertyField (Target.FindProperty ("CeillingMaterial"), new GUIContent ("Ceilling Material"));

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (Target.FindProperty ("HoleThickness"), new GUIContent ("Hole Thickness"));
		EditorGUILayout.PropertyField (Target.FindProperty ("HoleSizeX"), new GUIContent ("Hole Size X"));
		EditorGUILayout.PropertyField (Target.FindProperty ("HoleSizeZ"), new GUIContent ("Hole Size Z"));

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (Target.FindProperty ("GroundLightHole"), new GUIContent ("Ground Light Hole Thickness"));

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		Target.ApplyModifiedProperties ();
		manager.StartCreating ();
	}
}
