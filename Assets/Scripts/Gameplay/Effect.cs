using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    [SerializeField]
    private AnimationClip _animation;
    private float _duration;
    public float Duration
    {
        get { return _duration; }
    }

    [SerializeField]
    private EffectsManager.EffectType _type;
    public EffectsManager.EffectType Type
    {
        get { return _type; }
    }

    private SpriteRenderer _renderer;

    void Awake()
    {
        _duration = _animation.averageDuration;
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(Vector3 position, bool flipX)
    {
        transform.position = position;
        _renderer.flipX = flipX;
        gameObject.SetActive(true);
    }


}
