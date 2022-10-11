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
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            if (_currentPoints >= _points.Length)
            {
                _currentPoints = 0;
            }
        }
    }
}
