using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Canvas _mainMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _mainMenuButton.onClick.AddListener(backToMainMenu);
    }

    void backToMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
