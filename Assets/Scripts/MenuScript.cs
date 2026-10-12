using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [HideInInspector] public bool canOpenMenu = true;
    private Canvas _canvas;

    [SerializeField] private bool _isPauseMenu;

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _options;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _options.SetActive(false);
        _menu.SetActive(true);

        if (_isPauseMenu)
        {
            _canvas.enabled = false;
            canOpenMenu = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canOpenMenu && _isPauseMenu)
        {
            OpenPause();
        }
    }

    //main menu options
    public void StartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void SwitchToOptions()
    {
        _options.SetActive(true);
        _menu.SetActive(false);
    }

    public void SwitchToMainMenu()
    {
        _options.SetActive(false);
        _menu.SetActive(true);
    }

    //pause menu options
    public void ContinueGame()
    {
        _canvas.enabled = false;
        canOpenMenu = true;
        Time.timeScale = 1f;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenPause()
    {
        _canvas.enabled = true;
        canOpenMenu = false;
        Time.timeScale = 0f;
    }
}
