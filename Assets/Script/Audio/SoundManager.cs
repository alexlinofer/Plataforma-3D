using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JogoPlataforma3D.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;

    public void PlayMusicbyType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }

}

public enum MusicType
{
    MENU,
    LEVEL_01,
    LEVEL_02,
    LEVEL_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    NONE,
    COLLECT_COIN,
    JUMP,
    COLLECT_POWERUP,
    COLLECT_LIFEPACK,
    SHOOT_PLAYER,
    SHOOT_ENEMY,
    DAMAGE_PLAYER,
    DAMAGE_ENEMY
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}
