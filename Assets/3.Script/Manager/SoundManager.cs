using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public void PlayBGM(AudioClip bgmSound)
    {
        bgmSource.PlayOneShot(bgmSound);
        bgmSource.loop = true;
        bgmSource.volume = 0.05f;
    }

    public void PlaySFX(AudioClip efsSound)
    {
        sfxSource.PlayOneShot(efsSound);
        sfxSource.loop = false;
        sfxSource.volume = 0.1f;
    }
}
