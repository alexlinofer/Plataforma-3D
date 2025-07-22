using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    private int _gunIndex;

    [SerializeField] private List<GunBase> _gunPrefabs;
    public FlashColor flashColor;

    protected override void Init()
    {
        base.Init();

        CreateGun();
        ChangeGunInput();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun()
    {
        if (_gunPrefabs.Count > 0)
        {
            _currentGun = Instantiate(_gunPrefabs[_gunIndex], gunPosition);

            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }

    private void ChangeGun(int i)
    {
        int newIndex = i - 1; // Ajusta o índice para começar de 0

        if (newIndex >= 0 && newIndex < _gunPrefabs.Count) // Garante que o índice é válido
        {
            if (_currentGun != null)
            {
                Destroy(_currentGun.gameObject); // Remove a arma anterior antes de trocar
            }

            _gunIndex = newIndex; // Atualiza o índice
            _currentGun = Instantiate(_gunPrefabs[_gunIndex], gunPosition); // Instancia a nova arma

            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }

    private void ChangeGunInput()
    {
        inputs.Gameplay.Gun1.performed += cts => ChangeGun(1);
        inputs.Gameplay.Gun2.performed += cts => ChangeGun(2);
        inputs.Gameplay.Gun3.performed += cts => ChangeGun(3);
    }


    private void StartShoot()
    {
        if(player.alive) _currentGun.StartShoot();
        flashColor?.Flash();
    }

    private void CancelShoot()
    {
        if (player.alive) _currentGun.StopShoot();
    }

    private void OnDestroy()
    {
        inputs.Gameplay.Gun1.performed -= cts => ChangeGun(1);
        inputs.Gameplay.Gun2.performed -= cts => ChangeGun(2);
        inputs.Gameplay.Gun3.performed -= cts => ChangeGun(3);
    }
}
