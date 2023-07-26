using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillEffect))]
public class EffectColorChangeButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SkillEffect effect = (SkillEffect)target;
        if(GUILayout.Button("Color Change"))
        {
            effect.ChangeColor();
        }
    }
}
