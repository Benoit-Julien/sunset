using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MeshManager))]
public class MeshManagerEditor : Editor
{
	MeshManager t;
	SerializedObject Target;
	SerializedProperty ThisList;
	int ListSize;

	void OnEnable () {
		t = (MeshManager)target;
		Target = new SerializedObject (t);
		ThisList = Target.FindProperty ("Walls");
	}

	public override void OnInspectorGUI () {
		Target.Update ();

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (Target.FindProperty ("DirectionalLight"), new GUIContent ("Directional Light :"));
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (Target.FindProperty ("ChangeColorSky"), new GUIContent ("Change Color Sky :"));
		EditorGUILayout.Space ();
		ListSize = EditorGUILayout.IntField ("List Size", ThisList.arraySize);

		if(ListSize != ThisList.arraySize){
			while(ListSize > ThisList.arraySize){
				ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
			}
			while(ListSize < ThisList.arraySize){
				ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
			}
		}

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("Or");
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		//Or add a new item to the List<> with a button
		EditorGUILayout.LabelField("Add a new item with a button");

		if(GUILayout.Button("Add New")){
			t.Walls.Add (new MeshManager.Properties());
		}
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		for (int c = 0; c < ThisList.arraySize; c++) {
			SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex (c);
			SerializedProperty Show_Prop = MyListRef.FindPropertyRelative ("Show");

			bool show = EditorGUILayout.Foldout ((bool)Show_Prop.boolValue, "Elements" + c);
			if (show) {
				SerializedProperty Type_Prop = MyListRef.FindPropertyRelative ("Type");

				EditorGUILayout.PropertyField (Type_Prop);

				MeshManager.Properties.WallType type = (MeshManager.Properties.WallType)Type_Prop.enumValueIndex;

				switch (type) {
				case MeshManager.Properties.WallType.Left:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("X"), new GUIContent ("Dimensinon X : "));
					break;

				case MeshManager.Properties.WallType.Right:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("X"), new GUIContent ("Dimensinon X : "));
					break;

				case MeshManager.Properties.WallType.Front:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Z"), new GUIContent ("Dimensinon Z : "));
					break;

				case MeshManager.Properties.WallType.Back:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Z"), new GUIContent ("Dimensinon Z : "));
					break;

				case MeshManager.Properties.WallType.Ground:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					break;

				case MeshManager.Properties.WallType.Ceilling:
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Mat"), new GUIContent ("Material : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("Y"), new GUIContent ("Dimensinon Y : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("HoleSizeX"), new GUIContent ("HoleSize X : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("HoleSizeZ"), new GUIContent ("HoleSize Z : "));
					EditorGUILayout.PropertyField (MyListRef.FindPropertyRelative ("HoleThickness"), new GUIContent ("Hole Thickness : "));
					break;
				}

				Show_Prop.boolValue = show;

				EditorGUILayout.Space ();
				//Remove this index from the List
				EditorGUILayout.LabelField("Remove an index from the List<> with a button");
				if(GUILayout.Button("Remove This Index (" + c + ")"))
					ThisList.DeleteArrayElementAtIndex(c);
				EditorGUILayout.Space ();
				EditorGUILayout.Space ();

			} else
				Show_Prop.boolValue = show;
		}
		Target.ApplyModifiedProperties ();
	}
}

