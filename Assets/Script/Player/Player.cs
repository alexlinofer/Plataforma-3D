using System.Collections;
using UnityEngine;
using Ebac.StateMachine;
using System.Collections.Generic;
using JogoPlataforma3D.Singleton;
using Cloth;

public class Player : Singleton<Player>
{
    public List<Collider> colliders;
    [Header("Configs")]
    public CharacterController characterController;
    public Animator animator;

    [Header("General Setup")]
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    public UIFillUpdate uiGunUpdater;
    public bool alive = true;

    private bool _jumping = false;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;



    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;

        StartCoroutine(ApplySavedClothWithDelay());
    }

    private IEnumerator ApplySavedClothWithDelay()
    {
        yield return new WaitForSeconds(0.05f); // Ajuste conforme necessário

        string clothString = SaveManager.Instance.Setup.cloth;
        if (!string.IsNullOrEmpty(clothString) && System.Enum.TryParse<ClothType>(clothString, out var clothType) && clothType != ClothType.BASE)
        {
            ClothSetup setup = ClothManager.Instance.GetSetupByType(clothType);
            if (setup != null)
            {
                ChangeTexture(setup, 100f); // Ajuste a duração se quiser
            }
            else
            {
                _clothChanger.ResetTexture();
            }
        }
        else
        {
            _clothChanger.ResetTexture();
        }
    }

    private void OnKill(HealthBase h)
    {
        if (alive)
        {
            alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 5f);

        }
    }

    private void Revive()
    {
        alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        colliders.ForEach(i => i.enabled = true);
    }

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
    }
#endregion

    private void Update()
    {
        if(!alive) return;

        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }

            vSpeed = 0f;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;

                if (!_jumping)
                {
                    _jumping = true;
                    animator.SetTrigger("Jump");
                }
                
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

    public void ApplyClothByType(ClothType clothType, float duration = 5f)
    {
        var setup = ClothManager.Instance.GetSetupByType(clothType);
        if (setup != null)
        {
            ChangeTexture(setup, duration);
        }
    }

    public Cloth.ClothType ActiveClothType
    {
        get
        {
            // Supondo que ClothChanger tenha uma referência ao ClothSetup atual
            // Adapte conforme sua lógica de troca de roupa
            return _clothChanger.CurrentClothSetup != null ? _clothChanger.CurrentClothSetup.clothType : Cloth.ClothType.BASE;
        }
    }

    public void RestoreClothFromSave()
    {
        if (!string.IsNullOrEmpty(SaveManager.Instance.Setup.cloth))
        {
            if (System.Enum.TryParse<ClothType>(SaveManager.Instance.Setup.cloth, out var clothType))
            {
                ApplyClothByType(clothType, 5f);
            }
        }
    }
}
