using UnityEngine;
using UnityEditor;
using System.Collections;

#if UNITY_EDITOR
[CustomEditor(typeof(CurvedText))]
public class CurvedTextEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}
}
#endif