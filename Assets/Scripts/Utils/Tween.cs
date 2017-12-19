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
        BOUNCE
    }
    public delegate float EasingFunction(float t);

    [SerializeField]
    protected T _begin;
    [SerializeField]
    protected T _end;
    [SerializeField]
    protected float _duration;
    [SerializeField]
    protected EasingType _easingType;

    protected float _time;
    protected bool _tween = false;
    protected EasingFunction _easingFunction;

    protected void Awake()
    {
        switch (_easingType)
        {
            case EasingType.LINEAR:
                _easingFunction = Easings.Linear;
                break;
            case EasingType.QUADRATIC:
                _easingFunction = Easings.Quadratic.In;
                break;
            case EasingType.CUBIC:
                _easingFunction = Easings.Cubic.In;
                break;
            case EasingType.QUARTIC:
                _easingFunction = Easings.Quartic.In;
                break;
            case EasingType.QUINTIC:
                _easingFunction = Easings.Quintic.In;
                break;
            case EasingType.SINUSOIDAL:
                _easingFunction = Easings.Sinusoidal.In;
                break;
            case EasingType.EXPONENTIAL:
                _easingFunction = Easings.Exponential.In;
                break;
            case EasingType.CIRCULAR:
                _easingFunction = Easings.Circular.In;
                break;
            case EasingType.ELASTIC:
                _easingFunction = Easings.Elastic.In;
                break;
            case EasingType.BACK:
                _easingFunction = Easings.Back.In;
                break;
            case EasingType.BOUNCE:
                _easingFunction = Easings.Bounce.In;
                break;
        }
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
            _begin = begin;
            _end = end;
            _duration = duration;
        }
    }

    protected void Update()
    {
        if (_tween)
        {
            _time += Time.deltaTime;
            ApplyEasing(_easingFunction(_time / _duration));

            if (_time > _duration)
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
