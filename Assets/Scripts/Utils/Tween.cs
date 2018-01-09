using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tween<T> : MonoBehaviour
{
    public enum EasingType
    {
        LINEAR,
        QUADRATIC,
        CUBIC,
        QUARTIC,
        QUINTIC,
        SINUSOIDAL,
        EXPONENTIAL,
        CIRCULAR,
        ELASTIC,
        BACK,
        BOUNCE,
        CUSTOM
    }
    public enum InOut
    {
        IN,
        OUT,
        INOUT
    }
    public delegate float EasingFunction(float t);

    public T begin;
    public T end;
    public float duration;
    public EasingType easingType = EasingType.LINEAR;
    public InOut inOut = InOut.IN;
    public bool playOnAwake = false;
    public AnimationCurve customCurve = AnimationCurve.Linear(0, 0, 1, 1);
    protected Easings.CustomEasing customEasing = null;

    protected float _time;
    protected bool _tween = false;
    protected EasingFunction _easingFunction;

    protected void Awake()
    {
        switch (easingType)
        {
            case EasingType.LINEAR:
                _easingFunction = Easings.Linear;
                break;
            case EasingType.QUADRATIC:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Quadratic.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Quadratic.Out;
                else
                    _easingFunction = Easings.Quadratic.InOut;
                break;
            case EasingType.CUBIC:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Cubic.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Cubic.Out;
                else
                    _easingFunction = Easings.Cubic.InOut;
                break;
            case EasingType.QUARTIC:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Quartic.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Quartic.Out;
                else
                    _easingFunction = Easings.Quartic.InOut;
                break;
            case EasingType.QUINTIC:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Quintic.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Quintic.Out;
                else
                    _easingFunction = Easings.Quintic.InOut;
                break;
            case EasingType.SINUSOIDAL:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Sinusoidal.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Sinusoidal.Out;
                else
                    _easingFunction = Easings.Sinusoidal.InOut;
                break;
            case EasingType.EXPONENTIAL:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Exponential.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Exponential.Out;
                else
                    _easingFunction = Easings.Exponential.InOut;
                break;
            case EasingType.CIRCULAR:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Circular.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Circular.Out;
                else
                    _easingFunction = Easings.Circular.InOut;
                break;
            case EasingType.ELASTIC:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Elastic.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Elastic.Out;
                else
                    _easingFunction = Easings.Elastic.InOut;
                break;
            case EasingType.BACK:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Back.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Back.Out;
                else
                    _easingFunction = Easings.Back.InOut;
                break;
            case EasingType.BOUNCE:
                if (inOut == InOut.IN)
                    _easingFunction = Easings.Bounce.In;
                else if (inOut == InOut.OUT)
                    _easingFunction = Easings.Bounce.Out;
                else
                    _easingFunction = Easings.Bounce.InOut;
                break;
            case EasingType.CUSTOM:
                customEasing = new Easings.CustomEasing(customCurve);
                _easingFunction = customEasing.Easing;
                break;
        }

        if (playOnAwake)
            Play();
    }

    public void Play()
    {
        if (!_tween)
        {
            _tween = true;
            _time = 0f;
        }
    }

    public void Play(T begin, T end, float duration)
    {
        if (!_tween)
        {
            _tween = true;
            _time = 0f;
            this.begin = begin;
            this.end = end;
            this.duration = duration;
        }
    }

    protected void Update()
    {
        if (_tween)
        {
            _time += Time.deltaTime;
            ApplyEasing(_easingFunction(_time / duration));

            if (_time > duration)
                _tween = false;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                Play();
        }
    }

    protected abstract void ApplyEasing(float t);
}
