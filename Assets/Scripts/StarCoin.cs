using UnityEngine;

public class StarCoin : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private const string STARCOIN = "coin";

    private void OnTriggerEnter2D(Collider2D col)
    {
        var ball = col.gameObject.GetComponent<Ball>();
        if (ball == null)
            return;

        ball.GetComponent<AudioSource>().PlayOneShot(_audioClip);
        
        var coins = PlayerPrefs.GetInt(STARCOIN);
        PlayerPrefs.SetInt(STARCOIN, coins + 1);
        Destroy(gameObject);
    }
}