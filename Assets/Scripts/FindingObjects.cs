using System.Collections.Generic;
using UnityEngine;

public class FindingObjects : MonoBehaviour
{
    private Puzzle _puzzleScript;

    private CircleCollider2D _foundRobots;
    private bool _canAct;

    //The person who triggered the event
    private PlayerWalking _target;

    //The character we want to switch away from
    private PlayerWalking _fromWho;

    private readonly List<PlayerWalking> _inRange = new List<PlayerWalking>();

    private bool _canChange;

    void Awake()
    {
        _foundRobots = GetComponent<CircleCollider2D>();
        if (_foundRobots == null) Debug.LogError("Add CircleCollider2D to " + gameObject);
        _fromWho = GetComponentInParent<PlayerWalking>();
    }

    void Update()
    {
        if (_fromWho == null || !_fromWho.isCurrentPlayer) return;
        if (Input.GetKeyDown(KeyCode.E) && _canAct)
        {
            if (!_puzzleScript.done)
            {
                _puzzleScript.StartPuzzle();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerWalking target = GetClosestInRange();
            if (target != null)
                _fromWho.SwitchToRobot(target);
        }
    }

    private PlayerWalking GetClosestInRange()
    {
        _inRange.RemoveAll(p => p == null); // clean up destroyed objects

        PlayerWalking best = null;
        float bestDist = float.MaxValue;
        foreach (var p in _inRange)
        {
            float d = ((Vector2)p.transform.position - (Vector2)_fromWho.transform.position).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = p;
            }
        }
        return best;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Task"))
        {
            _puzzleScript = collision.GetComponent<Puzzle>();
            _puzzleScript.player = transform.parent.gameObject;
            _canAct = true;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerWalking other = collision.GetComponentInParent<PlayerWalking>();
            if (other != null && other != _fromWho && !_inRange.Contains(other))
                _inRange.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Task"))
        {
            if (_puzzleScript.canDelete == true)
            {
                _puzzleScript.player = null;
                _puzzleScript = null;
                _canAct = false;
            }
        }

        if (other.CompareTag("Player"))
        {
            PlayerWalking exiting = other.GetComponentInParent<PlayerWalking>();
            if (exiting != null)
                _inRange.Remove(exiting);
        }
    }
}
