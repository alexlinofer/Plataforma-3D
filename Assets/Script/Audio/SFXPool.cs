using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JogoPlataforma3D.Singleton;
using UnityEngine.Audio;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;
    public AudioMixerGroup sfxMixer;

    public int poolSize = 10;

    private int _index = 0;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceitem();
        }
    }

    private void CreateAudioSourceitem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);

        _audioSourceList.Add(go.AddComponent<AudioSource>());
        go.GetComponent<AudioSource>().outputAudioMixerGroup = sfxMixer;
        
    }

    public void Play(SFXType sfxType)
    {
        if(sfxType == SFXType.NONE) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);
        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;

        if (_index >= _audioSourceList.Count) _index = 0;

    }

}
