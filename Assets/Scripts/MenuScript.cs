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
    [SerializeField] private GameObject _authors;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _options.SetActive(false);
        _authors.SetActive(false);
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
        SceneManager.LoadScene("SampleScene");
    }

    public void SwitchToOptions()
    {
        _options.SetActive(true);
        _authors.SetActive(false);
        _menu.SetActive(false);
    }

    public void SwitchToMainMenu()
    {
        _options.SetActive(false);
        _authors.SetActive(false);
        _menu.SetActive(true);
    }

    public void SwitchToAuthors()
    {
        _authors.SetActive(true);
        _menu.SetActive(false);
        _options.SetActive(false);
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
