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
        DontDestroyOnLoad(gameObject);
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

    public void SaveItens()
    {
        _saveSetup.coins = Itens.CollectableManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Itens.CollectableManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItens();
        Save();
    }

    #endregion

    private void SaveFile(string json)
    {
        // Use essa vers�o quando for um jogo de verdade
        //string path = Application.persistentDataPath + "/save.txt";

        // Estou usando essa vers�o para testes para manter o arquivo na pasta do projeto
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
    public int coins;
    public int lifePack;


    public string playerName;
}
