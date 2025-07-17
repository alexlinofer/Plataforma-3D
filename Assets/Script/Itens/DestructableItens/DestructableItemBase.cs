using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = 0.1f;
    public int shakeForce = 5;

    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnValidate()
    {   
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        DropCoins();
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = transform.position;
    }
}
