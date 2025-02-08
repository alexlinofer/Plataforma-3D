using System.Collections;
using UnityEngine;
using Ebac.StateMachine;
using JogoPlataforma3D.Singleton;

public class PlayerController : MonoBehaviour
{
    public bool canMove = false;
    public bool canJump = false;
    public bool canPerformActions = false;
    public bool isGrounded = true;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        GameManager.Instance.playerController = this;
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Apenas verifica se pode se mover e realizar ações
        if (canMove)
        {
            MovePlayer();
        }

        if (canJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    private void Jump()
    {

        if (!isGrounded || !canJump) return;

        // Adiciona o impulso de pulo
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, _rigidbody.velocity.z);

        isGrounded = false;
        canJump = false;

        Debug.Log("should have jumped");

        // Alterna para o estado JUMP
        GameManager.Instance.stateMachine.SwitchState(GameManager.GameStates.JUMP, this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isGrounded = true;

            // Volta ao estado GAMEPLAY ao tocar o chão
            GameManager.Instance.stateMachine.SwitchState(GameManager.GameStates.GAMEPLAY, this);
        }
    }
}
