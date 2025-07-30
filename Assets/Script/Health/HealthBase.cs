using Cloth;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 30f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    // Flashcolor adicionado por mim
    private FlashColor _flashColor;
    private ParticleSystem _particleSystem;

    public List<UIFillUpdate> uiGunUpdater;

    public float damageMultiply = 1f;

    public SFXType damageSFX;


    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }

    // Start Adicionado por mim
    private void Start()
    {
        _flashColor = GetComponentInChildren<FlashColor>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }


    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 3f);
        OnKill?.Invoke(this);
    }

    System.Collections.IEnumerator WaitForKill()
    {
        //destroyOnKill = true;
        yield return new WaitForEndOfFrame();
        Kill();
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(4);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiply;
        PlaySFX();

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        // tudo abaixo adicionado por mim
        transform.DOMove(transform.position - dir, .1f);

        if (_flashColor != null) _flashColor.Flash();
        if (_particleSystem != null) _particleSystem.Emit(60);

        transform.position -= transform.forward;

        _currentLife -= damage * damageMultiply;
        PlaySFX();

        if (_currentLife <= 0)
        {
            StartCoroutine(WaitForKill());
        }
        UpdateUI();
        OnDamage?.Invoke(this);
        Debug.Log("damage");
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(damageSFX);
    }

    private void UpdateUI()
    {
        if(uiGunUpdater != null)
        {
            uiGunUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damageMultiply, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1f;
    }
}
