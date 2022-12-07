using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Basket : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _semicircles;
    [SerializeField] private SpriteRenderer _semicirclesPrefab;
    [SerializeField] private StarCoin _starCoinPrefab;
    [SerializeField] private ViewScores _viewScoresPrefab;
    [SerializeField] private Transform _basketCanvas;
    [SerializeField] private Transform _net;
    [SerializeField] private int _addCount = 2;
    [SerializeField] private bool _isFirstBasket;

    private Camera _camera;
    private AudioSource _audioSource;
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector3 _netScale;
    private float _distance;
    private bool _isDragging;

    [NonSerialized] public bool _isSelected;

    private void OnEnable()
    {
        if (Random.Range(0, 6) == 5 && !_isFirstBasket)
            Instantiate(_starCoinPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
    }

    private void Awake() 
    {
        _camera = Camera.main;
        _netScale = _net.localScale;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isSelected) return;
        
            _isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!_isSelected) return;
        
            _isDragging = false;
            _net.localScale = _netScale;
            
            _audioSource.Play();
        }

        if (_isDragging)
            OnDrag();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.gameObject.GetComponent<Ball>();

        _isSelected = ball != null;

        if (ball == null || _isFirstBasket) return;

        foreach (var semicircle in _semicircles)
            semicircle.color = new Color(0.6666667f, 0.6666667f, 0.6666667f, 1);

        _isFirstBasket = true;
        AddScoreText(_addCount);
        StartCoroutine(ball.ChangeMass());
        ball.Score.AddScore(_addCount);

        var semicircles = Instantiate(_semicirclesPrefab, _semicircles[0].transform.position, Quaternion.identity);

        StartCoroutine(ChangeScale(semicircles));
    }

    private void OnDragStart() => _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

    private void OnDrag()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        var rotZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (_distance > 0)
        {
            _net.localScale = new Vector3(0.49f, 0.66f, 0.49f);
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        }
    }

    private void AddScoreText(int amount)
    {
        var viewScores = Instantiate(_viewScoresPrefab, _basketCanvas, false);

        viewScores.SetScore(amount);
    }

    private IEnumerator ChangeScale(SpriteRenderer change)
    {
        float time = 0;
        while (time < 2)
        {
            change.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
            change.color -= new Color(0, 0, 0, Time.deltaTime * 2);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(change.gameObject);
    }
}