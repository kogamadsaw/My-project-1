using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputControlScheme;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Canvas _puzleCanvas;

    [HideInInspector] public GameObject player;
    [HideInInspector] public bool canDelete = true;

    [SerializeField] private TMP_Text _terminalNumber;
    [SerializeField] private string _terminalNumberText = "";

    [SerializeField] private TMP_Text _taskTextUI;
    [SerializeField] private TMP_Text _correctCountUI;
    [SerializeField] private TMP_Text _timerText;
    private string _taskText;

    [SerializeField] private TMP_InputField _inputField;
    private float _timeRemaining;

    private int _correctCount;
    [SerializeField] private int _correctToPass;

    [SerializeField] private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789!@#$%^&*()_+-=|;'<>,.[]{}";

    [SerializeField] private string _numbers = "123456789";
    [SerializeField] private string _signs = "+-";

    [HideInInspector] public bool done;

    public enum taskType
    {
        Task1,
        Task2
    }
    public taskType task;

    void Awake()
    {
        _puzleCanvas.enabled = false;

        _inputField.onEndEdit.AddListener(OnInputSubmitted);

        if (_terminalNumber != null) 
        {
            _terminalNumber.text = _terminalNumberText;
        }
    }

    public void StartPuzzle()
    {
        _puzleCanvas.enabled = true;
        canDelete = false;
        player.SetActive(false);

        _correctCount = _correctToPass;

        switch (task)
        {
            case taskType.Task1:
                Task1();
                break;
            case taskType.Task2:
                Task2();
                break;
        }

        StartCoroutine(TimerTick());
    }

    public void ExitPuzzle()
    {
        player.SetActive(true);
        _puzleCanvas.enabled = false;
        canDelete = true;
    }

    private IEnumerator TimerTick()
    {
        while (_timeRemaining > 0)
        {
            _timerText.text = $"{_timeRemaining:F1}";

            yield return null;
            _timeRemaining -= Time.deltaTime;
        }

        if (_timeRemaining <= 0)
        {
            CheckAnswer("");
        }
    }

    private void OnInputSubmitted(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckAnswer(text);
        }
    }

    private void CheckAnswer(string playerText)
    {
        if (playerText.Trim() == _taskText.Trim())
        {
            _correctCount--;

            if (_correctCount <= 0)
            {
                StopCoroutine(TimerTick());
                done = true;
                ExitPuzzle();
            }
            else
            {
                switch (task)
                {
                    case taskType.Task1:
                        Task1();
                        break;
                    case taskType.Task2:
                        Task2();
                        break;
                }
            }
        }
        else
        {
            switch (task)
            {
                case taskType.Task1:
                    Task1();
                    break;
                case taskType.Task2:
                    Task2();
                    break;
            }
        }
    }

    private void Task1()
    {
        _timeRemaining = 15;

        _inputField.text = "";
        _inputField.ActivateInputField();

        _correctCountUI.text = _correctCount.ToString();

        char[] charArray;

        int a = Random.Range(5, 7);
        int randomLetter;

        charArray = new char[a];

        for (int i = 0; i < a; i++)
        {
            randomLetter = Random.Range(0, _alphabet.Length);
            charArray[i] = _alphabet[randomLetter];
        }

        _taskTextUI.text = new string(charArray);

        _taskText = _taskTextUI.text;
    }

    private void Task2()
    {
        _inputField.text = "";
        _inputField.ActivateInputField();
        _correctCountUI.text = _correctCount.ToString();

        _timeRemaining = 20;

        string number1 = "";
        string number2 = "";
        char sign;

        int a = 0;
        int b = 0;
        int c = 0;

        int randomNumber;
        for (int j = 0; j < 2; j++)
        {
            char[] charArray = new char[3];

            for (int i = 0; i < 2; i++)
            {
                a = Random.Range(0, _numbers.Length + 2);

                if (b >= 1) a = Random.Range(0, _numbers.Length);

                else if (a >= _numbers.Length)
                {
                    b += 1;
                    continue;
                }

                randomNumber = Random.Range(0, _numbers.Length);
                charArray[i] = _numbers[randomNumber];
            }

            char[] numbersArray = DeleteNulls(charArray);

            switch (j)
            {
                case 0:
                    number1 = new string(numbersArray);
                    break;
                case 1:
                    number2 = new string(numbersArray);
                    break;
            }
        }

        c = Random.Range(0, _signs.Length);

        sign = _signs[c];

        int num1 = int.Parse(number1);
        int num2 = int.Parse(number2);

        switch (sign)
        {
            case '+':
                _taskText = (num1+num2).ToString();
                break;
            case '-':
                _taskText = (num1-num2).ToString();
                break;
        }

        Debug.Log(_taskText);

        _taskTextUI.text = number1 + "" + sign + "" + number2;
    }



    private char[] DeleteNulls(char[] charArray)
    {
        int count = 0;
        for (int i = 0; i < charArray.Length; i++)
        {
            if (charArray[i] != '\0')
            {
                count++;
            }
        }

        char[] numbersArray = new char[count];

        int index = 0;
        for (int i = 0; i < charArray.Length; i++)
        {
            if (charArray[i] != '\0')
            {
                numbersArray[index] = charArray[i];
                index++;
            }
        }

        return numbersArray;
    }
}
