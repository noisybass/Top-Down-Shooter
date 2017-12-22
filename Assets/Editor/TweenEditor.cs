using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (Tween<>), true)]
public class TweenEditor<T> : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        Tween<T> tweenTarget = (Tween<T>)target;
        EditorGUILayout.FloatField("Duration", tweenTarget.duration);

        //if (target.GetType() == typeof(TweenPosition2D) || target.GetType() == typeof(TweenScale2D))
        //{
        //    Tween<Vector2> tweenTarget = (Tween<Vector2>)target;
        //    EditorGUILayout.Vector2Field("Begin Position", tweenTarget.begin);
        //    EditorGUILayout.Vector2Field("End Position", tweenTarget.end);
        //}
        //else if (target.GetType() == typeof(TweenPosition3D) || target.GetType() == typeof(TweenScale3D))
        //{
        //    Tween<Vector3> tweenTarget = (Tween<Vector3>)target;
        //    EditorGUILayout.Vector3Field("Begin Position", tweenTarget.begin);
        //    EditorGUILayout.Vector3Field("End Position", tweenTarget.end);
        //}
    }
}
