using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    public void OnLoad(SaveSetup setup)
    {
        if(setup.lastLevel <= 1)
        {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
        }
        else
        {
            uiTextName.text = "Play " + (setup.lastLevel);
        }
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
