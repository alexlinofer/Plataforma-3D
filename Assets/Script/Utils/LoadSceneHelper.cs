using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadLastLevel()
    {
        int lastSavedLevel = SaveManager.Instance.Setup.lastLevel;
        
        if (lastSavedLevel < 1)
        {
            LoadLevel(1);
        }
        else
        {
            LoadLevel(lastSavedLevel);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelCoRoutine());
    }

    IEnumerator LoadLevelCoRoutine()
    {
        int lastLevel = SaveManager.Instance.Setup.lastLevel;
        SaveManager.Instance.SaveItens();
        SaveManager.Instance.SaveCloth();
        yield return new WaitForSeconds(0.1f);
        lastLevel++;
        yield return new WaitForSeconds(0.1f);
        SaveManager.Instance.SaveLastLevel(lastLevel);
        yield return new WaitForSeconds(0.1f);
        LoadLevel(lastLevel);
    }
}
