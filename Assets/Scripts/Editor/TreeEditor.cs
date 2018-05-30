using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TreeScript))]
public class TreeEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TreeScript Tree = (TreeScript)target; 

        if(GUILayout.Button("Generate Tree"))
        {
            Tree.GenerateTree();
        }
    }
}
