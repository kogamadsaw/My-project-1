using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    [HideInInspector] public bool canOpenMenu = true;

    private void Awake()
    {
        _continueButton.onClick.AddListener(continueGame);
        _exitButton.onClick.AddListener(exitToMainMenu);
        gameObject.SetActive(false);
    }

    public void openPause()
    {
        gameObject.SetActive(true);
        canOpenMenu = false;
        Time.timeScale = 0f;
    }

    private void continueGame()
    {
        gameObject.SetActive(false);
        canOpenMenu = true;
        Time.timeScale = 1f;
    }

    public void exitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }
}
