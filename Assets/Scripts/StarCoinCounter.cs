using TMPro;
using UnityEngine;

public class StarCoinCounter : MonoBehaviour
{
    private TextMeshProUGUI _starCoinCounterText;

    private const string STARCOIN = "coin";

    private void Awake()
    {
        _starCoinCounterText = GetComponent<TextMeshProUGUI>();
        _starCoinCounterText.text = PlayerPrefs.GetInt(STARCOIN).ToString();
    }

    private void FixedUpdate() => _starCoinCounterText.text = PlayerPrefs.GetInt(STARCOIN).ToString();
}