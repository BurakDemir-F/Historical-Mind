using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

public class TestAudioPlayer : Singleton<TestAudioPlayer>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    public void PlayMusic()
    {
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
}
