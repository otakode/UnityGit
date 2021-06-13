using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneListPopupDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (SceneReference.sceneList != null && SceneReference.sceneList.Count != 0)
		{
			int selectedIndex = Mathf.Max(SceneReference.sceneList.IndexOf(property.FindPropertyRelative("sceneName").stringValue), 0);
			selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, SceneReference.sceneList.ToArray());
			property.FindPropertyRelative("sceneName").stringValue = SceneReference.sceneList[selectedIndex];
		}
		else
			EditorGUI.PropertyField(position, property, label);
		property.serializedObject.ApplyModifiedProperties();
	}
}

#endif

[Serializable]
public class SceneReference : ISerializationCallbackReceiver
{
	public static List<string> sceneList;
	[SerializeField]
	public string sceneName = "";

	public void OnBeforeSerialize()
	{
		sceneList = GetScenesNames();
	}

	public void OnAfterDeserialize()
	{
	}

	public static List<string> GetScenesNames()
	{
		List<string> scenesNames = new List<string>(SceneManager.sceneCountInBuildSettings);
		for (int i = 0 ; i < SceneManager.sceneCountInBuildSettings ; ++i)
		{
			string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
			string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
			scenesNames.Add(sceneName);
		}

		return scenesNames;
	}
}

public class LoadLevel : MonoBehaviour
{
	public void Load(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
