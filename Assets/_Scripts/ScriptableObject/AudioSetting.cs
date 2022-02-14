using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioSetting_", menuName = "Audio/AudioSetting")]

public class AudioSetting : ScriptableObject
{
    public float FXAudioVolume;
    public float MainAudioVolume;
    public bool _mute;
  
    public void PlaySound(AudioClip clip,AudioSource Source)
    {
        Debug.Log("start fx clip");
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
