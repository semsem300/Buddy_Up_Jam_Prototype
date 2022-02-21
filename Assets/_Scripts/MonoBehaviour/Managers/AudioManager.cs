using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : StaticInstance<AudioManager>
{
    [SerializeField] AudioSetting setting;
    public AudioSource MainSource;
    AudioSource FXSource;
    protected override void Awake()
    {
        base.Awake();
        FXSource = GetComponent<AudioSource>();
        MainSource.volume = setting.MainAudioVolume;
        FXSource.volume = setting.FXAudioVolume;
        FXSource.mute = setting._mute;
        MainSource.mute = setting._mute;
    }
    private void Update()
    {
        MainSource.volume = setting.MainAudioVolume;
        FXSource.volume = setting.FXAudioVolume;
        FXSource.mute = setting._mute;
        MainSource.mute = setting._mute;
    }
    public void PlaySoundMainSource(AudioClip clip)
    {
        MainSource.Stop();
        MainSource.clip = clip;
        MainSource.Play();

    }
    public void StopSoundFxSource()
    {
        FXSource.Stop();
    }
    public void ChangeSoundMainSource(AudioClip clip1)
    {
        MainSource.Stop();
        MainSource.clip = clip1;
        MainSource.Play();
    }
    public void StopSoundMainSource()
    {
        MainSource.clip = null;
        MainSource.Stop();
    }
    public void PlaySoundFxSource(AudioClip clip)
    {
        setting.PlaySound(clip, FXSource);
    }
    public void UpAndDownAudioMainSource(float amount)
    {
        setting.UpAndDownAudio(amount, MainSource);
    }
    public void MuteDisMuteMainSource(bool mute)
    {
        setting.MuteDisMute(mute, MainSource);
    }
    public void MuteDisMuteFXSource(bool mute)
    {
        setting.MuteDisMute(mute, FXSource);
    }
    public float GetMainVolumeValue()
    {
        return setting.MainAudioVolume;
    }
    public float GetFXVolumeValue()
    {
        return setting.FXAudioVolume;
    }
    public void PlayMouseHover()
    {
        PlaySoundFxSource(setting.mouseHoveringClip);
    }
}
