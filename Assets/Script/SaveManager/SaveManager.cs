using JogoPlataforma3D.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;

    // Agora salva em uma subpasta "Saves" dentro de Application.persistentDataPath
    private string _saveDirectory;
    private string _path;

    public int lastLevel;
    public Action<SaveSetup> FileLoaded;
    
    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        // Define o diretório de saves e cria se não existir
        _saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(_saveDirectory))
        {
            Directory.CreateDirectory(_saveDirectory);
        }
        _path = Path.Combine(_saveDirectory, "save.txt");
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.cloth = "BASE";
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }

    [NaughtyAttributes.Button("Test Save")] 
    private void TestSave()
    {
        SaveItens();
        SaveCloth();
        SaveLastLevel(_saveSetup.lastLevel);
    }

    public void SaveItens()
    {
        _saveSetup.coins = Itens.CollectableManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.lifePack = Itens.CollectableManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveCloth()
    {
        _saveSetup.cloth = Player.Instance.ActiveClothType.ToString();
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
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button("Test Load")]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded?.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public int coins;
    public int lifePack;
    public string cloth;
}
