using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private Vector3 PositionCamera => transform.position;

    private void Update()
    {
        var position = Vector3.Lerp(PositionCamera, _target.position + new Vector3(0, 2), _speed * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, 0, 0);
        position.y = Mathf.Clamp(position.y, PositionCamera.y, PositionCamera.y + 1);
        position.z = PositionCamera.z;

        transform.position = position;
    }
}