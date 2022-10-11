using UnityEngine;

public class BanditController : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPoints;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoints];

        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoints++;

            if (transform.localScale.x > 0)
            {
                FilpDirection(false);
            }
            else
            {
                FilpDirection(true);
            }

            if (_currentPoints >= _points.Length)
            {
                _currentPoints = 0;
            }
        }
    }
    private void FilpDirection(bool direction)
    {
        transform.localScale = new Vector3(direction ? 1 : -1, transform.localScale.y, transform.localScale.z);
    }
}