using UnityEngine;

public class BodyMenu : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Pause _pause;
    [SerializeField] private SettingsPanel _settingsPanel;

    private void OnMouseDown()
    {
        if (_settingsPanel.isActiveAndEnabled) return;
        
        ChangeActive(_pause.gameObject, true);
        ChangeActive(_menu.gameObject, false);
    }

    private void ChangeActive(GameObject panel, bool isActive) => panel.SetActive(isActive);
}