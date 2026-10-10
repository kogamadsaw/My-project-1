using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    [SerializeField] Canvas _puzleCanvas;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool canDelete = true;

    private Button _exitPuzzleButton;

    void Awake()
    {
        _puzleCanvas.enabled = false;
        _exitPuzzleButton = _puzleCanvas.GetComponentInChildren<Button>();
        _exitPuzzleButton.onClick.AddListener(ExitPuzzle);
    }

    public void StartPuzzle()
    {
        _puzleCanvas.enabled = true;
        canDelete = false;
        player.SetActive(false);
    }

    void ExitPuzzle()
    {
        _puzleCanvas.enabled = false;
        canDelete = true;
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
