using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("[Rigidbody]")]
    public Rigidbody2D Rigidbody2D;

    [Header("[Animator]")]
    [SerializeField] private Animator _animator;
    [Header("[Parametr]")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 10f;
    [Header("[UI]")]
    [SerializeField] private GameObject _playerStats;
    [Header("[Player]")]
    [SerializeField] private Player _player;

    protected float targetVelocity;

    enum TransitionParametr
    {
        Jump,
        Idle,
        Run,
        Attack,
        Die
    }

    public bool isGrounded { get; private set; }

    public void Landing(bool grounded)
    {
        isGrounded = grounded;
    }

    public void Recover()
    {
        _animator.Play(TransitionParametr.Idle.ToString());
    }

    private void OnEnable()
    {
        _player.PlayerDie += OnPlayerDie;
    }

    private void OnDisable()
    {
        _player.PlayerDie -= OnPlayerDie;
    }

    private void Update()
    {
        if (this.enabled != false)
        {
            targetVelocity = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            UpdateAnimation();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
            {
                transform.Translate(Vector2.right * targetVelocity * _speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(targetVelocity * _speed * Time.deltaTime, 0, 0));
            }

            if (!Input.GetKey(KeyCode.A))
            {
                FilpDirection(false);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isGrounded)
            {
                transform.Translate(Vector2.right * targetVelocity * _speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3(targetVelocity * _speed * Time.deltaTime, 0, 0));
            }

            if (!Input.GetKey(KeyCode.D))
            {
                FilpDirection(true);
            }
        }
    }

    private void FilpDirection(bool direction)
    {
        transform.localScale = new Vector3(direction ? 1 : -1, transform.localScale.y, transform.localScale.z);
        _playerStats.transform.localScale = new Vector3(direction ? -0.01f : 0.01f, _playerStats.transform.localScale.y, _playerStats.transform.localScale.z);
    }

    private void Jump()
    {
        _animator.Play(TransitionParametr.Jump.ToString());
        Rigidbody2D.velocity = new Vector2(0, 0);
        Rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void UpdateAnimation()
    {
        if (targetVelocity == 0 && isGrounded)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _animator.Play(TransitionParametr.Attack.ToString());
            }
            else
            {
                _animator.Play(TransitionParametr.Idle.ToString());
            }
        }
        else if (targetVelocity != 0 && isGrounded)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _animator.Play(TransitionParametr.Attack.ToString());
            }
            else
            {
                _animator.Play(TransitionParametr.Run.ToString());
            }
        }
        else
        {
            _animator.Play(TransitionParametr.Jump.ToString());
        }
    }

    private void OnPlayerDie()
    {
        _animator.Play(TransitionParametr.Die.ToString());
        this.enabled = false;
    }
}