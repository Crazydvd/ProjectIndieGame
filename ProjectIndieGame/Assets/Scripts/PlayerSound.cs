using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip[] Audioclips;

    public int RollingsoundIndex;
    public int HittingsoundIndex;
    public int AttackingsoundIndex;

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void playRollingSound()
    {
        _audioSource.clip = Audioclips[RollingsoundIndex];
        _audioSource.loop = true;
    }

    public void stopRollingSound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
    }

    public void playHittingSound()
    {
        _audioSource.loop = false;
        _audioSource.PlayOneShot(Audioclips[HittingsoundIndex]);
    }

    public void playAttackingSound()
    {
        _audioSource.loop = false;
        _audioSource.PlayOneShot(Audioclips[AttackingsoundIndex]);
    }
}
