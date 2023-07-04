using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EventSystems;

[InitializeOnLoad]
public class PCSClickDetect
{
	static PCSClickDetect()
	{
		SceneView.beforeSceneGui -= ClickDetect;
		SceneView.beforeSceneGui += ClickDetect;
	}

	static void ClickDetect(SceneView sceneView)
	{
		if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && Event.current.modifiers == EventModifiers.None)
		{
			Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Transform t;
				t = GetConveyorObject(hit.transform);
				if (t != null)
				{
					PCS.PCSConfig config = t.gameObject.GetComponent<PCS.PCSConfig>();
					if (config.editMode == PCS.PCSConfig.EditModes.None && Selection.activeGameObject != t.gameObject)
					{
						Event.current.Use();
						Selection.activeGameObject = t.gameObject;
					}
				}
			}
		}

		Transform GetConveyorObject(Transform t)
		{
			if (t.GetComponent<PCS.PCSConfig>() != null)
				return t;
			else if (t.parent != null)
				return GetConveyorObject(t.parent);
			else
				return null;
		}	}

}
