using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerStay2D(Collider2D colider)
    {
        if (colider.TryGetComponent<Ground>(out Ground ground))
        {
            if (_playerMovement.Rigidbody2D.velocity.y <= 0)
            {
                _playerMovement.Landing(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        _playerMovement.Landing(false);
    }
}