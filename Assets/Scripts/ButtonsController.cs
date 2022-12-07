using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Color _lightColor;
    [SerializeField] private Color _nightColor;

    private Camera _camera;
    private int _count;

    private const string SAVELIGHT = "save";

    private void Awake()
    {
        _camera = Camera.main;
        _count = PlayerPrefs.GetInt(SAVELIGHT);
        UpdateLite();
    }

    private void UpdateLite()
    {
        print(_count);
        _camera.backgroundColor = _count >= 0 ? _lightColor : _nightColor;
        foreach (var image in _images)
            image.color = _count >= 0 ? _lightColor : _nightColor;
    }

    public void OpenWindow(GameObject panel) => panel.SetActive(true);

    public void CloseWindow(GameObject panel) => panel.SetActive(false);

    public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ChangeLite()
    {
        PlayerPrefs.SetInt(SAVELIGHT, (_count == 0 ? _count += 1: _count) * -1);
        _count = PlayerPrefs.GetInt(SAVELIGHT);

        UpdateLite();
    }
}