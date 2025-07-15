using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JogoPlataforma3D.Singleton;
using TMPro;

namespace Itens
{
    // Enum to define the type of collectable item
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class CollectableManager : Singleton<CollectableManager>
    {
        public List<ItemSetup> itemSetups;

        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach (var i in itemSetups)
            {
                i.soInt.value = 0;
            }

        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itemSetups.Find(itemSetups => itemSetups.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = -1)
        {
            if (amount > 0) return;
            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if(item.soInt.value < 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button("Add Coin")]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button("Add Life Pack")]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }
    }


    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
