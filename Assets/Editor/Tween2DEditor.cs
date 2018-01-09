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

        // Begin
        tweenTarget.begin = EditorGUILayout.Vector2Field("Begin", tweenTarget.begin);
        // End
        tweenTarget.end = EditorGUILayout.Vector2Field("End", tweenTarget.end);
    }
}
