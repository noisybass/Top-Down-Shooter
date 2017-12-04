using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : Singleton<EffectsManager>
{
    public enum EffectType
    {
        DUST,
        MONSTER_HIT,
        OBJECT_HIT,
        MONSTER_SPAWN
    }

    [SerializeField]
    private Effect _dustPrefab;
    [SerializeField]
    private int _dustPoolSize = 5;
    private Pool<Effect> _dustEffects;

    [SerializeField]
    private Effect _monsterHitPrefab;
    [SerializeField]
    private int _monsterHitPoolSize = 5;
    private Pool<Effect> _monsterHitEffects;

    [SerializeField]
    private Effect _objectHitPrefab;
    [SerializeField]
    private int _objectHitPoolSize = 5;
    private Pool<Effect> _objectHitEffects;

    [SerializeField]
    private Effect _monsterSpawnPrefab;
    [SerializeField]
    private int _monsterSpawnPoolSize = 5;
    private Pool<Effect> _monsterSpawnEffects;

    protected override void Awake()
    {
        base.Awake();

        _dustEffects = new Pool<Effect>(_dustPoolSize, _dustPrefab, gameObject);
        //_monsterHitEffects = new Pool<Effect>(_monsterHitPoolSize, _monsterHitPrefab, gameObject);
        //_objectHitEffects = new Pool<Effect>(_objectHitPoolSize, _objectHitPrefab, gameObject);
        _monsterSpawnEffects = new Pool<Effect>(_monsterSpawnPoolSize, _monsterSpawnPrefab, gameObject);
    }

    public void CreateEffect(EffectType type, Vector3 position, bool flipX)
    {
        Effect effect;

        switch (type)
        {
            case EffectType.DUST:
                effect = _dustEffects.CreateObject();
                effect.Init(position, flipX);
                StartCoroutine(DestroyEffect(effect));
                break;
            case EffectType.MONSTER_HIT:
                effect = _monsterHitEffects.CreateObject();
                effect.Init(position, flipX);
                StartCoroutine(DestroyEffect(effect));
                break;
            case EffectType.OBJECT_HIT:
                effect = _objectHitEffects.CreateObject();
                effect.Init(position, flipX);
                StartCoroutine(DestroyEffect(effect));
                break;
            case EffectType.MONSTER_SPAWN:
                effect = _monsterSpawnEffects.CreateObject();
                effect.Init(position, flipX);
                StartCoroutine(DestroyEffect(effect));
                break;
            default:
                break;
        }
    }

    IEnumerator DestroyEffect(Effect effect)
    {
        yield return new WaitForSeconds(effect.Duration);

        switch(effect.Type)
        {
            case EffectType.DUST:
                _dustEffects.DestroyObject(effect);
                break;
            case EffectType.MONSTER_HIT:
                _monsterHitEffects.DestroyObject(effect);
                break;
            case EffectType.OBJECT_HIT:
                _objectHitEffects.DestroyObject(effect);
                break;
            case EffectType.MONSTER_SPAWN:
                _monsterSpawnEffects.DestroyObject(effect);
                break;
            default:
                break;
        }
    }


}
