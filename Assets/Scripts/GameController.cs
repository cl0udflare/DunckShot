using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private Pause _pause;
    [SerializeField] private Lose _lose;
    [SerializeField] private float _pushForce = 4;
    [SerializeField] private float _maxForce = 8;

    private Camera _camera;
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;
    private bool _isDragging;

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _pause.isActiveAndEnabled)
        {
            if (!_ball._canJump) return;

            _isDragging = true;
            OnDragStart();

            _ball.ChangeSimulated(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!_ball._canJump) return;

            _isDragging = false;
            OnDragEnd();

            _ball.ChangeSimulated(true);
        }

        if (_isDragging)
            OnDrag();

        if (!_ball.isActiveAndEnabled)
        {
            _lose.ChangeActive(true);
            _pause.ChangeActive(false);
        }
    }

    private void OnDragStart()
    {
        _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _trajectory.Show();
    }

    private void OnDrag()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;

        var force = _direction * (_distance * _pushForce);

        _force = force.y > _maxForce
            ? new Vector2(force.x, _maxForce)
            : force;

        _trajectory.UpdateDots(_ball.Position, _force);
    }

    private void OnDragEnd()
    {
        _ball.Push(_force);
        _trajectory.Hide();
    }
}