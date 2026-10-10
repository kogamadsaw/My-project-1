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
        _puzleCanvas.enabled = false;
    }

    public void StartPuzzle()
    {
        _puzleCanvas.enabled = true;
        canDelete = false;
        player.SetActive(false);
    }

    public void ExitPuzzle()
    {
        player.SetActive(true);
        _puzleCanvas.enabled = false;
        canDelete = true;
    }
}
