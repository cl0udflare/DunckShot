using System.Collections;
using TMPro;
using UnityEngine;

public class ViewScores : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _speedup = 0.5f;

    private TextMeshProUGUI _scoreText;
    private Vector2 Position => transform.position;

    private void Awake() => _scoreText = GetComponent<TextMeshProUGUI>();

    private void OnEnable() => StartCoroutine(DestroyAfter(_lifeTime));

    private void Update() => transform.position =
        Vector2.MoveTowards(Position, Position + Vector2.up, _speedup * Time.deltaTime);
    
    public void SetScore(int value) => _scoreText.text = "+" + value;

    private IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}