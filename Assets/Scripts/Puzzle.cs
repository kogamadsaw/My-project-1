using System.Collections;
using UnityEngine;
using TMPro;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Canvas _puzleCanvas;

    [HideInInspector] public GameObject player;
    [HideInInspector] public bool canDelete = true;

    [SerializeField] private TMP_Text _taskTextUI;
    [SerializeField] private TMP_Text _correctCountUI;
    [SerializeField] private TMP_Text _timerText;
    private string _taskText;

    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private float _timerTime = 20;
    private float _timeRemaining;

    int _correctCount;
    int _correctToPass = 3;

    [HideInInspector] public bool done;

    void Awake()
    {
        _puzleCanvas.enabled = false;
        _puzleCanvas.enabled = false;

        _inputField.onEndEdit.AddListener(OnInputSubmitted);
    }

    public void StartPuzzle()
    {
        _puzleCanvas.enabled = true;
        canDelete = false;
        player.SetActive(false);
        Task1();

        _correctCount = 0;
        StartCoroutine(TimerTick());
    }

    public void ExitPuzzle()
    {
        player.SetActive(true);
        _puzleCanvas.enabled = false;
        canDelete = true;
    }

    private void Task1()
    {
        _timeRemaining = _timerTime;

        _inputField.text = "";
        _inputField.ActivateInputField();

        _correctCountUI.text = _correctCount.ToString();

        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789!@#$%^&*()_+-=|;'<>,.[]{}";

        char[] charArray;

        int a = Random.Range(5, 12);
        int randomLetter;

        charArray = new char[a];

        for (int i = 0; i < a; i++)
        {
            randomLetter = Random.Range(0, alphabet.Length);
            charArray[i] = alphabet[randomLetter];
        }

        _taskText = new string(charArray);

        _taskTextUI.text = _taskText;
    }

    private void OnInputSubmitted(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckAnswer(text);
            Debug.Log(text);
        }
    }

    private void CheckAnswer(string playerText)
    {
        if (playerText.Trim() == _taskText.Trim())
        {
            _correctCount++;

            if (_correctCount >= _correctToPass)
            {
                StopCoroutine(TimerTick());
                done = true;
                ExitPuzzle();
            }
            else
            {
                Task1();
            }
        }
        else
        {
            Task1();
        }
    }

    private IEnumerator TimerTick()
    {
        while (_timeRemaining > 0)
        {
            _timerText.text = $"{_timeRemaining:F1}";

            yield return null;
            _timeRemaining -= Time.deltaTime;
        }

        // Если время вышло, а таймер всё еще шел — фиксируем окончание
        if (_timeRemaining <= 0)
        {
            CheckAnswer("");
        }
    }
}
