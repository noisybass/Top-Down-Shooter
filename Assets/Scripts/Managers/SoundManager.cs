using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource music;
    public AudioSource button;
    public AudioSource pickUp;
    public AudioSource shoot;
    public AudioSource dogAttack;
    public AudioSource playerHit;
    public AudioSource dogHit;
    public AudioSource monsterHit;
    public AudioSource hit;
    public AudioSource explosion;
    public AudioSource death;

    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void PlayButton()
    {
        button.Play();
    }

    public void PlayPickUp()
    {
        pickUp.Play();
    }

    public void PlayShoot()
    {
        shoot.Play();
    }

    public void PlayDogAttack()
    {
        dogAttack.Play();
    }

    public void PlayPlayerHit()
    {
        playerHit.Play();
    }

    public void PlayDogHit()
    {
        dogHit.Play();
    }

    public void PlayMonsterHit()
    {
        monsterHit.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }

    public void PlayExplosion()
    {
        explosion.Play();
    }

    public void PlayDeath()
    {
        death.Play();
    }
}
