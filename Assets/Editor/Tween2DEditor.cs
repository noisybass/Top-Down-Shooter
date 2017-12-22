using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tween<Vector2>), true)]
public class Tween2DEditor : TweenEditor<Vector2>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tween<Vector2> tweenTarget = (Tween<Vector2>)target;
        EditorGUILayout.Vector2Field("Begin Position", tweenTarget.begin);
        EditorGUILayout.Vector2Field("End Position", tweenTarget.end);
    }
}
