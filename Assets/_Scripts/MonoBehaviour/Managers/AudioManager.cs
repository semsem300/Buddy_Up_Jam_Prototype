using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSetting Source;
    public AudioSource MainSource;
    AudioSource FXSource;
    protected override void Awake()
    {
        base.Awake();
        FXSource = GetComponent<AudioSource>();
        MainSource.volume = Source.MainAudioVolume;
        FXSource.volume = Source.FXAudioVolume;
        FXSource.mute = Source._mute;
        MainSource.mute = Source._mute;
    }
    public void PlaySoundMainSource(AudioClip clip)
    {
        Source.PlaySound(clip, MainSource);
    }
    public void StopSoundFxSource()
    {
        FXSource.Stop();
    }
    public void PlaySoundFxSource(AudioClip clip)
    {
        Source.PlaySound(clip, FXSource);
    }
    public void UpAndDownAudioMainSource(float amount)
    {
        Source.UpAndDownAudio(amount, MainSource);
    }
    public void MuteDisMuteMainSource(bool mute)
    {
        Source.MuteDisMute(mute, MainSource);
    }
    public void MuteDisMuteFXSource(bool mute)
    {
        Source.MuteDisMute(mute, FXSource);
    }
    public float GetMainVolumeValue()
    {
        return Source.MainAudioVolume;
    }
    public float GetFXVolumeValue()
    {
        return Source.FXAudioVolume;
    }
}
