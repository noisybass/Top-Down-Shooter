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

        // Duration
        tweenTarget.duration = EditorGUILayout.FloatField("Duration", tweenTarget.duration);

        // Easing type
        tweenTarget.easingType = (Tween<T>.EasingType)EditorGUILayout.EnumPopup("Easing function", tweenTarget.easingType);
        if (tweenTarget.easingType != Tween<T>.EasingType.LINEAR)
        {
            if (tweenTarget.easingType != Tween<T>.EasingType.CUSTOM)
                tweenTarget.inOut = (Tween<T>.InOut)EditorGUILayout.EnumPopup("In Out", tweenTarget.inOut);
            else
                tweenTarget.customCurve = EditorGUILayout.CurveField("Custom Curve", tweenTarget.customCurve);
        }

        // Play on awake
        tweenTarget.playOnAwake = EditorGUILayout.Toggle("Play On Awake", tweenTarget.playOnAwake);
    }
}
