using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance = 0.6f;

    private Rigidbody2D _rigidbody2D;

    [NonSerialized] public bool _canJump = true;

    public Vector2 Position => transform.position;
    public Score Score => _score;

    private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _layerMask);
        _canJump = hit.collider;
    }

    private void OnCollisionEnter2D() => GetComponent<AudioSource>().PlayOneShot(_audioClip);

    private void OnBecameInvisible() => gameObject.SetActive(false);

    public void Push(Vector2 force) => _rigidbody2D.AddForce(force, ForceMode2D.Impulse);

    public void ChangeSimulated(bool isSimulated) => _rigidbody2D.simulated = isSimulated;

    public IEnumerator ChangeMass()
    {
        _rigidbody2D.mass = 5;
        yield return new WaitForSeconds(0.1f);
        _rigidbody2D.mass = 1;
    }
}