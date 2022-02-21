using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioSetting_", menuName = "Audio/AudioSetting")]

public class AudioSetting : ScriptableObject
{
    [Range(0, 1)]
    public float FXAudioVolume;
    [Range(0, 1)]
    public float MainAudioVolume;
    public bool _mute;
    public AudioClip mainThemeClip;
    public AudioClip dialogueThemeClip;
    public AudioClip winThemeClip;
    public AudioClip defeatThemeClip;
    public AudioClip bossPhase1Clip;
    public AudioClip bossPhase2Clip;
    public AudioClip bossPhase3Clip;
    public AudioClip bossPhase4_1Clip;
    public AudioClip bossPhase4_2Clip;
    public AudioClip mouseHoveringClip;
    public void PlaySound(AudioClip clip, AudioSource Source)
    {
        Source.Stop();
        Source.clip = clip;
        Source.Play();
    }

    public void UpAndDownAudio(float amount, AudioSource Source)
    {
        Source.volume = amount;
        MainAudioVolume = amount;
    }

    public void MuteDisMute(bool mute, AudioSource Source)
    {
        Source.mute = mute;
        _mute = mute;
    }


}
