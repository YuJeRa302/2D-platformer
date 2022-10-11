using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField] private SwordManController _swordManController;

    private void OnTriggerStay2D(Collider2D colider)
    {
        if (colider.CompareTag("Ground"))
        {
            if (_swordManController.Rigidbody2D.velocity.y <= 0)
            {
                _swordManController.Landing(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colider)
    {
        _swordManController.Landing(false);
    }
}
