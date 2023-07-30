using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectColorChange))]
public class EffectColorChangeButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EffectColorChange effect = (EffectColorChange)target;
        if(GUILayout.Button("Change Color and Save"))
        {
            effect.ChangeColor();
        }
    }
}
