using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _optionsButton;

    [SerializeField] private Canvas _optionsCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _optionsButton.onClick.AddListener(SwitchToOptions);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void SwitchToOptions()
    {
        _optionsCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
