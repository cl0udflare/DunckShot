using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _amount;
    private int _bestScore;
    
    private const string BESTSCORE = "bestScore";

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void AddScore(int amount)
    {
        _amount += amount;
        _bestScore += amount;
        _text.text = _amount.ToString();
        
        if (_bestScore > PlayerPrefs.GetInt(BESTSCORE))
            PlayerPrefs.SetInt(BESTSCORE, _bestScore);
    }
}