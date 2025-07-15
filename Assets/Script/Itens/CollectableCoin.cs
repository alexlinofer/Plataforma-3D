using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class CollectableCoin : CollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddByType(ItemType.COIN);
    }
}
