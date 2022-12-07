using TMPro;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _BestScoreText;

    private const string BESTSCORE = "bestScore";

    private void OnEnable() => _BestScoreText.text = PlayerPrefs.GetInt(BESTSCORE).ToString();
    
    public void ChangeActive(bool isActive) => gameObject.SetActive(isActive);
}