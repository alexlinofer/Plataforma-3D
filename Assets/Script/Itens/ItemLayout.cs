using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Itens
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _curSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;

        public void Load(ItemSetup setup)
        {
            _curSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _curSetup.icon;
            
        }

        private void Update()
        {
            uiValue.text = _curSetup.soInt.value.ToString();
        }

    }
}
