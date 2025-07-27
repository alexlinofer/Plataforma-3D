using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSavedInfo : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void ShowText()
    {
        var setup = SaveManager.Instance.Setup;
        var currentLevel = setup.lastLevel < 1 ? 1 : setup.lastLevel;
        var currentCloth = string.IsNullOrEmpty(setup.cloth) ? "BASE" : setup.cloth;

        textMeshPro.text =
            $"Último Level: {currentLevel}\n" +
            $"Moedas: {setup.coins}\n" +
            $"Life Packs: {setup.lifePack}\n" +
            $"Cloth: {currentCloth}";
    }
}
