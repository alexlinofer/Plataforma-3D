using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JogoPlataforma3D.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "Rafael";
    }

    #region SAVE
    [NaughtyAttributes.Button("Save")]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }

    #endregion

    private void SaveFile(string json)
    {
        // Use essa versão quando for um jogo de verdade
        //string path = Application.persistentDataPath + "/save.txt";

        // Estou usando essa versão para testes para manter o arquivo na pasta do projeto
        string path = Application.streamingAssetsPath + "/save.txt";

        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    [NaughtyAttributes.Button("Save Level 1")]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button("Save Level 5")]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
