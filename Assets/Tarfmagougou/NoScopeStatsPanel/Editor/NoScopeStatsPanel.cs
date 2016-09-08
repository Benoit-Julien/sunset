using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace tarfmagougou
{
	[System.Serializable]
	struct ObjectStat
	{
		public string name;
		public ulong mesh_tris;
		public HashSet<int> mesh_total;
		public ulong mesh_instances;
		public HashSet<int> mat_total;
		public ulong mat_instances;
		public ulong shader_passes;
		public ulong renderers_total;
		public ulong renderers_visible;
		public double renderers_size;

		public ObjectStat(string name)
		{
			this.name = name;
			mesh_tris = 0u;
			mesh_total = new HashSet<int>();
			mesh_instances = 0u;
			mat_total = new HashSet<int>();
			mat_instances = 0u;
			shader_passes = 0u;
			renderers_total = 0u;
			renderers_visible = 0u;
			renderers_size = 0f;
		}
	}

	public class NoScopeStatsPanel : EditorWindow
	{
		[SerializeField] bool _display_in_edit_mode = true;

		ObjectStat[] _selected_objects = new ObjectStat[0];
		Dictionary<GameObject, ObjectStat> _cache = new Dictionary<GameObject, ObjectStat>();
		Vector2 _scroll_pos;

		[MenuItem("Window/NoScope Stats Panel")]
		public static void ShowWindow()
		{
			NoScopeStatsPanel w = EditorWindow.GetWindow<NoScopeStatsPanel>();
			w.titleContent = new GUIContent("NoScope Stats");
		}

		void OnGUI()
		{
			/* Toolbar */
			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
			_display_in_edit_mode = GUILayout.Toggle(_display_in_edit_mode, "Update in EditMode", EditorStyles.toolbarButton);
			if (GUILayout.Button("Refresh Visible", EditorStyles.toolbarButton)) {
				OnHierarchyChange();
			}

			EditorGUILayout.EndHorizontal();
			if (!_display_in_edit_mode && !EditorApplication.isPlaying) {
				return;
			}

			/* Data */
			EditorGUILayout.BeginVertical();
			_scroll_pos = EditorGUILayout.BeginScrollView(_scroll_pos);


			EditorGUILayout.Space();
			foreach (ObjectStat x in _selected_objects) {
				if (x.mesh_total == null || x.mat_total == null)
					continue;

				EditorGUILayout.LabelField(x.name, EditorStyles.boldLabel);
				EditorGUILayout.BeginHorizontal();
				EditorGUIUtility.labelWidth = 30;
				EditorGUILayout.LabelField(x.renderers_total.ToString("N0") + " renderers");
				EditorGUILayout.LabelField(x.renderers_visible.ToString("N0") + " visible");
				EditorGUILayout.LabelField(((x.renderers_size / 1024f) / 1024f).ToString("F") + " MB");
				EditorGUILayout.EndHorizontal();

				EditorGUI.indentLevel++;

				EditorGUIUtility.labelWidth = 100;
				EditorGUILayout.LabelField("Tris : ", x.mesh_tris.ToString("N0"));
				EditorGUILayout.LabelField("Meshes : ", x.mesh_total.Count.ToString("N0")
						+ " (" + x.mesh_instances.ToString("N0") + " instances)");
				EditorGUILayout.LabelField("Materials : ", x.mat_total.Count.ToString("N0")
						+ " (" + x.mat_instances.ToString("N0") + " instances)");
				EditorGUILayout.LabelField("Shader Passes : ", x.shader_passes.ToString("N0") + " (max)");


				EditorGUI.indentLevel--;
				EditorGUILayout.Space();
			}

			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndVertical();
		}

		/* User selected one or multiple objects, display and/or calculate stats. */
		void OnSelectionChange()
		{
			_selected_objects = new ObjectStat[Selection.gameObjects.Length];

			for (int i = 0; i < Selection.gameObjects.Length; ++i) {
				_selected_objects[i] = GetStats(Selection.gameObjects[i]);
			}

			Repaint();
		}

		/* Cache gets invalidated while editing scene. Reprocess stats. */
		void OnHierarchyChange()
		{
			_cache.Clear();
			OnSelectionChange();
		}

		/* Return a cached stat object or generate new one. */
		ObjectStat GetStats(GameObject obj)
		{
			if (_cache.ContainsKey(obj)) {
				return _cache[obj];
			}
			ObjectStat ret = new ObjectStat(obj.name);
			RecurseStats(obj, ref ret);
			_cache[obj] = ret;
			return ret;
		}

		/* Drill down hierarchy and collect stats. */
		static void RecurseStats(GameObject obj, ref ObjectStat ret)
		{
			MeshFilter mf = obj.GetComponent<MeshFilter>();
			if (mf) {
				Mesh sm = mf.sharedMesh;
				if (sm) {
					GetSharedMeshStats(sm, ref ret);
				}
			}

			SkinnedMeshRenderer smr = obj.GetComponent<SkinnedMeshRenderer>();
			if (smr) {
				Mesh sm = smr.sharedMesh;
				if (sm) {
					GetSharedMeshStats(sm, ref ret);
				}
			}

			Renderer r = obj.GetComponent<Renderer>();
			if (r) {
				ret.renderers_total += 1u;
				ret.renderers_visible += r.isVisible ? 1u : 0u;
				GetMaterialStats(r.sharedMaterials, ref ret);
			}

			for (int i = 0; i < obj.transform.childCount; ++i) {
				RecurseStats(obj.transform.GetChild(i).gameObject, ref ret);
			}
		}

		static void GetSharedMeshStats(Mesh sm, ref ObjectStat ret)
		{
			/* First time we encounter or is an instance. */
			if (!ret.mesh_total.Contains(sm.GetInstanceID())) {
				ret.renderers_size += (double)Profiler.GetRuntimeMemorySize(sm);
			}
			ret.mesh_tris += (ulong)(sm.triangles.LongLength / 3);
			ret.mesh_total.Add(sm.GetInstanceID());
			ret.mesh_instances += sm.name.Contains("Instance") ? 1u : 0u;
		}

		static void GetMaterialStats(Material[] mats, ref ObjectStat ret)
		{
			for (int i = 0; i < mats.Length; ++i) {
				/* First time we encounter or is an instance. */
				if (!ret.mat_total.Contains(mats[i].GetInstanceID())) {
					ret.shader_passes += (ulong)mats[i].passCount;
					ret.renderers_size += (double)Profiler.GetRuntimeMemorySize(mats[i]);

					/* Get all textures from shader. */
					Shader s = mats[i].shader;
					for (int j = 0; j < ShaderUtil.GetPropertyCount(s); ++j) {
						if (ShaderUtil.GetPropertyType(s, j) != ShaderUtil.ShaderPropertyType.TexEnv)
							continue;

						Texture t = mats[i].GetTexture(ShaderUtil.GetPropertyName(s, j));
						if (t == null)
							continue;
						
						ret.renderers_size += (double)Profiler.GetRuntimeMemorySize(t);
					}
				}
				ret.mat_total.Add(mats[i].GetInstanceID());
				ret.mat_instances += mats[i].name.Contains("Instance") ? 1u : 0u;
			}
		}
	}
}
