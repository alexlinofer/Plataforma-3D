using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [NaughtyAttributes.Button("Save")]
    private void Save()
    {
        SaveSetup setup = new SaveSetup();
        setup.lastLevel = 2;
        setup.playerName = "Rafael";

        string setupToJson = JsonUtility.ToJson(setup, true);
        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }

    private void SaveFile(string json)
    {
        // Use essa versão quando for um jogo de verdade
        //string path = Application.persistentDataPath + "/save.txt";

        // Estou usando essa versão para testes para manter o arquivo na pasta do projeto
        string path = Application.streamingAssetsPath + "/save.txt";

        Debug.Log(path);
        File.WriteAllText(path, json);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
