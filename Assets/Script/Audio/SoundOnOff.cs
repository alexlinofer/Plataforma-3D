using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnOff : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Escolha no Inspector qual parâmetro controlar: "MusicAudioParam" ou "SFXAudioParam"
    public string exposedParam = "MusicAudioParam";

    [ContextMenu("Sound On")]
    public void SoundOn()
    {
        if (audioMixer != null && !string.IsNullOrEmpty(exposedParam))
        {
            audioMixer.SetFloat(exposedParam, 0f); // 0dB = volume máximo
        }
    }

    [ContextMenu("Sound Off")]
    public void SoundOff()
    {
        if (audioMixer != null && !string.IsNullOrEmpty(exposedParam))
        {
            audioMixer.SetFloat(exposedParam, -80f); // -80dB = mudo
        }
    }
}
