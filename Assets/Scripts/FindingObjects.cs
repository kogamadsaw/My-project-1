using UnityEngine;

public class FindingObjects : MonoBehaviour
{
    private bool _canAct;
    private Puzzle _puzzleScript;
    private CircleCollider2D _foundRobots;

    void Awake()
    {
        _foundRobots = GetComponent<CircleCollider2D>();
        if (_foundRobots == null) Debug.LogError("Add CircleCollider2D to " + gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canAct)
        {
            if (!_puzzleScript.done)
            {
                _puzzleScript.StartPuzzle();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Robot"))
        {
            Debug.Log("Robot entered ");

            _puzzleScript = collision.GetComponent<Puzzle>();
            _puzzleScript.player = transform.parent.gameObject;
            Debug.Log(_puzzleScript.player);
            _canAct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            if (_puzzleScript.canDelete == true)
            {
                _puzzleScript.player = null;
                _puzzleScript = null;
                _canAct = false;
            }
        }
    }
}
