using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;

    private void Start()
    {
        soInt = CollectableManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }


    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            CollectableManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
            Debug.Log("Vida recuperada");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
            Debug.Log("L apertado");
        }
    }
}
