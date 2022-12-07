using UnityEngine;

public class Pause : MonoBehaviour
{
    private void OnEnable() => Time.timeScale = 1;

    private void OnDisable() => Time.timeScale = 0;

    public void ChangeActive(bool isActive) => gameObject.SetActive(isActive);
}