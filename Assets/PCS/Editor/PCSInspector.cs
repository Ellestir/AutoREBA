using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PCS
{
	[InitializeOnLoad]
	[CustomEditor(typeof(PCSConfig))]
	public class PCSInspector : Editor
	{
		public PCSConfig config;
		//TextAsset jsonFile;
		bool undoCallbackAdded;
		Vector2 scrollPos;
		public Texture2D img;
		GUIStyle title;

		private void OnEnable()
		{
			config = (PCSConfig)target;
			//CheckRailingData();

			if (!undoCallbackAdded)
			{
				Undo.undoRedoPerformed += UndoCallback;
				undoCallbackAdded = true;
			}

			if (!config.settingsImported)
				ImportSettings();

			if (!config.ready)
				config.CreatePCS();
	

		}
		
		private void OnDisable()
		{
			if (undoCallbackAdded)
			{
				Undo.undoRedoPerformed -= UndoCallback;
				undoCallbackAdded = false;
			}
			init = false;


			if (config.settingsImported && config.editMode == PCSConfig.EditModes.Railings)
			{
				config.editMode = PCSConfig.EditModes.None;
				config.EditRailings();
				UpdateRailingEditColliders(config.editMode);
			}

		}

		void UndoCallback()
		{
			config.Fix();
		}


		bool init = false;
		public override void OnInspectorGUI()
		{
			if (!config.settingsImported)
				ImportSettings();

			if (!init)
			{
				title = new GUIStyle(GUI.skin.label);
				init = true;
				title.fontSize = 18;
			}
			EditorGUILayout.LabelField("Procedural Conveyor System", title, GUILayout.Height(1.5f*title.fontSize));

			#region Stlyes
			/*
			string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
			path = path.Replace("Editor/PCSInspector.cs", "Styles");

			string[] stylePaths = AssetDatabase.GetSubFolders(path);


			EditorGUILayout.LabelField("Style");
			scrollPos = GUILayout.BeginScrollView(scrollPos, true, false, GUI.skin.horizontalScrollbar, GUIStyle.none, GUI.skin.box, GUILayout.Height(126));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			//Color c = GUI.color;
			//GUI.color = Color.red;
			foreach (string stylePath in stylePaths)
			{

				string[] split = stylePath.Split('/');
				string styleName = split[split.Length - 1];

				Texture2D thumbnail = (Texture2D)AssetDatabase.LoadAssetAtPath(stylePath + "/thumb.png", typeof(Texture2D));

				GUIContent button;
				if (thumbnail != null)
					button = new GUIContent(thumbnail);
				else
					button = new GUIContent(styleName);

				if (GUILayout.Button(button, GUILayout.Width(100), GUILayout.Height(100)))
				{
					if(LoadStyle(stylePath))
							config.CreatePCS();
				}
			}
			//GUI.color = c;


			GUILayout.BeginVertical(GUILayout.Height(100));
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal(GUILayout.Width(100));
			GUILayout.FlexibleSpace();
			//GUIStyle linkLabel = new GUIStyle();
			//linkLabel.normal.textColor = Color.blue;

			if (GUILayout.Button("More", GUILayout.Width(100), GUILayout.Height(100)))
				Application.OpenURL("https://assetstore.unity.com/lists/procedural-conveyor-system-additional-styles-108107");


			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndScrollView();
			*/
			#endregion

			GUILayout.BeginVertical(GUI.skin.box);
			GUILayout.Space(2);

			//int length = Mathf.Max(1, EditorGUILayout.IntField("Length", config.length));
			EditorGUILayout.LabelField("Length");
			GUILayout.BeginHorizontal(GUILayout.Width(100));

			bool changed = false;
			int length = config.length;

			if (GUILayout.Button("-", GUILayout.Width(22)))
			{
				length = config.length - 1;

				if(length > 0)
					changed = true;
			}

			GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField(((config.length + 2f) / 5).ToString("f1"), GUILayout.Width(35));
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("+", GUILayout.Width(22)))
			{
				length = config.length + 1;

				if (length > 0)
					changed = true;
			}
			//GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			if (changed)
			{
				Undo.RecordObject(config, "PCS");

				config.length = length;

				config.CreatePCS();

			}

			GUILayout.Space(8);




			EditorGUILayout.LabelField("Width");
			GUILayout.BeginHorizontal(GUILayout.Width(100));

			changed = false;
			float width = config.width;

			if (GUILayout.Button("-", GUILayout.Width(22)))
			{
				width = config.width - 0.1f;

				if (width > 0)
					changed = true;
			}

			GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField(width.ToString("f1"), GUILayout.Width(35));
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("+", GUILayout.Width(22)))
			{
				width = config.width + 0.1f;

				if (width > 0)
					changed = true;
			}
			//GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			if (changed)
			{
				Undo.RecordObject(config, "PCS");

				config.width = width;

				config.CreatePCS();

			}

			GUILayout.Space(8);




			EditorGUI.BeginChangeCheck();
			float speed = EditorGUILayout.FloatField("Speed", config.speed);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(config, "PCS");
				config.speed = speed;
				config.CreatePCS();
			}

			GUILayout.Space(8);

			EditorGUI.BeginChangeCheck();
			Color32 colour = EditorGUILayout.ColorField("Colour", config.colour);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(config, "PCS");
				config.colour = colour;
				config.CreatePCS();
			}

			GUILayout.Space(8);

			EditorGUI.BeginChangeCheck();
			bool internalsEnabled = EditorGUILayout.Toggle("Internals", config.internalsEnabled);
			int internalsCount = config.internalsCount;
			if (internalsEnabled)
				internalsCount = Mathf.Clamp(EditorGUILayout.IntField("Internals Count", config.internalsCount), 2, length);

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(config, "PCS");
				config.internalsEnabled = internalsEnabled;
				config.internalsCount = internalsCount;
				config.CreatePCS();
			}

			GUILayout.Space(8);

			EditorGUI.BeginChangeCheck();
			//PCSConfig.EditModes editMode = (PCSConfig.EditModes)EditorGUILayout.EnumPopup("Edit Mode", config.editMode);
			bool edit = GUILayout.Toggle(config.editMode == PCSConfig.EditModes.None ? false : true, "Edit Railings", GUI.skin.button);
			//bool railingEditMode = GUILayout.Toggle(config.railingEditMode, "Edit Railings", GUI.skin.button);
			if (EditorGUI.EndChangeCheck())
			{
				if (config.settingsImported)
				{
					Undo.RecordObject(config, "PCS");
					config.CreatePCS();

					config.editMode = edit ? PCSConfig.EditModes.Railings : PCSConfig.EditModes.None;
					SceneView.RepaintAll();

					config.EditRailings();

					UpdateRailingEditColliders(config.editMode);
				}
			}

			GUILayout.Space(2);


			GUILayout.EndVertical();

			/*EditorGUI.BeginChangeCheck();
			jsonFile = (TextAsset)EditorGUILayout.ObjectField("JSON", jsonFile, typeof(TextAsset), true);

			if (EditorGUI.EndChangeCheck() && jsonFile != null)
			{
				Undo.RecordObject(config, "PCS");

				PCSJson data = CreateInstance<PCSJson>();
				JsonUtility.FromJsonOverwrite(jsonFile.text, data);
				jsonFile = null;
				path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
				path = path.Replace("Editor/PCSInspector.cs", "prefabs/");
				data.Parse(config, path);
				DestroyImmediate(data);
			}*/

			/*GUILayout.Space(20);
			EditorGUILayout.LabelField("0");
			CheckRailingData();
			for (int i = 0; i < config.railingData[0].enabledStates.Count; i++)
			{
				EditorGUILayout.LabelField("[" + i + "] " + config.railingData[0].enabledStates[i]);
			}
			

			GUILayout.Space(50);
			EditorGUILayout.LabelField("----------------------------------------");
			//EditorUtility.SetDirty(config);*/

			//base.OnInspectorGUI();

		}

		bool drag = false;
		//bool[][] changedByDrag;
		bool dragType;
		private void OnSceneGUI()
		{
			Event e = Event.current;

			if (config.editMode == PCSConfig.EditModes.Railings && e != null && e.button == 0 && !e.alt)
			{
				Undo.RecordObject(config, "PCS Railing Edit");
				HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

				if (e.type == EventType.MouseDown)
				{
					SceneView scene = SceneView.lastActiveSceneView;

					Vector3 mousePos = e.mousePosition;
					float ppp = EditorGUIUtility.pixelsPerPoint;
					mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
					mousePos.x *= ppp;

					Ray ray = scene.camera.ScreenPointToRay(mousePos);

					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						if (config.railingEditCollidersRailingIndex.ContainsKey(hit.collider))
						{
							int side = config.railingEditCollidersSideIndex[hit.collider];
							int railing = config.railingEditCollidersRailingIndex[hit.collider];

							if (!e.shift)
								config.railingData[side].enabledStates[railing] = true;
							else
								config.railingData[side].enabledStates[railing] = false;

							drag = true;
							//changedByDrag = new bool[2][];
							//changedByDrag[0] = new bool[config.railingData[0].enabledStates.Count];
							//changedByDrag[1] = new bool[config.railingData[1].enabledStates.Count];

							//changedByDrag[side][railing] = true;


							dragType = config.railingData[side].enabledStates[railing];

							e.Use();
						}
					}
				}
				else if (e.type == EventType.MouseDrag && drag)
				{
					Undo.RecordObject(config, "PCS Railing Edit");
					SceneView scene = SceneView.lastActiveSceneView;

					Vector3 mousePos = e.mousePosition;
					float ppp = EditorGUIUtility.pixelsPerPoint;
					mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
					mousePos.x *= ppp;

					Ray ray = scene.camera.ScreenPointToRay(mousePos);

					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						if (config.railingEditCollidersRailingIndex.ContainsKey(hit.collider))
						{
							int side = config.railingEditCollidersSideIndex[hit.collider];
							int railing = config.railingEditCollidersRailingIndex[hit.collider];
							//if (!changedByDrag[side][railing])
							//{
							config.railingData[side].enabledStates[railing] = dragType;
							//changedByDrag[side][railing] = true;
							//}
							e.Use();
						}
					}
				}
				else if (e.type == EventType.MouseUp && drag)
				{
					drag = false;
				}
			}

			SceneView.RepaintAll();
		}


		public void UpdateRailingEditColliders(PCSConfig.EditModes editMode)
		{
			if (editMode == PCSConfig.EditModes.Railings)
			{
				if (config.railingEditCollidersParent != null)
					DestroyImmediate(config.railingEditCollidersParent);

				config.deleteAllColliders();

				config.physicsParent.SetActive(false);

				config.railingEditCollidersParent = new GameObject("RailingEditColliders");
				config.railingEditCollidersParent.transform.parent = config.transform;
				config.railingEditCollidersParent.transform.localPosition = Vector3.zero;
				config.railingEditCollidersParent.transform.localRotation = Quaternion.identity;
				config.railingEditCollidersParent.transform.localScale = Vector3.one;
				config.railingEditCollidersParent.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable;
				for (int i = 0; i < 2; i++)
				{
					if (config.railingData[i].enabledStates != null)
					{
						Vector3 instantiatePosition = config.startCap.positionOffset;
						for (int j = 0; j < config.railingData[i].enabledStates.Count; j++)
						{
							for (int k = 0; k < config.railing.prefab.transform.childCount; k++)
							{
								if (config.railing.renderers != null && k < config.railing.renderers.Length && config.railing.renderers[k] != null)
								{
									Gizmos.color = config.railingData[i].enabledStates[j] ? Color.green : Color.red;

									Vector3 centre = config.railing.renderers[k].bounds.center;
									Vector3 offset = new Vector3(((config.width - 0.4f) / 2) + 0.1f, 0, 0);
									centre.x *= (i == 0 ? 1 : -1);
									offset.x *= (i == 0 ? 1 : -1);
									Vector3 position = instantiatePosition + centre + offset;
									Vector3 size = config.railing.renderers[k].bounds.size;

									BoxCollider c = config.railingEditCollidersParent.AddComponent<BoxCollider>();
									c.center = position;
									c.size = size;

									config.railingEditCollidersSideIndex.Add(c, i);
									config.railingEditCollidersRailingIndex.Add(c, j);
								}
							}
							instantiatePosition += config.belt.positionOffset;
						}
					}
				}
			}
			else
			{
				config.physicsParent.SetActive(true);
				DestroyImmediate(config.railingEditCollidersParent);
				//CheckRailingData();
				config.CreatePCS();
			}
		}

		public void ImportSettings()
		{
			string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
			path = path.Replace("Editor/PCSInspector.cs", "Styles");

			string[] stylePaths = AssetDatabase.GetSubFolders(path);

			if (config.length == 0)
				config.length = 18;

			if (!config.internalsEnabled && config.internalsCount == 0)
			{
				config.internalsEnabled = true;
				config.internalsCount = 4;
			}

			if (LoadStyle(stylePaths[0]))
				config.CreatePCS();
		}

		bool LoadStyle(string stylePath)
		{
			TextAsset jsonFile = (TextAsset)AssetDatabase.LoadAssetAtPath(stylePath + "/style.json", typeof(TextAsset));

			if (jsonFile != null)
			{
				Undo.RecordObject(config, "PCS");

				PCSJson data = CreateInstance<PCSJson>();
				JsonUtility.FromJsonOverwrite(jsonFile.text, data);
				jsonFile = null;
				//string prefabFolderPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
				//prefabFolderPath = prefabFolderPath.Replace("Editor/PCSInspector.cs", "prefabs/");
				data.Parse(config, stylePath + "/Prefabs/");
				DestroyImmediate(data);
				return true;
			}
			else
			{
				Debug.LogError("PCS Style not found, stlye.json missing from " + stylePath);
				return false;
			}
		}
	}



	public class PCSJson : ScriptableObject
	{
		public string beltPrefabName;
		public string startCapPrefabName;
		public string endCapPrefabName;
		public string railingPrefabName;
		public string railingStartCapPrefabName;
		public string railingEndCapPrefabName;
		public string railingDoubleCapPrefabName;
		public string internalsPrefabName;

		public bool mirrorBelt;
		public bool mirrorStartCap;
		public bool mirrorEndCap;
		public bool mirrorRailing;
		public bool mirrorRailingStartCap;
		public bool mirrorRailingEndCap;
		public bool mirrorRailingDoubleCap;
		public bool mirrorInternals;

		public string beltLengthMode;
		public string railingLengthMode;
		public float internalsOffset;

		public void Parse(PCSConfig config, string prefabFolderPath)
		{
			config.belt.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + beltPrefabName + ".prefab", typeof(GameObject));
			config.belt.mirror = mirrorBelt;
			config.belt.lengthMode = (PCSPart.LengthMode)System.Enum.Parse(typeof(PCSPart.LengthMode), beltLengthMode);

			config.startCap.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + startCapPrefabName + ".prefab", typeof(GameObject));
			config.startCap.mirror = mirrorStartCap;

			config.endCap.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + endCapPrefabName + ".prefab", typeof(GameObject));
			config.endCap.mirror = mirrorEndCap;

			config.railing.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + railingPrefabName + ".prefab", typeof(GameObject));
			config.railing.mirror = mirrorRailing;
			config.railing.lengthMode = (PCSPart.LengthMode)System.Enum.Parse(typeof(PCSPart.LengthMode), railingLengthMode);

			config.railingStartCap.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + railingStartCapPrefabName + ".prefab", typeof(GameObject));
			config.railingStartCap.mirror = mirrorRailingStartCap;

			config.railingEndCap.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + railingEndCapPrefabName + ".prefab", typeof(GameObject));
			config.railingEndCap.mirror = mirrorRailingEndCap;

			config.railingDoubleCap.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + railingDoubleCapPrefabName + ".prefab", typeof(GameObject));
			config.railingDoubleCap.mirror = mirrorRailingDoubleCap;

			config.internals.prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFolderPath + internalsPrefabName + ".prefab", typeof(GameObject));
			config.internals.mirror = mirrorInternals;
			config.internals.positionOffset = new Vector3(0, 0, internalsOffset);
			config.settingsImported = true;
		}
	}

}