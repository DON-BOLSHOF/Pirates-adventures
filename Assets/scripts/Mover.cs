using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private float _current, _target;

    private Vector3 _initialPosition;
    private Vector3 _moveToPosition;

    void Start()
    {
        _initialPosition = GetComponent<Transform>().position;

        _moveToPosition = _initialPosition;
        _moveToPosition.x = _moveToPosition.x + 5;
    }

    void Update()
    {
        if (_current == 0) _target = 1;
        else if (_current == 1) _target = 0;

        _current = Mathf.MoveTowards(_current, _target, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(_initialPosition, _moveToPosition, _current);
    }
}
