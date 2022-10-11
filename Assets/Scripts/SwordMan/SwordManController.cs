using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SwordManController : MonoBehaviour
{
    [Header("[Rigidbody]")]
    public Rigidbody2D Rigidbody2D;

    [Header("[Animator]")]
    [SerializeField] private Animator _animator;
    [Header("[Parametr]")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 10f;

    protected float targetVelocity;
    enum TransitionParametr
    {
        Jump,
        Idle,
        Run
    }

    public bool isGrounded { get; private set; }

    public void Landing(bool grounded)
    {
        isGrounded = grounded;
    }

    private void Update()
    {
        targetVelocity = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        UpdateAnimation();
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
        transform.localScale = new Vector3(direction ? 1 : -1, 1, 1);
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
            _animator.Play(TransitionParametr.Idle.ToString());
        }
        else if (targetVelocity != 0 && isGrounded)
        {
            _animator.Play(TransitionParametr.Run.ToString());
        }
        else
        {
            _animator.Play(TransitionParametr.Jump.ToString());
        }
    }
}
